using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WhiteBoard.Services.Interfaces;
using WhiteBoard.View;
using WhiteBoard.ViewModel;

namespace WhiteBoard.Services.Classes
{
    internal class SendService : ISendService
    {
        byte[] Image;

        public void Send(InkCanvas inkCanvas, byte[] ImageBytes)
        {
            Image = ImageBytes;

            double inkCanvasWidth = inkCanvas.ActualWidth;
            double inkCanvasHeight = inkCanvas.ActualHeight;

            RenderTargetBitmap renderBitmap = new((int)inkCanvasWidth, (int)inkCanvasHeight, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            renderBitmap.Render(inkCanvas);

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using MemoryStream ms = new();
            encoder.Save(ms);
            Image = ms.ToArray();

            var sendVM = new SendVM(this);
            sendVM.ImageBytes = Image;

            var sendView = new SendView
            {
                DataContext = sendVM
            };

            sendView.ShowDialog();
        }

        public void SendToEmail(string Email, string Subject, string Name, string Message)
        {
            MailAddress fromAddress = new("your email", Name);
            MailAddress toAddress = new(Email);

            MailMessage mailMessage = new(fromAddress, toAddress);
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;

            using MemoryStream imageStream = new(Image);
            var imageAttachment = new Attachment(imageStream, "image.png");
            mailMessage.Attachments.Add(imageAttachment);

            mailMessage.Body = $@"<html><body><h1>{Message}</h1><img src='cid:image.png'></img></body></html>";

            using SmtpClient smtpClient = new("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("your email", "your device password");
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mailMessage);
                MessageBox.Show("Email sent successfully.", "Info");
            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error");
            }
        }
    }
}
