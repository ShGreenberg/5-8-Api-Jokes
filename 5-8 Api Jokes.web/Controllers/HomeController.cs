using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _5_8_Api_Jokes.web.Models;
using _5_8_Api_Jokes.data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace _5_8_Api_Jokes.web.Controllers
{
    public class HomeController : Controller
    {
        private string _connString;
        public HomeController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            JokesRepository rep = new JokesRepository(_connString);
            Joke joke = rep.GetJoke();
            return View(joke);
        }

        public IActionResult AllJokes()
        {
            JokesRepository rep = new JokesRepository(_connString);
            List<Joke> jokes = rep.GetJokes().ToList();
            return View(rep.GetJokes());
        }

       
        public IActionResult LikeJoke(int jokeId, bool like)
        {
            if(User.Identity.Name == null)
            {
                return Json(new { result = "Redirect", url = Url.Action("Login", "Account") });
            }
            JokesRepository rep = new JokesRepository(_connString);
            rep.LikeJoke(jokeId, User.Identity.Name, like);
            return Json("done");
        }

        public IActionResult GetLikes(int jokeId)
        {
            JokesRepository rep = new JokesRepository(_connString);
            List<UsersLikedJokes> like = rep.GetLikes(jokeId);
            return Json(like.Count);
        }

        
    }
}
