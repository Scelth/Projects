using Admin.Messages;
using Admin.Model;
using Admin.Model.Services;
using Admin.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Admin.ViewModel.CategoryVM
{
    class AddCategoryVM : ViewModelBase
    {
        private readonly IMessenger _messenger;
        //private readonly ISerializeService _serializeService;
        private readonly INavigateService _navigateService;

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
        public AddCategoryVM(IMessenger messenger/*, ISerializeService serializeService*/, INavigateService navigateService)
        {
            _navigateService = navigateService;
            _messenger = messenger;
            //_serializeService = serializeService;

            _messenger.Register<DataMessage>(this, messenger =>
            {
                Data.Categories = messenger.Data as ObservableCollection<CategoryModel>;
            });
        }

        public RelayCommand AddCommand
        {
            get => new(() =>
            {
                CategoryModel Category = new()
                {
                    Name = CategoryModel.Name
                };

                Data.Categories.Add(Category);
                string? json = JsonConvert.SerializeObject(Data.Categories);
                using FileStream fs = new("Categories.json", FileMode.OpenOrCreate);
                using StreamWriter sw = new(fs);
                sw.Write(json);

                string? json1 = JsonConvert.SerializeObject(CategoryModel);
                using FileStream fs1 = new($"{CategoryModel.Name}Category.json", FileMode.OpenOrCreate);
                using StreamWriter sw1 = new(fs1);
                sw.Write(json1);
                MessageBox.Show($"{CategoryModel.Name}Category.json added");
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
