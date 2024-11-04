using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using WebRest.Interfaces;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;

namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersLineController : ControllerBase, iController<OrdersLine>
    {
        private readonly WebRestOracleContext _context;

        public OrdersLineController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrdersLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersLine>>> Get()
        {
            return await _context.OrdersLines.ToListAsync();
        }

        // GET: api/OrdersLines/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OrdersLine>> Get(string id)
        {
            var OrdersLine = await _context.OrdersLines.FindAsync(id);

            if (OrdersLine == null)
            {
                return NotFound();
            }

            return OrdersLine;
        }

        // PUT: api/OrdersLines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrdersLine OrdersLine)
        {
            if (id != OrdersLine.OrdersLineId)
            {
                return BadRequest();
            }
            _context.OrdersLines.Update(OrdersLine);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersLineExists(id))
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

        // POST: api/OrdersLines
        [HttpPost]
        public async Task<ActionResult<OrdersLine>> Post(OrdersLine OrdersLine)
        {
            _context.OrdersLines.Add(OrdersLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetOrdersLine",
                new { id = OrdersLine.OrdersLineId },
                OrdersLine
            );
        }

        // DELETE: api/OrdersLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var OrdersLine = await _context.OrdersLines.FindAsync(id);
            if (OrdersLine == null)
            {
                return NotFound();
            }

            _context.OrdersLines.Remove(OrdersLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersLineExists(string id)
        {
            return _context.OrdersLines.Any(e => e.OrdersLineId == id);
        }
    }
}
