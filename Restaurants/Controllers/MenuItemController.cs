using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Data;
using Restaurants.Model;

namespace Restaurants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();
        // GET: api/MenuItem
        [HttpGet]
        public IEnumerable<MenuItem> Get()
        {
            return _context.MenuItems.ToList();

        }

        // GET: api/MenuItem/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MenuItem
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/MenuItem/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
