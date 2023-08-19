using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteBoard.Model;

namespace WhiteBoard.Services.Interfaces
{
    public interface IUserManageService
    {
        public void Add(UsersModel user);
        UsersModel GetUser(string username, string password);
        public bool CheckExists(string username);
        public void SaveUserToKeep(int userID);
        bool HasKeepUser();
        int GetKeepUserId();
        UsersModel GetUserById(int userId);
    }
}
