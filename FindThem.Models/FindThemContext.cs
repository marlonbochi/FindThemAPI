using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

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
                .Property<Int64>("userID");

            modelBuilder.Entity<Provider>()
                .Property<Int64>("userID");

            modelBuilder.Entity<Service>()
                .Property<Int64>("providerID");

            modelBuilder.Entity<Payment>()
                .Property<Int64>("requestID");

            modelBuilder.Entity<Message>()
                .Property<Int64>("requestID");

            modelBuilder.Entity<Log>()
                .Property<Int64>("userID");

            modelBuilder.Entity<Request>()
                .Property<Int64>("clientID");

            modelBuilder.Entity<Request>()
                .Property<Int64>("providerID");

            modelBuilder.Entity<Request>()
                .Property<Int64>("serviceID");

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
