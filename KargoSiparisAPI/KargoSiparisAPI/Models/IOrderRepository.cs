using System.Collections.Generic;
using System.Threading.Tasks;
namespace KargoSiparisAPI.Models
{
    public interface IOrderRepository
    {
        public interface IOrderRepository
        {
            Task<IEnumerable<Order>> GetAllOrdersAsync();  
            Task<Order> GetOrderByIdAsync(int id);         
            Task AddOrderAsync(Order order);              
            Task DeleteOrderAsync(int id);                
        }
    }
}
