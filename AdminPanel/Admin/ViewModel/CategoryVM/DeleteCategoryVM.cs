using Admin.Model.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ViewModel.CategoryVM
{
    class DeleteCategoryVM : ViewModelBase
    {
        private readonly INavigateService _navigationService;
        public DeleteCategoryVM(INavigateService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
