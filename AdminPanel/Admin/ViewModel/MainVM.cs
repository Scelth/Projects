using Admin.Model.Messages;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Admin.Model.Services;

namespace Admin.ViewModel
{
    class MainVM : ViewModelBase
    {
        private ViewModelBase selectedPage;
        private readonly IMessenger _messenger;
        private readonly INavigateService _navigateService;

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
            CurrentViewModel = App.container.GetInstance(message.VMType) as ViewModelBase;
        }

        public MainVM(IMessenger messenger)
        {
            CurrentViewModel = App.container.GetInstance<MenuVM>(); // Ставлю окно по умолчанию 

            _messenger = messenger;
            _messenger.Register<NavigationMessage>(this, ReceiveMessage);
        }
    }
}