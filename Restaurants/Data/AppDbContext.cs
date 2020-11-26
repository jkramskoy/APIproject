using System;
using Microsoft.EntityFrameworkCore;
using Restaurants.Model;

namespace Restaurants.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<MenuReservation> MenuReservation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"server=localhost;user=root;password=amy251202;database=Restaurants");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
