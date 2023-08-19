using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WhiteBoard.Model;

namespace WhiteBoard.Services.Interfaces
{
    internal interface ISaveService
    {
        public void SaveCommand(InkCanvas inkCanvas, string ImgName, UsersModel Users);
        public void SaveAsCommand(InkCanvas inkCanvas, string ImgName, UsersModel Users);
    }
}