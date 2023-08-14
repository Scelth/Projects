using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
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

        //public bool IsKeepLoggedIn
        //{
        //    get => AppSettings.IsKeepLoggedIn;
        //    set
        //    {
        //        AppSettings.IsKeepLoggedIn = value;
        //        RaisePropertyChanged();
        //    }
        //}

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
                var password = param as PasswordBox;
                var user = _userService.GetUser(Username, password.Password);

                if (user != null)
                {
                    Messenger.Default.Send(new UserMessage { User = user }); // Отправляем сообщение с данными пользователя
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
