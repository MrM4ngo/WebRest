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
    public class AddressTypeController : ControllerBase, iController<AddressType>
    {
        private readonly WebRestOracleContext _context;

        public AddressTypeController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/AddressTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressType>>> Get()
        {
            return await _context.AddressTypes.ToListAsync();
        }

        // GET: api/AddressTypes/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AddressType>> Get(string id)
        {
            var AddressType = await _context.AddressTypes.FindAsync(id);

            if (AddressType == null)
            {
                return NotFound();
            }

            return AddressType;
        }

        // PUT: api/AddressTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, AddressType AddressType)
        {
            if (id != AddressType.AddressTypeId)
            {
                return BadRequest();
            }
            _context.AddressTypes.Update(AddressType);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressTypeExists(id))
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

        // POST: api/AddressTypes
        [HttpPost]
        public async Task<ActionResult<AddressType>> Post(AddressType AddressType)
        {
            _context.AddressTypes.Add(AddressType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddressType", new { id = AddressType.AddressTypeId }, AddressType);
        }

        // DELETE: api/AddressTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var AddressType = await _context.AddressTypes.FindAsync(id);
            if (AddressType == null)
            {
                return NotFound();
            }

            _context.AddressTypes.Remove(AddressType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressTypeExists(string id)
        {
            return _context.AddressTypes.Any(e => e.AddressTypeId == id);
        }
    }
}