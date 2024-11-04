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
    public class ProductStatusController : ControllerBase, iController<ProductStatus>
    {
        private readonly WebRestOracleContext _context;

        public ProductStatusController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/ProductStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStatus>>> Get()
        {
            return await _context.ProductStatuses.ToListAsync();
        }

        // GET: api/ProductStatuses/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductStatus>> Get(string id)
        {
            var ProductStatus = await _context.ProductStatuses.FindAsync(id);

            if (ProductStatus == null)
            {
                return NotFound();
            }

            return ProductStatus;
        }

        // PUT: api/ProductStatuses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ProductStatus ProductStatus)
        {
            if (id != ProductStatus.ProductStatusId)
            {
                return BadRequest();
            }
            _context.ProductStatuses.Update(ProductStatus);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductStatusExists(id))
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

        // POST: api/ProductStatuses
        [HttpPost]
        public async Task<ActionResult<ProductStatus>> Post(ProductStatus ProductStatus)
        {
            _context.ProductStatuses.Add(ProductStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductStatus", new { id = ProductStatus.ProductStatusId }, ProductStatus);
        }

        // DELETE: api/ProductStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ProductStatus = await _context.ProductStatuses.FindAsync(id);
            if (ProductStatus == null)
            {
                return NotFound();
            }

            _context.ProductStatuses.Remove(ProductStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductStatusExists(string id)
        {
            return _context.ProductStatuses.Any(e => e.ProductStatusId == id);
        }
    }
}