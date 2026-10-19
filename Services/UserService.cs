using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Services
{
    public class UserService : IUserService
    {
        private readonly EmployeeDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(EmployeeDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public User? ValidateUser(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password);

            if (result == PasswordVerificationResult.Success)
            {
                return user;
            }

            return null;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUserById(int id)
        {
            return _context.Users
                .FirstOrDefault(u => u.Id == id);
        }


        public void AddUser(User user, string password)
        {
            user.PasswordHash = _passwordHasher.HashPassword(
                user,
                password);

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool UsernameExists(string username)
        {
            return _context.Users.Any(
                u => u.Username == username);
        }


        public void DeleteUser(int id)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return;
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
        }


        public void UpdateUser(User user)
        {
            var existingUser = _context.Users
                .FirstOrDefault(u => u.Id == user.Id);

            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Role = user.Role;

                _context.SaveChanges();
            }
        }
    }
}