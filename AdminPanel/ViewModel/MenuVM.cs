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

        public RelayCommand AddButtonCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<AddVM>();
            });
        }

        public RelayCommand EditButtonCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<EditVM>();
            });
        }

        public RelayCommand DeleteButtonCommand
        {
            get => new(() =>
            {
                _navigateService.NavigateTo<DeleteVM>();
            });
        }
    }
}
