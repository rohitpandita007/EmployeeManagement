using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Data
{
    public static class UserSeeder
    {
        public static void Seed(EmployeeDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var passwordHasher = new PasswordHasher<User>();

            var admin = new User
            {
                Username = "admin",
                Role = "Admin"
            };

            admin.PasswordHash = passwordHasher.HashPassword(
                admin,
                "admin123");

            var user = new User
            {
                Username = "user",
                Role = "User"
            };

            user.PasswordHash = passwordHasher.HashPassword(
                user,
                "user123");

            context.Users.Add(admin);
            context.Users.Add(user);

            context.SaveChanges();
        }
    }
}