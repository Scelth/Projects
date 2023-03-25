using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using Launcher.Messages;
using Launcher.Model;
using Launcher.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Launcher.Services.Classes;
using System.IO;
using System.Windows;

namespace Launcher.ViewModel
{
    class InfoVM : ViewModelBase
    {
        public ProjectModel Project { get; set; } = new();

        private readonly IMessenger _messenger;
        private readonly INavigateService _navigateService;
        private readonly IDownloadService _downloadService;
        private readonly ISerializeService _serializeService;

        public InfoVM(IMessenger messenger, INavigateService navigationService, IDownloadService downloadService, ISerializeService serializeService)
        {
            _messenger = messenger;
            _navigateService = navigationService;
            _downloadService = downloadService;
            _serializeService = serializeService;

            // Зарегистрируем, чтобы получить выбранный экземпляр модели проекта
            _messenger.Register<DataMessage>(this, message =>
            {
                Project = message.Data as ProjectModel;
            });
        }

        public RelayCommand CartCommand
        {
            get => new(() =>
            {
                Data.Projects.Add(Project);
                string json = _serializeService.Serialize<Data>(Data.Projects);

                if (string.IsNullOrEmpty(json))
                {
                    json = "[]";
                }

                using FileStream fs = new("Cart.json", FileMode.OpenOrCreate);
                using StreamWriter sw = new(fs);
                sw.Write(json);

                MessageBox.Show($"{Project.Name} has been added to the cart", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }
    }
}