using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();

            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(User user, string password)
        {
            if (_userService.UsernameExists(user.Username))
            {
                ModelState.AddModelError(
                    "Username",
                    "This username already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            _userService.AddUser(user, password);

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _userService.DeleteUser(id);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost]
        public IActionResult Edit(User user)
        {
            var existingUser = _userService.GetUserById(user.Id);

            if (existingUser == null)
            {
                return NotFound();
            }

            if (_userService.UsernameExists(user.Username)
                && existingUser.Username != user.Username)
            {
                ModelState.AddModelError(
                    "Username",
                    "This username already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            _userService.UpdateUser(user);

            return RedirectToAction("Index");
        }



    }
}