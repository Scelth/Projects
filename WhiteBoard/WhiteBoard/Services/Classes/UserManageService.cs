using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteBoard.Context;
using WhiteBoard.Model;
using WhiteBoard.Services.Interfaces;

namespace WhiteBoard.Services.Classes
{
    public class UserManageService : IUserManageService
    {
        private readonly WhiteBoardDbContext _context;

        public UserManageService(WhiteBoardDbContext context)
        {
            _context = context;
        }

        public bool CheckExists(string username)
        {
            CheckService checkService = new();
            return checkService.CheckUserExist(username);
        }

        public void Add(UsersModel user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public UsersModel GetUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
