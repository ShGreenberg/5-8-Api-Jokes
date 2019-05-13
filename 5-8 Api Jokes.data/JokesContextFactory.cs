using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _5_8_Api_Jokes.data
{
    public class JokesContextFactory : IDesignTimeDbContextFactory<JokesContext>
    {
        public JokesContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}5-8 Api Jokes.web"))
                 .AddJsonFile("appsettings.json")
                 .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();
            return new JokesContext(config.GetConnectionString("ConStr"));
        }
    }
}
