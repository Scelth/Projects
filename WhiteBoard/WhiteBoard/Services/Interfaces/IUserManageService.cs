using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteBoard.Model;

namespace WhiteBoard.Services.Interfaces
{
    interface IUserManageService
    {
        public void Add(UsersModel user);
        UsersModel GetUser(string username, string password);
        public bool CheckExists(string username);
    }
}
