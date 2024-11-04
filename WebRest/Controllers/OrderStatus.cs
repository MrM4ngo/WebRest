using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;
using WebRest.Interfaces;

namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase, iController<OrderStatus>
    {
        private readonly WebRestOracleContext _context;

        public OrderStatusController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> Get()
        {
            return await _context.OrderStatuses
                .Select(os => new OrderStatus
                {
                    OrderStatusId = os.OrderStatusId,
                    OrderStatusDesc = os.OrderStatusDesc,
                    OrderStatusNextOrderStatusId = os.OrderStatusNextOrderStatusId,
                    OrderStatusCrtdId = os.OrderStatusCrtdId,
                    OrderStatusCrtdDt = os.OrderStatusCrtdDt,
                    OrderStatusUpdtId = os.OrderStatusUpdtId,
                    OrderStatusUpdtDt = os.OrderStatusUpdtDt
                })
                .ToListAsync();
        }

        // GET: api/OrderStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatus>> Get(string id)
        {
            var orderStatus = await _context.OrderStatuses
                .Where(os => os.OrderStatusId == id)
                .Select(os => new OrderStatus
                {
                    OrderStatusId = os.OrderStatusId,
                    OrderStatusDesc = os.OrderStatusDesc,
                    OrderStatusNextOrderStatusId = os.OrderStatusNextOrderStatusId,
                    OrderStatusCrtdId = os.OrderStatusCrtdId,
                    OrderStatusCrtdDt = os.OrderStatusCrtdDt,
                    OrderStatusUpdtId = os.OrderStatusUpdtId,
                    OrderStatusUpdtDt = os.OrderStatusUpdtDt
                })
                .FirstOrDefaultAsync();

            if (orderStatus == null)
            {
                return NotFound();
            }

            return orderStatus;
        }

        // PUT: api/OrderStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrderStatus orderStatus)
        {
            if (id != orderStatus.OrderStatusId)
            {
                return BadRequest();
            }

            _context.OrderStatuses.Update(orderStatus);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStatusExists(id))
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

        // POST: api/OrderStatus
        [HttpPost]
        public async Task<ActionResult<OrderStatus>> Post(OrderStatus orderStatus)
        {
            _context.OrderStatuses.Add(orderStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = orderStatus.OrderStatusId }, orderStatus);
        }

        // DELETE: api/OrderStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var orderStatus = await _context.OrderStatuses.FindAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            _context.OrderStatuses.Remove(orderStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderStatusExists(string id)
        {
            return _context.OrderStatuses.Any(e => e.OrderStatusId == id);
        }
    }
}
