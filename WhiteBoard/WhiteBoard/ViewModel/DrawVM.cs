﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WhiteBoard.Context;
using WhiteBoard.Messages;
using WhiteBoard.Model;
using WhiteBoard.Services.Interfaces;

namespace WhiteBoard.ViewModel
{
    internal class DrawVM : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly INavigateService _navigateService;

        public UsersModel Users { get; set; } = new();
        public string ImgName;

        public DrawVM(IMessenger messenger, INavigateService navigateService)
        {
            _messenger = messenger;
            _navigateService = navigateService;

            Messenger.Default.Register<UserMessage>(this, message =>
            {
                Users = message.User;
            });
        }

        private string _tempImgName;
        public string TempImgName
        {
            get => _tempImgName;
            set
            {
                _tempImgName = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand SaveNameCommand
        {
            get => new RelayCommand(() =>
            {
                ImgName = TempImgName;

                MessageBox.Show(ImgName, "Info");
            });
        }

        public RelayCommand<InkCanvas> SaveAsCommand
        {
            get => new RelayCommand<InkCanvas>((inkCanvas) =>
            {
                if (inkCanvas != null)
                {
                    double inkCanvasWidth = inkCanvas.ActualWidth;
                    double inkCanvasHeight = inkCanvas.ActualHeight;

                    RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)inkCanvasWidth, (int)inkCanvasHeight, 96d, 96d, System.Windows.Media.PixelFormats.Default);
                    renderBitmap.Render(inkCanvas);

                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                    var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                    saveFileDialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg;*.jpg)|*.jpeg;*.jpg|All Files (*.*)|*.*";
                    saveFileDialog.FileName = ImgName;
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                        {
                            encoder.Save(fileStream);
                        }

                        var savePath = saveFileDialog.FileName;

                        using (var context = new WhiteBoardDbContext())
                        {
                            var picture = new PicturesModel
                            {
                                UserID = Users.ID,
                                Name = ImgName,
                                Date = DateTime.Now,
                                PicturePath = savePath
                            };

                            context.Pictures.Add(picture);
                            context.SaveChanges();
                        }

                        MessageBox.Show($"{ImgName} saved", "Info");

                        _navigateService.NavigateTo<LogVM>();
                    }

                    else
                    {
                        MessageBox.Show($"{ImgName} not saved", "Info");
                    }
                }
            });
        }

        public RelayCommand<InkCanvas> SaveCommand
        {
            get => new RelayCommand<InkCanvas>((inkCanvas) =>
            {
                if (inkCanvas != null)
                {
                    double inkCanvasWidth = inkCanvas.ActualWidth;
                    double inkCanvasHeight = inkCanvas.ActualHeight;

                    RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)inkCanvasWidth, (int)inkCanvasHeight, 96d, 96d, System.Windows.Media.PixelFormats.Default);
                    renderBitmap.Render(inkCanvas);

                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                    var userFolderPath = Path.Combine(Directory.GetCurrentDirectory(), Users.Username);

                    if (!Directory.Exists(userFolderPath))
                    {
                        Directory.CreateDirectory(userFolderPath);
                    }

                    var fileName = $"{ImgName}.png"; // Создаем имя файла с расширением PNG
                    var filePath = Path.Combine(userFolderPath, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        encoder.Save(fileStream);
                    }

                    using (var context = new WhiteBoardDbContext())
                    {
                        var picture = new PicturesModel
                        {
                            UserID = Users.ID,
                            Name = ImgName,
                            Date = DateTime.Now,
                            PicturePath = filePath
                        };

                        context.Pictures.Add(picture);
                        context.SaveChanges();
                    }

                    MessageBox.Show($"{ImgName}.png saved", "Info");

                    _navigateService.NavigateTo<LibraryVM>();
                }

                else
                {
                    MessageBox.Show($"{ImgName}.png not saved", "Info");
                }
            });
        }

        public RelayCommand BackCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<LibraryVM>();
            });
        }

        public RelayCommand LogOutCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<LogVM>();
            });
        }
    }
}