using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using WhiteBoard.Context;
using WhiteBoard.Converters;
using WhiteBoard.Messages;
using WhiteBoard.Model;
using WhiteBoard.Services.Interfaces;

namespace WhiteBoard.ViewModel
{
    class LibraryVM : ViewModelBase
    {
        public UsersModel Users { get; set; } = new();
        private readonly INavigateService _navigateService;
        private readonly IMessenger _messenger;

        private ObservableCollection<PicturesModel> _userPictures;
        public ObservableCollection<PicturesModel> UserPictures
        {
            get => _userPictures;
            set => Set(ref _userPictures, value);
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    Set(ref _username, value);
                }
            }
        }

        private string _selectedImagePath;
        public string SelectedImagePath
        {
            get => _selectedImagePath;
            set => Set(ref _selectedImagePath, value);
        }

        public LibraryVM(INavigateService navigateService, IMessenger messenger)
        {
            _navigateService = navigateService;
            _messenger = messenger;

            _messenger.Register<UserMessage>(this, message =>
            {
                Users = message.User;
                LoadUserPictures();
            });
        }

        public void LoadUserPictures()
        {
            using WhiteBoardDbContext context = new();
            var userPictures = context.Pictures.Where(p => p.UserID == Users.ID).ToList();
            UserPictures = new ObservableCollection<PicturesModel>(userPictures);
        }

        public RelayCommand<PicturesModel> OpenPictureCommand
        {
            get => new((picture) =>
            {
                if (picture != null)
                {
                    _messenger.Send(picture.PicturePath);
                    _navigateService.NavigateTo<DrawVM>();
                }
            });
        }

        public RelayCommand AddCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<DrawVM>();
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
                    UserConverter.UserID = 0;
                }
                _navigateService.NavigateTo<LogVM>();
            });
        }
    }
}