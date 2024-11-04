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
    public class ProductPriceController : ControllerBase, iController<ProductPrice>
    {
        private readonly WebRestOracleContext _context;

        public ProductPriceController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/ProductPrices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPrice>>> Get()
        {
            return await _context.ProductPrices.ToListAsync();
        }

        // GET: api/ProductPrices/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductPrice>> Get(string id)
        {
            var ProductPrice = await _context.ProductPrices.FindAsync(id);

            if (ProductPrice == null)
            {
                return NotFound();
            }

            return ProductPrice;
        }

        // PUT: api/ProductPrices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ProductPrice ProductPrice)
        {
            if (id != ProductPrice.ProductPriceId)
            {
                return BadRequest();
            }
            _context.ProductPrices.Update(ProductPrice);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPriceExists(id))
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

        // POST: api/ProductPrices
        [HttpPost]
        public async Task<ActionResult<ProductPrice>> Post(ProductPrice ProductPrice)
        {
            _context.ProductPrices.Add(ProductPrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetProductPrice",
                new { id = ProductPrice.ProductPriceId },
                ProductPrice
            );
        }

        // DELETE: api/ProductPrices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ProductPrice = await _context.ProductPrices.FindAsync(id);
            if (ProductPrice == null)
            {
                return NotFound();
            }

            _context.ProductPrices.Remove(ProductPrice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductPriceExists(string id)
        {
            return _context.ProductPrices.Any(e => e.ProductPriceId == id);
        }
    }
}
