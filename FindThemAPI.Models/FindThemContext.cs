using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FindThemAPI.Models
{
    public class FindThemContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connection = @"Server=DESKTOP-NAR4HMR;Database=FindThemDB;Trusted_Connection=True;ConnectRetryCount=0";
            var connection = @"Data Source=DESKTOP-NAR4HMR;Initial Catalog=FindThemDB;persist security info=True;user id=sa;password=12345678";
            optionsBuilder.UseSqlServer(connection);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}
