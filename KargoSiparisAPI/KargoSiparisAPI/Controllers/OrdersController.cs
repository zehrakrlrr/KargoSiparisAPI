using KargoSiparisAPI.Data;
using KargoSiparisAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KargoSiparisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public OrdersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { Message = "Validation failed", Errors = errors });
            }

            
            if (order == null)
            {
                return BadRequest("Order bilgisi eksik.");
            }

            
            var carrier = await _dbContext.Carriers
                .Where(c => order.Desi >= c.MinDesi && order.Desi <= c.MaxDesi)
                .OrderBy(c => c.Price)
                .FirstOrDefaultAsync();

            if (carrier == null)
            {
                return NotFound("Uygun kargo firması bulunamadı.");
            }

            
            order.ShippingPrice = carrier.Price;

            
            var difference = order.Desi - carrier.MaxDesi;
            if (difference > 0)
            {
                order.ShippingPrice += carrier.PricePerExtraDesi * difference;
            }

            
            order.CarrierId = carrier.CarrierId;

            
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return Ok("Sipariş başarıyla eklendi.");
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _dbContext.Orders
                .Include(o => o.Carrier) 
                .ToListAsync();
            return Ok(orders);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound("Sipariş bulunamadı.");
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return Ok("Sipariş başarıyla silindi.");
        }

        [HttpPost("carrier")] 
        public async Task<IActionResult> CreateCarrier([FromBody] Carrier carrier)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { Message = "Validation failed", Errors = errors });
            }

            _dbContext.Carriers.Add(carrier);
            await _dbContext.SaveChangesAsync();
            return Ok("Kargo firması başarıyla eklendi.");
        }
    }
}
