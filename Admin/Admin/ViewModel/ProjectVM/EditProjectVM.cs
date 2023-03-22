using Admin.Model.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ViewModel.ProjectVM
{
    class EditProjectVM : ViewModelBase
    {
        private readonly INavigateService _navigationService;
        public EditProjectVM(INavigateService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
