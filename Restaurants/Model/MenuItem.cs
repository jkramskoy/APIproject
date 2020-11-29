using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Model
{
    public class MenuItem
    {
        [Key]
        public int MenuId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
    }
}
