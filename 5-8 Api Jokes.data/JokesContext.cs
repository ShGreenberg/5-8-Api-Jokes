using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _5_8_Api_Jokes.data
{
    public class JokesContext: DbContext
    {
        private string _connString;
        public JokesContext(string connString)
        {
            _connString = connString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connString);
        }

        public DbSet<Joke> Jokes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersLikedJokes> UsersLikedJokes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<UsersLikedJokes>().HasKey(ulj => new { ulj.UserId, ulj.JokeId });

            modelBuilder.Entity<UsersLikedJokes>()
                .HasOne(ulj => ulj.Joke)
                .WithMany(j => j.UsersLikedJokes)
                .HasForeignKey(ulj => ulj.JokeId);

            modelBuilder.Entity<UsersLikedJokes>()
                .HasOne(ulj => ulj.User)
                .WithMany(u => u.UsersLikedJokes)
                .HasForeignKey(ulj => ulj.UserId);
        }
    }
}
