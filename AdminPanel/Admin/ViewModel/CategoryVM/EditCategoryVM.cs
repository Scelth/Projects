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

namespace Admin.ViewModel.CategoryVM
{
    class EditCategoryVM : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly ISerializeService _serializeService;
        private readonly INavigateService _navigateService;
        public Data Data { get; set; } = new();

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

        public EditCategoryVM(IMessenger messenger, ISerializeService serializeService, INavigateService navigateService)
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
                string json = File.ReadAllText("Categories.json");
                ObservableCollection<CategoryModel> categories = _serializeService.Deserialize<ObservableCollection<CategoryModel>>(json);
                Data.Categories = new ObservableCollection<CategoryModel>(categories);
            }
        }

        public RelayCommand EditCommand
        {
            get => new(() =>
            {
                if (!File.Exists($"{CategoryModel.NewName}Category.json")) // Если файла категории не существует
                {
                    // Находим категорию по ее имени и обновляем свойство Name на новое значение
                    CategoryModel category = Data.Categories.FirstOrDefault(x => x.Name == CategoryModel.Name);
                    if (category != null)
                    {
                        string temp = category.Name;
                        category.Name = CategoryModel.NewName;

                        // Сериализуем список Data.Categories и записываем в файл Categories.json
                        string json = _serializeService.Serialize<Data>(Data.Categories);
                        File.WriteAllText("Categories.json", json);

                        // Переименовываем файл с данными категории
                        File.Move($"{temp}Category.json", $"{category.Name}Category.json");
                        MessageBox.Show($"{temp} category changed to {category.Name}");
                        _navigateService.NavigateTo<MenuVM>();
                    }
                }

                else
                {
                    MessageBox.Show($"{CategoryModel.NewName} category already exists");
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
