using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WhiteBoard.Context;
using WhiteBoard.Messages;
using WhiteBoard.Model;
using WhiteBoard.Services.Classes;
using WhiteBoard.Services.Interfaces;
using WhiteBoard.View;

namespace WhiteBoard.ViewModel
{
    internal class DrawVM : ViewModelBase
    {
        private readonly INavigateService _navigateService;
        private readonly ISaveService _saveService;
        private readonly ISendService _sendService;public UsersModel Users { get; set; } = new();
        public string ImgName;

        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get => _imageBytes;
            set => Set(ref _imageBytes, value);
        }

        private string _tempImgName;
        public string TempImgName
        {
            get => _tempImgName;
            set => Set(ref _tempImgName, value);
        }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set => Set(ref _imagePath, value);
        }

        public DrawVM(INavigateService navigateService, ISaveService saveService, ISendService sendService)
        {
            _navigateService = navigateService;
            _saveService = saveService;
            _sendService = sendService;

            Messenger.Default.Register<UserMessage>(this, message =>
            {
                Users = message.User;
            });

            Messenger.Default.Register<string>(this, imagePath =>
            {
                LoadImage(imagePath);
            });
        }

        private void LoadImage(string imagePath)
        {
            ImagePath = imagePath;
        }

        public RelayCommand EraserCommand
        {
            get => new(() =>
            {
                
            });
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
                    _saveService.SaveAsCommand(inkCanvas, ImgName, Users);
                    _navigateService.NavigateTo<LibraryVM>();
                }

                else
                {
                    MessageBox.Show($"{ImgName} not saved", "Info");
                }
            });
        }

        public RelayCommand<InkCanvas> SaveCommand
        {
            get => new RelayCommand<InkCanvas>((inkCanvas) =>
            {
                if (inkCanvas != null)
                {
                    _saveService.SaveCommand(inkCanvas, ImgName, Users);
                    _navigateService.NavigateTo<LibraryVM>();
                }

                else
                {
                    MessageBox.Show($"{ImgName}.png not saved", "Info");
                }
            });
        }

        public RelayCommand<InkCanvas> SendCommand
        {
            get => new RelayCommand<InkCanvas>((inkCanvas) =>
            {
                if (inkCanvas != null)
                {
                    _sendService.Send(inkCanvas, ImageBytes);
                }
            });
        }

        public RelayCommand<InkCanvas> PrintCommand
        {
            get => new RelayCommand<InkCanvas>((inkCanvas) =>
            {
                PrintDialog printDialog = new();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(inkCanvas, "InkCanvas Print");
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
                using WhiteBoardDbContext context = new();
                if (Users != null)
                {
                    var userKeepEntries = context.Keep.Where(x => x.UserID == Users.ID).ToList();
                    context.Keep.RemoveRange(userKeepEntries);
                    context.SaveChanges();
                }

                _navigateService.NavigateTo<LogVM>();
            });
        }
    }
}