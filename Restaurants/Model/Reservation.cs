using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Model
{
    public class Reservation
    {
        

        [Key]
        public int ReservationId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }


        // public List<MenuReservation> MenuItems { get; set; }
        // public List<MenuItem> MenuItems { get; set; }
    }
}
