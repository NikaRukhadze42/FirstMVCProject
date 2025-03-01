using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsManagementSystem.Interfaces;
using ProductsManagementSystem.Models;
using ProductsManagementSystem.Models.Entities;
using ProductsManagementSystem.Models.VM.Order;

namespace ProductsManagementSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(OrderViewModel model)
        {
            var products = await _context.Products
                .Where(p => model.ProductIds.Contains(p.Id))
                .ToListAsync();

            var newOrder = new Order()
            {
                Products = products
            };

            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int Id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == Id);
            if(order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<Order>> GetAllOrdersAsync()
        {
            return _context.Orders.ToListAsync();
        }

        public async Task UpdateOrderAsync(int Id, OrderViewModel model)
        {
            var order = await _context.Orders.Include(x => x.Products).FirstOrDefaultAsync(p => p.Id == Id);
            var products = await _context.Products
                .Where(p => model.ProductIds.Contains(p.Id))
                .ToListAsync();
            order.Products = products;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
