using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using WhiteBoard.Messages;
using WhiteBoard.Model;
using WhiteBoard.Services.Interfaces;

namespace WhiteBoard.ViewModel
{
    class MainVM : ViewModelBase
    {
        private ViewModelBase selectedPage;
        private readonly IMessenger _messenger;
        private readonly IUserManageService _userManage;
        public UsersModel User { get; set; }

        public ViewModelBase CurrentViewModel
        {
            get => selectedPage;
            set
            {
                Set(ref selectedPage, value);
            }
        }

        public void ReceiveMessage(NavigationMessage message)
        {
            CurrentViewModel = App.Container.GetInstance(message.VMType) as ViewModelBase;
        }

        public MainVM(IMessenger messenger, IUserManageService userManage)
        {
            User = new();

            _messenger = messenger;
            _userManage = userManage;
            _messenger.Register<NavigationMessage>(this, ReceiveMessage);

            if (_userManage.HasKeepUser())
            {
                var userId = _userManage.GetKeepUserId();
                var user = _userManage.GetUserById(userId);
                if (user != null)
                {
                    _messenger.Send(new UserMessage 
                    { 
                        User = user 
                    });


                    var libraryViewModel = App.Container.GetInstance<LibraryVM>();
                    libraryViewModel.Users = user;
                    libraryViewModel.LoadUserPictures();
                    CurrentViewModel = libraryViewModel;
                }
            }

            else
            {
                CurrentViewModel = App.Container.GetInstance<LogVM>();
            }
        }
    }
}