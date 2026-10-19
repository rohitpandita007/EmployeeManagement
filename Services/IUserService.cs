using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public interface IUserService
    {
        User? ValidateUser(string username, string password);

        List<User> GetAllUsers();

        User? GetUserById(int id);

        bool UsernameExists(string username);

        void AddUser(User user, string password);

        void UpdateUser(User user);

        void DeleteUser(int id);

        


    }
}