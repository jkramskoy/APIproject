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
            //return _context.Reservations.ToList();

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

             })
             .FirstOrDefault();

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
                //for (int i = 0; i < value.MenuItems.Count(); i++)
                //{

                //    var item = new MenuReservation
                //    {
                //        ID = _context.MenuReservation.AsEnumerable().Last().ID + 1,
                //        ReservationId = newReservation.ReservationId,
                //        MenuId = value.MenuItems[i].MenuId
                //    };
                //    _context.MenuReservation.Add(item);

                //}

                foreach (MenuItem mi in value.MenuItems)
                {
                    MenuReservation rmi = new MenuReservation
                    {
                        MenuId = mi.MenuId,
                        ReservationId = newReservation.ReservationId
                    };
                    _context.MenuReservation.Add(rmi);
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
            /*
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
            */
            Reservation reservation = _context
                .Reservations
                .Where(r => r.ReservationId == id)
                .FirstOrDefault();

            if (reservation != null)
            {
                reservation.Name = value.Name;
                reservation.Date = value.Date;

                var rangeMI = _context.MenuReservation
                    .Where(rmi => rmi.ReservationId == id).ToList();
                _context.MenuReservation.RemoveRange(rangeMI);

                foreach (MenuItem mi in value.MenuItems)
                {
                    MenuReservation rmi = new MenuReservation
                    {
                        MenuId = mi.MenuId,
                        ReservationId = id
                    };
                    _context.MenuReservation.Add(rmi);
                }

                _context.SaveChanges();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //TBD > implement the deletion of reservation and deletion of the coresponding menuitems
            
            Reservation re = _context.Reservations.Find(id);   //find reservation by ID

            if (re !=null)
            {
                var rangeMI = _context.MenuReservation
                    .Where(rmi => rmi.ReservationId == id).ToList();

                _context.MenuReservation.RemoveRange(rangeMI);
                _context.Reservations.Remove(re);
                _context.SaveChanges();

            }


        }
    

    }

   
}
