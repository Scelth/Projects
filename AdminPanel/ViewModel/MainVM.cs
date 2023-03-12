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
        //private ViewModelBase selectedPage;
        //private readonly IMessenger _messenger;
        //private readonly INavigateService _navigateService;

        //public MainVM(IMessenger messenger, INavigateService navigateService)
        //{
        //    _messenger = messenger;
        //    _messenger.Register<NavigationMessage>(this, ReceiveMessage);
        //    _navigateService = navigateService;
        //}

        //public ViewModelBase SelectedPage
        //{
        //    get
        //    {
        //        return selectedPage;
        //    }

        //    set
        //    {
        //        if (selectedPage != value)
        //        {
        //            Set(ref selectedPage, value);
        //        }
        //    }
        //}

        //public RelayCommand AddButtonCommand
        //{
        //    get => new(() =>
        //    {
        //        _navigateService.NavigateTo<AddVM>();
        //    });
        //}

        //public RelayCommand EditButtonCommand
        //{
        //    get => new(() =>
        //    {
        //        _navigateService.NavigateTo<EditVM>();
        //    });
        //}

        //public RelayCommand DeleteButtonCommand
        //{
        //    get => new(() =>
        //    {
        //        _navigateService.NavigateTo<DeleteVM>();
        //    });
        //}

        //public void ReceiveMessage(NavigationMessage message)
        //{
        //    SelectedPage = App.container.GetInstance(message.VMType) as ViewModelBase;
        //}

        private ViewModelBase selectedPage;
        private readonly IMessenger _messenger;
        private readonly INavigateService _navigateService;

        public ViewModelBase CurrentViewModel
        {
            get => selectedPage;
            set
            {
                Set(ref selectedPage, value); // Функция Set() автоматически вызывает PropertyChanged.
            }
        }

        public void ReceiveMessage(NavigationMessage message)
        {
            CurrentViewModel = App.container.GetInstance(message.VMType) as ViewModelBase;
        }

        public MainVM(IMessenger messenger) // constructor
        {
            CurrentViewModel = App.container.GetInstance<MenuVM>(); // Ставлю окно по умолчанию 

            _messenger = messenger;

            // Register - регистрация класса на сообщение какого-то типа 
            // параметр this - говорит о том, кого именно регистрировать.
            // this - это объект MainViewModel созданный SimpleInjector-ом в App.Container
            // ReceiveMessage - Функция, которая запускается после принятия сообщения и принимает в параметрах это сообщение
            _messenger.Register<NavigationMessage>(this, ReceiveMessage);
        }
    }
}
