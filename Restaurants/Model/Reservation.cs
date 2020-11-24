using System;
using System.Collections.Generic;

namespace Restaurants.Model
{
    public class Reservation
    {
        public Reservation()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public List<MenuItem> MenuItems { get; set; }
    }
}
