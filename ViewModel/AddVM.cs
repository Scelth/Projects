using Admin.Model.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ViewModel
{
    class AddVM : ViewModelBase
    {
        private readonly INavigateService _navigationService;
        public AddVM(INavigateService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
