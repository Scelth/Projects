using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WhiteBoard.Context;
using WhiteBoard.Messages;
using WhiteBoard.Model;
using WhiteBoard.Services.Interfaces;
using WhiteBoard.View;

namespace WhiteBoard.ViewModel
{
    internal class DrawVM : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly INavigateService _navigateService;

        public UsersModel Users { get; set; } = new();
        public string ImgName;

        private PicturesModel _selectedPicture;
        public PicturesModel SelectedPicture
        {
            get => _selectedPicture;
            set
            {
                Set(ref _selectedPicture, value);
            }
        }

        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get => _imageBytes;
            set
            {
                Set(ref _imageBytes, value);
            }
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get => _imageSource;
            set
            {
                Set(ref _imageSource, value);
            }
        }

        public DrawVM(IMessenger messenger, INavigateService navigateService)
        {
            _messenger = messenger;
            _navigateService = navigateService;

            Messenger.Default.Register<UserMessage>(this, message =>
            {
                Users = message.User;
            });

            Messenger.Default.Register<string>(this, picturePath =>
            {
                ImageSource = new BitmapImage(new Uri(picturePath));
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

                        _navigateService.NavigateTo<LibraryVM>();
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

                    var fileName = $"{ImgName}.png";
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

        public RelayCommand<InkCanvas> SendCommand
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

                    using (MemoryStream ms = new MemoryStream())
                    {
                        encoder.Save(ms);
                        ImageBytes = ms.ToArray();
                    }

                    var sendVM = new SendVM(_messenger, _navigateService);
                    sendVM.ImageBytes = ImageBytes;
                    var sendView = new SendView
                    {
                        DataContext = sendVM
                    };

                    sendView.ShowDialog();
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
                using (WhiteBoardDbContext context = new())
                {
                    if (Users != null)
                    {
                        var userKeepEntries = context.Keep.Where(x => x.UserID == Users.ID).ToList();
                        context.Keep.RemoveRange(userKeepEntries);
                        context.SaveChanges();
                    }
                }

                _navigateService.NavigateTo<LogVM>();
            });
        }
    }
}