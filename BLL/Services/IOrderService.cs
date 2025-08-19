using DAL.Entities;
using DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<List<Order>> GetOrdersByAccountIdAsync(int accountId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
    }
} 