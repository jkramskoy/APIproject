using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurants.Data;
using Restaurants.Model;

namespace Restaurants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();
        private readonly ILogger<ReservationsController> _logger;

        public ReservationsController(ILogger<ReservationsController> logger)
        {
            _logger = logger;
        }

        // GET: api/Rservations
        [HttpGet]
        public IEnumerable<Reservation> Get()
        {
            _logger.LogInformation("The Get MenuItem was invoked!");
            /*
           _logger.LogWarning("This is Warning!");

           _logger.LogError("This is Error");
           _logger.LogCritical("This is something critical!!!");
          */
            return _context.Reservations.ToList();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public Reservation Get(int id)
        {
            return _context.Reservations.Find(id);
        }

        // POST: api/Reservations
        [HttpPost]
        public void Post([FromBody] Reservation value)
        {
            value.ReservationId = _context.Reservations.AsEnumerable().Last().ReservationId + 1;
            _context.Reservations.Add(value);
            _context.SaveChanges();
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Reservation value)
        {
            Reservation re = _context.Reservations.Where(r => r.ReservationId == id).FirstOrDefault(); // find reservation by ID

            if (re != null)
            {
                re.Name = value.Name;
                re.Date = value.Date;
                _context.SaveChanges();

            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Reservation re = _context.Reservations.Find(id);   //find reservation by ID
            _context.Reservations.Remove(re);
            _context.SaveChanges();
        }
    

    }

   
}
