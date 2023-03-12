using Admin.Model.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ViewModel
{
    class EditVM : ViewModelBase
    {
        private readonly INavigateService _navigationService;
        public EditVM(INavigateService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
