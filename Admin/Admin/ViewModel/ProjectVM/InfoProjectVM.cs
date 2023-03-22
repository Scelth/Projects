using Admin.Messages;
using Admin.Model;
using Admin.Model.Services;
using Admin.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ViewModel.ProjectVM
{
    internal class InfoProjectVM : ViewModelBase
    {
        public ProjectModel Project { get; set; } = new();

        private readonly IMessenger _messenger;
        private readonly INavigateService _navigationService;
        private readonly IDownloadService _downloadService;

        public InfoProjectVM(IMessenger messenger, INavigateService navigationService, IDownloadService downloadService)
        {
            _messenger = messenger;
            _navigationService = navigationService;
            _downloadService = downloadService;

            // Register to receive the selected ProjectModel instance
            _messenger.Register<DataMessage>(this, message =>
            {
                Project = message.Data as ProjectModel;
            });
        }

        public RelayCommand BackCommand
        {
            get => new(() =>
            {
                _navigationService.NavigateTo<SearchProjectVM>();
            });
        }

        public RelayCommand EditCommand
        {
            get => new(() =>
            {
                _navigationService.NavigateTo<EditProjectVM>();
            });
        }

        public RelayCommand DeleteCommand
        {
            get => new(() =>
            {
                var json = _downloadService.DownloadJson($"{Project.Category}Category.json");
                var projects = JsonConvert.DeserializeObject<List<ProjectModel>>(json);

                // Remove the selected project by name
                var projectToRemove = projects.FirstOrDefault(p => p.Name == Project.Name);
                if (projectToRemove != null)
                {
                    projects.Remove(projectToRemove);

                    // Write the updated projects back to the JSON file
                    json = JsonConvert.SerializeObject(projects);
                    File.WriteAllText($"{Project.Category}Category.json", json);

                    // Set the IsUserControlUpdated property to true
                    //IsUserControlUpdated = true;
                }

                // Navigate back to the search page
                _navigationService.NavigateTo<SearchProjectVM>();
            });
        }
    }
}