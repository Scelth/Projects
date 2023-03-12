using Admin.Model;
using Admin.Model.Services;
using Admin.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Admin.ViewModel
{
    class AddVM : ViewModelBase
    {
        private ProjectModel ProjectModel { get; set; } = new();

        private readonly INavigateService _navigateService;
        private readonly IProjectManageService _projectService;
        public AddVM(INavigateService navigationService, IProjectManageService projectService)
        {
            _navigateService = navigationService;
            _projectService = projectService;
        }

        public RelayCommand AddCommand
        {
            get => new(() =>
            {
                if (_projectService.CheckExists(ProjectModel.Name, ProjectModel.ReleaseDate))
                {
                    _projectService.Add(ProjectModel);
                    MessageBox.Show("Ok");
                    _navigateService.NavigateTo<MenuVM>();
                }

                else
                {
                    MessageBox.Show("No");
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