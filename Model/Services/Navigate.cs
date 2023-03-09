using Admin.Model.Messages;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model.Services
{
    internal class Navigate : INavigateService
    {
        private readonly IMessenger _messenger;

        public Navigate(IMessenger messenger)
        {
            _messenger = messenger;
        }

        public void NavigateTo<T>() where T : ViewModelBase
        {
            _messenger.Send(new NavigationMessage()
            {
                VMType = typeof(T)
            });
        }
    }
}
