using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FindThem.Models
{
    public class FindThemContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if (!DEBUG)
            optionsBuilder.UseMySql("Database=localdb;Port=49936;Server=127.0.0.1;Uid=azure;Pwd=6#vWHD_$");
#endif
#if (DEBUG)
            optionsBuilder.UseMySql("Server=findthem-database;DataBase=findthem_bd;Uid=root;Pwd=");
#endif
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
        public DbSet<Service> Services { get; set; }
    }
}
