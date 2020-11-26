using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Model
{
    public class MenuItem
    {
        public MenuItem()
        {
        }

        [Key]
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public List<MenuReservation> ReservationList { get; set; }



    }
}
