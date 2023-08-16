using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WhiteBoard.Context;
using WhiteBoard.Model;
using WhiteBoard.Services.Interfaces;
using WhiteBoard.View;

namespace WhiteBoard.ViewModel
{
    public class SendVM : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly INavigateService _navigateService;
        public byte[] ImageBytes { get; set; }

        private string _subject;
        public string Subject
        {
            get => _subject;
            set
            {
                Set(ref _subject, value);
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                Set(ref _email, value);
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                Set(ref _message, value);
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                Set(ref _name, value);
            }
        }

        public SendVM(IMessenger messenger, INavigateService navigateService)
        {
            _messenger = messenger;
            _navigateService = navigateService;
        }

        public RelayCommand SendCommand
        {
            get => new(() =>
            {
                MailAddress fromAddress = new("adam.magomed1459@gmail.com", Name);
                MailAddress toAddress = new(Email);

                MailMessage mailMessage = new(fromAddress, toAddress);
                mailMessage.Subject = Subject;
                mailMessage.IsBodyHtml = true;

                using (MemoryStream imageStream = new MemoryStream(ImageBytes))
                {
                    var imageAttachment = new Attachment(imageStream, "image.png");
                    mailMessage.Attachments.Add(imageAttachment);

                    mailMessage.Body = $@"<html><body><h1>{Message}</h1><img src='cid:image.png'></img></body></html>";

                    using (SmtpClient smtpClient = new("smtp.gmail.com", 587))
                    {
                        smtpClient.Credentials = new NetworkCredential("adam.magomed1459@gmail.com", "xwirgbuyziarbmgu");
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
            });
        }
    }
}
