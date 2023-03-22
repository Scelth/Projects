using Admin.Messages;
using Admin.Model;
using Admin.Model.Services;
using Admin.Services.Classes;
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
        private readonly ISerializeService _serializeService;
        private readonly INavigateService _navigateService;
        private readonly ICheckService _checkService;

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
        public AddCategoryVM(IMessenger messenger, ISerializeService serializeService, INavigateService navigateService, ICheckService checkService)
        {
            _navigateService = navigateService;
            _messenger = messenger;
            _serializeService = serializeService;
            _checkService = checkService;

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

                if (_checkService.CkeckCategoryExist(Category)) //Существует ли такая категория
                {
                    Data.Categories.Add(Category);
                    string json = _serializeService.Serialize<Data>(Data.Categories);
                    using FileStream fs = new("Categories.json", FileMode.OpenOrCreate);
                    using StreamWriter sw = new(fs);
                    sw.Write(json);

                    using FileStream fs1 = new($"{CategoryModel.Name}Category.json", FileMode.Create);
                    using StreamWriter sw1 = new(fs1);
                    MessageBox.Show($"{CategoryModel.Name} category added");
                }
            });
        }
    }
}
