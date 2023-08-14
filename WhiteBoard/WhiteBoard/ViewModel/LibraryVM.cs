using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WhiteBoard.Context;
using WhiteBoard.Messages;
using WhiteBoard.Model;
using WhiteBoard.Services.Interfaces;

namespace WhiteBoard.ViewModel
{
    class LibraryVM : ViewModelBase
    {
        public UsersModel Users { get; set; } = new();
        private readonly IMessenger _messenger;
        private readonly INavigateService _navigateService;

        private ObservableCollection<PicturesModel> _userPictures;
        public ObservableCollection<PicturesModel> UserPictures
        {
            get => _userPictures;
            set
            {
                _userPictures = value;
                RaisePropertyChanged();
            }
        }

        public PicturesModel SelectedPicture { get; set; }

        public LibraryVM(IMessenger messenger, INavigateService navigateService)
        {
            _messenger = messenger;
            _navigateService = navigateService;

            Messenger.Default.Register<UserMessage>(this, message =>
            {
                Users = message.User;
                LoadUserPictures();
            });
        }

        private void LoadUserPictures()
        {
            using (WhiteBoardDbContext context = new())
            {
                UserPictures = new(context.Pictures.Where(p => p.UserID == Users.ID).ToList());
            }
        }

        public RelayCommand<PicturesModel> OpenPictureCommand
        {
            get => new RelayCommand<PicturesModel>((picture) =>
            {
                if (picture != null)
                {
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
                _navigateService.NavigateTo<LogVM>();
            });
        }
    }
}
