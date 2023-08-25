using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using WhiteBoard.Library;
using WhiteBoard.Context;
using WhiteBoard.Messages;
using WhiteBoard.Model;
using WhiteBoard.Services.Interfaces;
using System.Windows.Media;
using System.IO;

namespace WhiteBoard.ViewModel
{
    internal class DrawVM : ViewModelBase
    {
        private readonly INavigateService _navigateService;
        private readonly ISaveService _saveService;
        private readonly ISendService _sendService;
        private readonly IMessenger _messenger;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public UsersModel Users { get; set; } = new();
        public ColorList ColorList { get; set; } = new();
        public string ImgName;

        private InkCanvasEditingMode _myInkCanvasEditing = InkCanvasEditingMode.Ink;
        public InkCanvasEditingMode MyInkCanvasEditing 
        { 
            get => _myInkCanvasEditing; 
            set 
            {
                _myInkCanvasEditing = value; 
                NotifyPropertyChanged(nameof(InkCanvasEditingMode));
            }
        }

        private DrawingAttributes _drawingAttributes = new();
        public DrawingAttributes DrawingAttributes 
        { 
            get => _drawingAttributes; 
            set 
            {
                _drawingAttributes = value; 
                NotifyPropertyChanged(nameof(DrawingAttributes)); 
            } 
        }

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

        public DrawVM(INavigateService navigateService, ISaveService saveService, ISendService sendService, IMessenger messenger)
        {
            _navigateService = navigateService;
            _saveService = saveService;
            _sendService = sendService;
            _messenger = messenger;

            DrawingAttributes.Color = (Color)ColorConverter.ConvertFromString("WhiteSmoke");
            DrawingAttributes.Height = 15;
            DrawingAttributes.Width = 15;

            _messenger.Register<UserMessage>(this, message =>
            {
                Users = message.User;
            });

            _messenger.Register<string>(this, imagePath =>
            {
                LoadImage(imagePath);
            });
        }

        private void LoadImage(string imagePath)
        {
            ImagePath = imagePath;
            ImageBytes = File.ReadAllBytes(imagePath);
            NotifyPropertyChanged(nameof(ImagePath));
        }

        public RelayCommand EraserCommand
        {
            get => new(() =>
            {

                DrawingAttributes.Color = (Color)ColorConverter.ConvertFromString("#11131B");
                DrawingAttributes.StylusTip = StylusTip.Rectangle;
                MyInkCanvasEditing = InkCanvasEditingMode.EraseByPoint;
            });
        }

        public RelayCommand PenCommand
        {
            get => new(() =>
            {
                MyInkCanvasEditing = InkCanvasEditingMode.Ink;
            });
        }

        public RelayCommand<string> ChoiceColor
        {
            get => new(param =>
            {
                DrawingAttributes.Color = (Color)ColorConverter.ConvertFromString(param);
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
            get => new((inkCanvas) =>
            {
                if (inkCanvas != null)
                {
                    if(ImgName != "" && ImgName != null)
                    {
                        _saveService.SaveAsCommand(inkCanvas, ImgName, Users);
                        _navigateService.NavigateTo<LibraryVM>();
                    }

                    else
                    {
                        MessageBox.Show($"Enter image name", "Info");
                    }
                }

                else
                {
                    MessageBox.Show($"{ImgName} not saved", "Info");
                }
            });
        }

        public RelayCommand<InkCanvas> SaveCommand
        {
            get => new((inkCanvas) =>
            {
                if (inkCanvas != null)
                {
                    if (ImgName != "" && ImgName != null)
                    {
                        _saveService.SaveCommand(inkCanvas, ImgName, Users);
                        _navigateService.NavigateTo<LibraryVM>();
                    }

                    else
                    {
                        MessageBox.Show($"Enter image name", "Info");
                    }
                }

                else
                {
                    MessageBox.Show($"{ImgName}.png not saved", "Info");
                }
            });
        }

        public RelayCommand<InkCanvas> SendCommand
        {
            get => new((inkCanvas) =>
            {
                if (inkCanvas != null)
                {
                    _sendService.Send(inkCanvas, ImageBytes);
                }
            });
        }

        public RelayCommand<InkCanvas> PrintCommand
        {
            get => new((inkCanvas) =>
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