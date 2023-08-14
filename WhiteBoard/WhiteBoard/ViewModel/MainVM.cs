using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteBoard.Messages;
using WhiteBoard.Model;
using WhiteBoard.Services.Interfaces;
using GalaSoft.MvvmLight.Command;

namespace WhiteBoard.ViewModel
{
    class MainVM : ViewModelBase
    {
        private ViewModelBase selectedPage;
        private readonly IMessenger _messenger;
        private readonly INavigateService _navigateService;
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

        public MainVM(IMessenger messenger, INavigateService navigateService)
        {
            User = new();
            CurrentViewModel = App.Container.GetInstance<LogVM>(); // Ставлю окно по умолчанию 

            _messenger = messenger;
            _navigateService = navigateService;
            _messenger.Register<NavigationMessage>(this, ReceiveMessage);
        }
    }
}
