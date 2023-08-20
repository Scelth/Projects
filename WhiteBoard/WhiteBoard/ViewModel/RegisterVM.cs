using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WhiteBoard.Messages;
using WhiteBoard.Model;
using WhiteBoard.Services.Classes;
using WhiteBoard.Services.Interfaces;

namespace WhiteBoard.ViewModel
{
    class RegisterVM : ViewModelBase
    {
        public UsersModel User { get; set; } = new();

        private readonly INavigateService _navigateService;
        private readonly IMessenger _messenger;
        private readonly IUserManageService _userService;
        private readonly ICheckService _checkService;

        public RegisterVM(INavigateService navigateService, IMessenger messenger, IUserManageService userService, ICheckService checkService)
        {
            _navigateService = navigateService;
            _messenger = messenger;
            _userService = userService;
            _checkService = checkService;

            _messenger.Register<DataMessage>(this, message =>
            {
                User = message.Data as UsersModel;
            });
        }

        public RelayCommand<object> RegisterCommand
        {
            get =>
                new(param =>
                {
                    if (param != null)
                    {
                        object[] res = (object[])param;
                        var password = (PasswordBox)res[0];
                        var confirm = (PasswordBox)res[1];

                        var checker = new PasswordService(password, confirm);

                        if (checker.IsMatch() && _checkService.CheckUserExist(User.Username))
                        {
                            User.Password = password.Password;
                            _userService.Add(User);
                            _navigateService.NavigateTo<LogVM>();
                        }

                        else
                        {
                            MessageBox.Show("The username already exists or passwords do not match.", "Notice", MessageBoxButton.OK, MessageBoxImage.Stop);
                        }
                    }
                });
        }

        public RelayCommand BackCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<LogVM>();
            });
        }
    }
}