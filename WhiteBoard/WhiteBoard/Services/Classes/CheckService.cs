using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WhiteBoard.Context;
using WhiteBoard.Services.Interfaces;

namespace WhiteBoard.Services.Classes
{
    public class CheckService : ICheckService
    {
        public bool CheckUserExist(string Username)
        {
            WhiteBoardDbContext context = new();
            var check = context.Users.Where(x => x.Username == Username).Select(x => x.Username);
            foreach (var item in check)
            {
                if (item == Username)
                {
                    MessageBox.Show($"{item} already exist");
                    return false;
                }
            }

            return true;
        }
    }
}