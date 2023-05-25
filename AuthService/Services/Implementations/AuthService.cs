using AuthService.Database;
using AuthService.Database.Entities;
using AuthService.Models;
using AuthService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        public AuthService(AppDbContext db)
        {
            _db = db;
        }
        public bool CreateUser(User user, string Role)
        {
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                Role r = _db.Roles.Where(r => r.Name == Role).FirstOrDefault();
                user.Roles.Add(r);
                if (r != null)
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }



        }

        public IEnumerable<UserModel> GetUsers()
        {
            var data = _db.Users.Include(u => u.Roles).Select(u => new UserModel
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Roles = u.Roles.Select(r => r.Name).ToArray()
            }); 
            return data;
        }

        public UserModel ValidateUser(LoginModel model)
        {
            UserModel userModel = new UserModel();
            User data = _db.Users.Include(u=>u.Roles).Where(u=>u.Email == model.Email).FirstOrDefault();
            if (data != null)
            {
                bool isVerified = BCrypt.Net.BCrypt.Verify(model.Password, data.Password);
                if (isVerified)
                {
                    userModel.Id  = data.Id;
                    userModel.Email = data.Email;
                    userModel.Name  = data.Name;
                    userModel.Roles = data.Roles.Select(r => r.Name).ToArray();
                    userModel.Token = GenerateToken(userModel);
                    return userModel;
                }
              
            }
            return null;
        }

        private string GenerateToken(UserModel userModel)
        {
            return "token";
        }
    }
}
