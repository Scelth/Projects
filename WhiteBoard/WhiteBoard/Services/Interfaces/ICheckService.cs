using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteBoard.Services.Interfaces
{
    internal interface ICheckService
    {
        public bool CheckUserExist(string Username);
    }
}