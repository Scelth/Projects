using Admin.Messages;
using Admin.Model;
using Admin.Model.Services;
using Admin.Services.Interfaces;
using Admin.ViewModel.CategoryVM;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Admin.ViewModel.ProjectVM
{
    class AddProjectVM : ViewModelBase
    {
        //public ObservableCollection<ProjectModel> Projects { get; set; } = new();
        //private ProjectModel ProjectModel { get; set; } = new();
        public Data Data { get; set; } = new();

        private readonly IMessenger _messenger;
        private readonly ISerializeService _serializeService;
        private readonly INavigateService _navigateService;

        private ProjectModel _projectModel = new();
        public ProjectModel ProjectModel
        {
            get => _projectModel;

            set
            {
                if (_projectModel != value)
                {
                    Set(ref _projectModel, value);
                }
            }
        }

        public AddProjectVM(IMessenger messenger, ISerializeService serializeService, INavigateService navigateService)
        {
            _navigateService = navigateService;
            _messenger = messenger;
            _serializeService = serializeService;

            _messenger.Register<DataMessage>(this, messenger =>
            {
                Data.Projects = messenger.Data as ObservableCollection<ProjectModel>;
            });

            if (File.Exists("Categories.json"))
            {
                string json = File.ReadAllText("Categories.json");
                ObservableCollection<CategoryModel> categories = _serializeService.Deserialize<ObservableCollection<CategoryModel>>(json);
                Data.Categories = new ObservableCollection<CategoryModel>(categories);
            }
        }

        public RelayCommand AddCommand
        {
            get => new(() =>
            {

                ProjectModel Project = new()
                {
                    Name = ProjectModel.Name,
                    Studio = ProjectModel.Studio,
                    ReleaseDate = ProjectModel.ReleaseDate,
                    Price = ProjectModel.Price,
                    Poster = ProjectModel.Poster,
                    Description = ProjectModel.Description,
                    Category = ProjectModel.Category
                };

                Data.Projects.Add(Project);
                string? json = _serializeService.Serialize<Data>(Data.Projects);
                using FileStream fs = new($"{ProjectModel.Category}Category.json", FileMode.Open);
                using StreamWriter sw = new(fs);
                sw.Write(json);
                MessageBox.Show($"{Project.Name} has been added to the {Project.Category} category");
            });
        }

        public RelayCommand BackCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<MenuVM>();
            });
        }
    }
}