using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using _5_8_Api_Jokes.data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace _5_8_Api_Jokes.web.Controllers
{
    public class AccountController : Controller
    {
        private string _connString;
        public AccountController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user, string password)
        {
            JokesRepository rep = new JokesRepository(_connString);
            user.PasswordHash = PasswordHelper.HashPassword(password);
            rep.AddUser(user);
            return Redirect("/account/login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            JokesRepository rep = new JokesRepository(_connString);
            User user = rep.Login(email, password);
            if(user == null)
            {
                return Redirect("/account/login");
            }

            var claims = new List<Claim>
            {
                new Claim("user", email)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            return Redirect("/");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/");
        }
    }
}