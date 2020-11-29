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
        public IEnumerable<ReservationViewModel> Get()
        {
            _logger.LogInformation("The Get MenuItem was invoked!");
            /*
           _logger.LogWarning("This is Warning!");

           _logger.LogError("This is Error");
           _logger.LogCritical("This is something critical!!!");
          */
            

            List<ReservationViewModel> list = _context

              .MenuReservation

              .Select(r => r.Reservation)

              .Distinct()

              .Select(r => new ReservationViewModel
              {

                  Name = r.Name,

                  Date = r.Date,

                  MenuItems = _context

                      .MenuReservation

                      .Where(rmi => rmi.ReservationId == r.ReservationId)

                      .Select(rmi => rmi.MenuItem)

                      .ToList()

              })

              .ToList();

            return (IEnumerable<ReservationViewModel>)list;
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public ReservationViewModel Get(int id)
        {
            var result = _context

             .MenuReservation

             .Select(r => r.Reservation)

             .Distinct()
             .Where(r => r.ReservationId == id)

             .Select(r => new ReservationViewModel
             {

                 Name = r.Name,

                 Date = r.Date,

                 MenuItems = _context

                     .MenuReservation

                     .Where(rmi => rmi.ReservationId == r.ReservationId)

                     .Select(rmi => rmi.MenuItem)

                     .ToList()

             });

            return (ReservationViewModel)result;
        }

        // POST: api/Reservations
        [HttpPost]
        public void Post([FromBody] ReservationViewModel value)
        {
            try
            {
                //add new reservation
                var lastId =  _context.Reservations.AsEnumerable().Last().ReservationId + 1;
                var newReservation = new Reservation
                {
                    ReservationId = lastId,
                    Name = value.Name,
                    Date = value.Date
                };
                _context.Reservations.Add(newReservation);
                
                //add a list of items to the new reservation bridge relations
                for (int i = 0; i < value.MenuItems.Count(); i++)
                {
                    
                    var item = new MenuReservation
                    {
                        ID = _context.MenuReservation.AsEnumerable().Last().ID + 1,
                        ReservationId = newReservation.ReservationId,
                        MenuId = value.MenuItems[i].MenuId
                    };
                    _context.MenuReservation.Add(item);
                    
                }
                
                _context.SaveChanges();
            }
            catch (Exception ex) {
                // ex.Message;
                _logger.LogError(ex.Message);
            }
           
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ReservationViewModel value)
        {
            ReservationViewModel result = (ReservationViewModel)_context

            .MenuReservation

            .Select(r => r.Reservation)

            .Distinct()
            .Where(r => r.ReservationId == id)

            .Select(r => new ReservationViewModel
            {

                Name = r.Name,

                Date = r.Date,

                MenuItems = _context

                    .MenuReservation

                    .Where(rmi => rmi.ReservationId == r.ReservationId)

                    .Select(rmi => rmi.MenuItem)

                    .ToList()

            });

            if (result != null)
            {
                result.Name = value.Name;
                result.Date = value.Date;
                _context.SaveChanges();

            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //TBD > implement the deletion of reservation and deletion of the coresponding menuitems
            
            Reservation re = _context.Reservations.Find(id);   //find reservation by ID
            _context.Reservations.Remove(re);
            _context.SaveChanges();
            
        }
    

    }

   
}
