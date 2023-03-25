﻿using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using Launcher.Messages;
using Launcher.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace Launcher.ViewModel
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
            CurrentViewModel = App.Container.GetInstance(message.VMType) as ViewModelBase;
        }

        public MainVM(IMessenger messenger, INavigateService navigateService)
        {
            CurrentViewModel = App.Container.GetInstance<StoreVM>(); // Ставлю окно по умолчанию 

            _messenger = messenger;
            _navigateService = navigateService;
            _messenger.Register<NavigationMessage>(this, ReceiveMessage);
        }

        public RelayCommand StoreCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<StoreVM>();
            });
        }
    }
}
