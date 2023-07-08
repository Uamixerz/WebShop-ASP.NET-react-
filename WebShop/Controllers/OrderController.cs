using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebShop.Data;
using WebShop.Data.Entities;
using WebShop.Data.Entities.Basket;
using WebShop.Data.Entities.Order;
using WebShop.Models;
using WebShop.Models.Orders;

namespace WebShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppEFContext _appEFContext;
        private readonly IMapper _mapper;
        public OrderController(AppEFContext appEFContext, IMapper mapper)
        {
            _appEFContext = appEFContext;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] OrderViewModel model)
        {
            if (model.userId != null) 
            {
                OrderEntity entity = _mapper.Map<OrderEntity>(model);
                _appEFContext.Orders.Add(entity);
                _appEFContext.SaveChanges();
                var items = _appEFContext.Baskets.Include(x => x.Product).Where(x => x.UserId == model.userId).ToList().Select(x => new OrderItemEntity()
                {
                    Quintity = x.Quintity,
                    ProductId = x.ProductId
                }).ToList();
                if (items.Count > 0)
                    _appEFContext.SaveChanges();
                else
                    return BadRequest(404);
                var recordsToDelete = _appEFContext.Baskets.Where(e => e.UserId == model.userId);
                _appEFContext.Baskets.RemoveRange(recordsToDelete);

                entity.Items = items;
                _appEFContext.SaveChanges();
            }

            return Ok();
        }
    }
}
