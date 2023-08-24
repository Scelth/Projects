using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WhiteBoard.Context;
using WhiteBoard.Converters;
using WhiteBoard.Model;
using WhiteBoard.Services.Interfaces;

namespace WhiteBoard.Services.Classes
{
    internal class SaveService : ISaveService
    {
        public void SaveCommand(InkCanvas inkCanvas, string ImgName, UsersModel Users)
        {
            string userFolderPath;
            double inkCanvasWidth = inkCanvas.ActualWidth;
            double inkCanvasHeight = inkCanvas.ActualHeight;

            RenderTargetBitmap renderBitmap = new((int)inkCanvasWidth, (int)inkCanvasHeight, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            renderBitmap.Render(inkCanvas);

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            if (UserConverter.UserID != 0)
            {
                KeepModel Keep = new();
                Keep.UserID = UserConverter.UserID;
                using var _context = new WhiteBoardDbContext();
                var user = _context.Users.FirstOrDefault(u => u.ID == Keep.UserID);
                string username = user.Username;
                userFolderPath = Path.Combine(Directory.GetCurrentDirectory(), username);
                Users.ID = Keep.UserID;
            }

            else
            {
                userFolderPath = Path.Combine(Directory.GetCurrentDirectory(), Users.Username);
            }

            if (!Directory.Exists(userFolderPath))
            {
                Directory.CreateDirectory(userFolderPath);
            }

            var fileName = $"{ImgName}.png";
            var filePath = Path.Combine(userFolderPath, fileName);

            using FileStream fileStream = new(filePath, FileMode.Create);
            encoder.Save(fileStream);

            using WhiteBoardDbContext context = new();
            var picture = new PicturesModel
            {
                UserID = Users.ID,
                Name = ImgName,
                Date = DateTime.Now,
                PicturePath = filePath
            };

            context.Pictures.Add(picture);
            context.SaveChanges();

            MessageBox.Show($"{ImgName}.png saved", "Info");
        }

        public void SaveAsCommand(InkCanvas inkCanvas, string ImgName, UsersModel Users)
        {
            double inkCanvasWidth = inkCanvas.ActualWidth;
            double inkCanvasHeight = inkCanvas.ActualHeight;

            RenderTargetBitmap renderBitmap = new((int)inkCanvasWidth, (int)inkCanvasHeight, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            renderBitmap.Render(inkCanvas);

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg;*.jpg)|*.jpeg;*.jpg|All Files (*.*)|*.*";
            saveFileDialog.FileName = ImgName;

            if (saveFileDialog.ShowDialog() == true)
            {
                using FileStream fileStream = new(saveFileDialog.FileName, FileMode.Create);
                encoder.Save(fileStream);

                var savePath = saveFileDialog.FileName;

                using WhiteBoardDbContext context = new();
                var picture = new PicturesModel
                {
                    UserID = Users.ID,
                    Name = ImgName,
                    Date = DateTime.Now,
                    PicturePath = savePath
                };

                context.Pictures.Add(picture);
                context.SaveChanges();

                MessageBox.Show($"{ImgName} saved", "Info");
            }
        }
    }
}
