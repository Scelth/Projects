using System.Linq;
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

        public void SaveUserToKeep(int userID)
        {
            using (var dbContext = new WhiteBoardDbContext())
            {
                var keepModel = new KeepModel
                {
                    UserID = userID
                };

                dbContext.Keep.Add(keepModel);
                dbContext.SaveChanges();
            }
        }

        public bool HasKeepUser()
        {
            return _context.Keep.Any();
        }

        public int GetKeepUserId()
        {
            var keeper = _context.Keep.FirstOrDefault();
            return keeper?.UserID ?? -1; // Возвращает -1 если нет записи в Keepers
        }

        public UsersModel GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.ID == userId);
        }
    }
}