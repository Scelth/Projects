using Admin.Model;
using Admin.Model.Services;
using Admin.Services.Classes;
using Admin.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignColors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Admin.ViewModel.ProjectVM
{
    class EditProjectVM : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly ISerializeService _serializeService;
        private readonly INavigateService _navigateService;
        private readonly IDownloadService _downloadService;
        //public string? SearchBar { get; set; }
        //public ObservableCollection<ProjectModel> SearchResult { get; set; } = new();
        public Data Data { get; set; } = new();

        public ProjectModel ProjectModel { get; set; } = new();

        private CategoryModel _categoryModel = new();
        public CategoryModel CategoryModel
        {
            get => _categoryModel;

            set
            {
                if (_categoryModel != value)
                {
                    Set(ref _categoryModel, value);
                }
            }
        }

        private ObservableCollection<ProjectModel> _searchResult = new();
        public ObservableCollection<ProjectModel> SearchResult
        {
            get => _searchResult;
            set
            {
                _searchResult = value;
                RaisePropertyChanged();
            }
        }

        public EditProjectVM(INavigateService navigationService, IMessenger messenger, ISerializeService serializeService, IDownloadService downloadService)
        {
            _messenger = messenger;
            _serializeService = serializeService;
            _navigateService = navigationService;
            _downloadService = downloadService;
        }

        private string _selectedCategoryName;
        public string SelectedCategoryName
        {
            get => _selectedCategoryName;
            set
            {
                if (Set(ref _selectedCategoryName, value))
                {
                    SearchCommand.RaiseCanExecuteChanged();
                }
            }
        }

        //private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            //get
            //{
            //    return _searchCommand ?? (_searchCommand = new RelayCommand(
            //        () =>
            //        {
            //            SearchResult = new ObservableCollection<ProjectModel>(Data.Projects.Where(p => p.Category == SelectedCategoryName));
            //        },
            //        () => !string.IsNullOrEmpty(SelectedCategoryName)));
            //}
            
            get => new(() =>
            {


                //StreamReader filePath = new($"{SelectedCategoryName}Category.json");
                //string json = filePath.ReadToEnd();
                //var categoryData = JsonConvert.DeserializeObject(json);
                //SearchResult.Add(categoryData);
            });
        }
    }
}
