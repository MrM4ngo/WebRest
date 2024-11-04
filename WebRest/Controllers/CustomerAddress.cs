using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;
using WebRest.Interfaces;
namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressesController : ControllerBase, iController<CustomerAddress>
    {
        private readonly WebRestOracleContext _context;

        public CustomerAddressesController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/CustomerAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddress>>> Get()
        {
            return await _context.CustomerAddresses.ToListAsync();
        }

        // GET: api/CustomerAddresses/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CustomerAddress>> Get(string id)
        {
            var CustomerAddress = await _context.CustomerAddresses.FindAsync(id);

            if (CustomerAddress == null)
            {
                return NotFound();
            }

            return CustomerAddress;
        }

        // PUT: api/CustomerAddresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, CustomerAddress CustomerAddress)
        {
            if (id != CustomerAddress.CustomerAddressId)
            {
                return BadRequest();
            }
            _context.CustomerAddresses.Update(CustomerAddress);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomerAddresses
        [HttpPost]
        public async Task<ActionResult<CustomerAddress>> Post(CustomerAddress CustomerAddress)
        {
            _context.CustomerAddresses.Add(CustomerAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerAddress", new { id = CustomerAddress.CustomerAddressId }, CustomerAddress);
        }

        // DELETE: api/CustomerAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var CustomerAddress = await _context.CustomerAddresses.FindAsync(id);
            if (CustomerAddress == null)
            {
                return NotFound();
            }

            _context.CustomerAddresses.Remove(CustomerAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerAddressExists(string id)
        {
            return _context.CustomerAddresses.Any(e => e.CustomerAddressId == id);
        }
    }
}