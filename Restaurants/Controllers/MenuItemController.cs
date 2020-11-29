using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurants.Data;
using Restaurants.Model;

//implementation Swager and Logger

namespace Restaurants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();
        private readonly ILogger<MenuItemController> _logger;

        public MenuItemController(ILogger<MenuItemController> logger)
        {
            _logger = logger;
        }

        // GET: api/MenuItem
        [HttpGet]
        public IEnumerable<MenuItem> Get()
        {
            
            _logger.LogInformation("The Get MenuItem was invoked!");
            
            _logger.LogWarning("This is Warning!");
            /*
            _logger.LogError("This is Error");
            _logger.LogCritical("This is something critical!!!");
           */
            return _context.MenuItems.ToList();

        }

        // GET: api/MenuItem/5
        [HttpGet("{id}")]
        public MenuItem Get(int id)
        {
            return _context.MenuItems.Find(id);
        }

        // POST: api/MenuItem  //CREATE NEW ITEM
        [HttpPost]
        public void Post([FromBody] MenuItem value)
        {
            value.MenuId = _context.MenuItems.AsEnumerable().Last().MenuId + 1;
            _context.MenuItems.Add(value);
            _context.SaveChanges();
        }

        // PUT: api/MenuItem/5  // CHANGE SOMETHING
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] MenuItem value)
        {
            MenuItem mi = _context.MenuItems.Where(m => m.MenuId == id).FirstOrDefault(); // find menuItem by ID

            if (mi!=null)
            {
                mi.Name = value.Name;
                mi.Price = value.Price;
                _context.SaveChanges();

            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            MenuItem mi = _context.MenuItems.Find(id);   //find menuItem by ID
            _context.MenuItems.Remove(mi);
            _context.SaveChanges();
        }
    }
}
