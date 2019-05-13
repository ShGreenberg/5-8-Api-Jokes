using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace _5_8_Api_Jokes.data
{
    public class JokesApi
    {
        public Joke GetJoke()
        {
            using(var client = new HttpClient())
            {
                var json = client.GetStringAsync("https://official-joke-api.appspot.com/jokes/programming/random").Result;
                var jokes = JsonConvert.DeserializeObject<List<Joke>>(json);
                return jokes[0];
            }
        }
    }
}
