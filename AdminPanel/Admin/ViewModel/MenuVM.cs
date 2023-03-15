using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Windows.Controls;
using System.Windows;
using Admin.Services.Interfaces;
using Admin.Model.Services;

namespace Admin.ViewModel
{
    internal class MenuVM : ViewModelBase
    {
        private readonly INavigateService _navigateService;
        private readonly IProjectManageService _projectManageService;

        public MenuVM(INavigateService navigateService, IProjectManageService projectManageService)
        {
            _navigateService = navigateService;
            _projectManageService = projectManageService;
        }

        public RelayCommand AddCategoryCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<AddCategoryVM>();
            });
        }

        public RelayCommand EditCategoryCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<EditCategoryVM>();
            });
        }

        public RelayCommand DeleteCategoryCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<DeleteCategoryVM>();
            });
        }


        public RelayCommand AddProjectCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<AddProjectVM>();
            });
        }

        public RelayCommand EditProjectCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<EditProjectVM>();
            });
        }

        public RelayCommand DeleteProjectCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<DeleteProjectVM>();
            });
        }
    }
}