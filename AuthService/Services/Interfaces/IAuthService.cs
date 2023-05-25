using AuthService.Database.Entities;
using AuthService.Models;

namespace AuthService.Services.Interfaces
{
    public interface IAuthService
    {
        IEnumerable<UserModel> GetUsers();
        bool CreateUser(User user, string Role);

        UserModel ValidateUser(LoginModel model);
    }
}
