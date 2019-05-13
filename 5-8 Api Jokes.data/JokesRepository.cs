using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _5_8_Api_Jokes.data
{
    public class JokesRepository
    {
        private string _connString;
        public JokesRepository(string connString)
        {
            _connString = connString;
        }

        //users
        public void AddUser(User user)
        {
            using (var ctx = new JokesContext(_connString))
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }

        public User Login(string email, string password)
        {
            using (var ctx = new JokesContext(_connString))
            {
                User user = ctx.Users.FirstOrDefault(u => u.Email == email);
                if (user == null)
                {
                    return null;
                }
                if (PasswordHelper.PasswordMatch(password, user.PasswordHash))
                {
                    return user;
                }
                return null;
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var ctx = new JokesContext(_connString))
            {
                return ctx.Users.FirstOrDefault(u => u.Email == email);
            }
        }

        //jokes
        private void AddJoke(Joke joke)
        {
            using (var ctx = new JokesContext(_connString))
            {
                IEnumerable<Joke> jokes = ctx.Jokes;
                if(!jokes.Any(j => j.Id == joke.Id))
                {
                    ctx.Jokes.Add(joke);
                    ctx.SaveChanges();
                } 
            }
        }

        public Joke GetJoke()
        {
            JokesApi api = new JokesApi();
            Joke joke = api.GetJoke();
            AddJoke(joke);
            using(var ctx = new JokesContext(_connString))
            {
                joke = ctx.Jokes.Include(j => j.UsersLikedJokes).ThenInclude(ulj => ulj.User)
                    .FirstOrDefault(j => j.Id == joke.Id);
            }
            return joke;
        }

        public IEnumerable<Joke> GetJokes()
        {
            using (var ctx = new JokesContext(_connString))
            {
                return ctx.Jokes.Include(j => j.UsersLikedJokes).ThenInclude(ulj => ulj.User).ToList();
            }
        }

        public void LikeJoke(int jokeId, string userName, bool like)
        {
            User user = GetUserByEmail(userName);
            using (var ctx = new JokesContext(_connString))
            {
                if(ctx.UsersLikedJokes.Any(l => l.JokeId == jokeId && l.UserId == user.Id))
                {
                    UpdateLike(jokeId, user.Id, like);
                }
                else
                {
                    UsersLikedJokes ulj = new UsersLikedJokes
                    {

                        JokeId = jokeId,
                        Liked = like,
                        Date = DateTime.Now,
                        UserId = user.Id
                    };
                    ctx.UsersLikedJokes.Add(ulj);
                    ctx.SaveChanges();
                }
              
            }
        }

        private void UpdateLike(int jokeId, int userId, bool like)
        {
            using (var ctx = new JokesContext(_connString))
            {
                ctx.Database.ExecuteSqlCommand("UPDATE UsersLikedJokes SET Liked = @like WHERE jokeid = @jokeid" +
                    " AND UserId = @userid",
                   new SqlParameter("@like", like), new SqlParameter("@jokeid", jokeId), new SqlParameter("@userid", userId));
            }
        }

        public List<UsersLikedJokes> GetLikes(int jokeId)
        {
            using (var ctx = new JokesContext(_connString))
            {
                return ctx.UsersLikedJokes.Where(usl => usl.JokeId == jokeId && usl.Liked == true).ToList();
            }
        }
    }
}
