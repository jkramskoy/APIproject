using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.Model
{
    [Table("MenuReservation")]
    public class MenuReservation
    {
       
        public int ID { get; set; }
        
        public int ReservationId { get; set; }
        public int MenuId { get; set; }

        public MenuItem MenuItem { get; set; }
        public Reservation Reservation { get; set; }

    }
}
