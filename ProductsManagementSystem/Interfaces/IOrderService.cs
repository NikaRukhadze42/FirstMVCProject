using Microsoft.AspNetCore.Mvc;
using ProductsManagementSystem.Models.Entities;
using ProductsManagementSystem.Models.VM.Order;

namespace ProductsManagementSystem.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderViewModel model);
        Task<List<Order>> GetAllOrdersAsync();
        Task UpdateOrderAsync(int Id, OrderViewModel model);
        Task DeleteOrderAsync(int Id);
    }
}
