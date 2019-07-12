using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FindThemAPI.Models
{
    public class FindThemContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connection = @"Server=DESKTOP-NAR4HMR;Database=FindThemDB;Trusted_Connection=True;ConnectRetryCount=0";    
            optionsBuilder.UseSqlServer(Configuration.stringConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasOne<User>(a => a.user);

            modelBuilder.Entity<Provider>()
                .HasOne(b => b.user);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}
