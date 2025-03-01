using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsManagementSystem.Interfaces;
using ProductsManagementSystem.Models;
using ProductsManagementSystem.Models.VM.Order;

namespace ProductsManagementSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _service;
        private readonly ApplicationDbContext _context;


        public OrderController(IOrderService service, ApplicationDbContext context)
        {
            _context = context;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.Include(x => x.Products).ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.products = await _context.Products.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            await _service.CreateOrderAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int Id)
        {
            var order = await _context.Orders.Include(x => x.Products).FirstOrDefaultAsync(p => p.Id == Id);
            return View(order);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var order = await _context.Orders.Include(x => x.Products).FirstOrDefaultAsync(p => p.Id == Id);
            ViewBag.products = await _context.Products.ToListAsync();
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, OrderViewModel model)
        {
            await _service.UpdateOrderAsync(Id, model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            await _service.DeleteOrderAsync(Id);
            return RedirectToAction("Index");
        }
    }
}
