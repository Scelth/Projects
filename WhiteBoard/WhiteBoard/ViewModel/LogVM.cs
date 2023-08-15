using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;
using WhiteBoard.Messages;
using WhiteBoard.Model;
using WhiteBoard.Services.Interfaces;

namespace WhiteBoard.ViewModel
{
    class LogVM : ViewModelBase
    {
        public UsersModel User { get; set; } = new();

        private readonly INavigateService _navigateService;
        private readonly IUserManageService _userService;
        private readonly IMessenger _messenger;
        public string Username { get; set; }

        private PasswordBox _passwordBox;
        public PasswordBox PasswordBox
        {
            get => _passwordBox;
            set
            {
                Set(ref _passwordBox, value);
            }
        }

        private bool _isKeepLoggedIn;
        public bool IsKeepLoggedIn
        {
            get => _isKeepLoggedIn;
            set
            {
                Set(ref _isKeepLoggedIn, value);

                if (value)
                {
                    KeepUser();
                }
            }
        }

        private void KeepUser()
        {
            if (!string.IsNullOrEmpty(Username) && PasswordBox != null && !string.IsNullOrEmpty(PasswordBox.Password))
            {
                var user = _userService.GetUser(Username, PasswordBox.Password);

                _userService.SaveUserToKeep(user.ID);

                MessageBox.Show($"{user.Username} will be kept logged in.");
            }
        }

        public LogVM(INavigateService navigateService, IMessenger messenger, IUserManageService userService)
        {
            _navigateService = navigateService;
            _messenger = messenger;
            _userService = userService;

            _messenger.Register<DataMessage>(this, message =>
            {
                User = message.Data as UsersModel;
            });
        }

        public RelayCommand<object> LoginCommand
        {
            get => new(param =>
            {
                PasswordBox = param as PasswordBox;

                var user = _userService.GetUser(Username, PasswordBox.Password);

                if (user != null)
                {
                    if (IsKeepLoggedIn)
                    {
                        KeepUser();
                    }

                    Messenger.Default.Send(new UserMessage
                    { 
                        User = user
                    });

                    MessageBox.Show($"{user.Username} logged in");
                    _navigateService.NavigateTo<LibraryVM>();
                }

                else
                {
                    MessageBox.Show("Invalid login or password.");
                }
            });
        }

        public RelayCommand RegisterCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<RegisterVM>();
            });
        }
    }
}