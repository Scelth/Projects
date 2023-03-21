using Admin.Messages;
using Admin.Model;
using Admin.Model.Services;
using Admin.Services.Classes;
using Admin.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Admin.ViewModel.CategoryVM
{
    class DeleteCategoryVM : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly ISerializeService _serializeService;
        private readonly INavigateService _navigateService;
        public Data Data { get; set; } = new();
        public CategoryModel categoryModel { get; set; } = new();

        private CategoryModel _categorytModel = new();
        public CategoryModel CategoryModel
        {
            get => _categorytModel;

            set
            {
                if (_categorytModel != value)
                {
                    Set(ref _categorytModel, value);
                }
            }
        }

        public DeleteCategoryVM(INavigateService navigateService, ISerializeService serializeService, IMessenger messenger)
        {
            _navigateService = navigateService;
            _messenger = messenger;
            _serializeService = serializeService;

            _messenger.Register<DataMessage>(this, messenger =>
            {
                Data.Categories = messenger.Data as ObservableCollection<CategoryModel>;
            });

            if (File.Exists("Categories.json"))
            {
                StreamReader file = new("Categories.json");
                string json = file.ReadToEnd();
                ObservableCollection<CategoryModel> categories = _serializeService.Deserialize<ObservableCollection<CategoryModel>>(json);
                Data.Categories = new ObservableCollection<CategoryModel>(categories);
            }
        }

        public RelayCommand DeleteCommand
        {
            get => new(() =>
            {
                var categoryToRemove = Data.Categories.FirstOrDefault(x => x.Name == CategoryModel.Name);
                if (categoryToRemove != null)
                {
                    File.Delete($"{CategoryModel.Name}Category.json");
                    MessageBox.Show($"{CategoryModel.Name} category deleted");
                    Data.Categories.Remove(categoryToRemove);
                    string? json = _serializeService.Serialize<Data>(Data.Categories);
                    File.WriteAllText("Categories.json", json);
                }
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
