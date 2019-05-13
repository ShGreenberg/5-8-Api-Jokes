using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _5_8_Api_Jokes.data
{
    public class Joke
    {
        public int Id { get; set; }
        [Key]
        public int DataId { get; set; }
        public string SetUp { get; set; }
        public string Punchline { get; set; }
        public IEnumerable<UsersLikedJokes> UsersLikedJokes { get; set; }
    }
}
