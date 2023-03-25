using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Launcher.Messages;
using Launcher.Model;
using Launcher.Services.Classes;
using Launcher.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.ViewModel
{
    class StoreVM : ViewModelBase
    {
        private readonly IMessenger _messenger;
        public ProjectModel ProjectModel { get; set; } = new();

        private ObservableCollection<ProjectModel> _searchResult = new();
        public ObservableCollection<ProjectModel> SearchResult
        {
            get => _searchResult;
            set
            {
                if (_searchResult != value)
                {
                    Set(ref _searchResult, value);
                }
            }
        }

        public StoreVM(IMessenger messenger)
        {
            _messenger = messenger;

            // Зарегистрируем, чтобы получить выбранный экземпляр модели проекта
            _messenger.Register<DataMessage>(this, message =>
            {
                ProjectModel = message.Data as ProjectModel;
            });
        }
    }
}
