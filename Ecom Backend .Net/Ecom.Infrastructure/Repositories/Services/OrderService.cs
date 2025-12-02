using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities;
using Ecom.Core.Entities.Order;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork, AppDbContext dbContext, IMapper mapper, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
            _paymentService = paymentService;
        }

        public async Task<Orders> CreateOrdersAsync(OrderDTO orderDTO, string BuyerEmail)
        {
            var baskets = await _unitOfWork.CustomerBaskets.GetBasketAsync(orderDTO.basketId);
            List<OrderItem> orderItems = new List<OrderItem>();

            foreach(var item in baskets!.basketItems)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(item.Id);
                var orderItem = new OrderItem(product.Id, item.Image, product.Name, item.Price, item.Quantity);
                orderItems.Add(orderItem);
            }
            var deliverMethod = await _dbContext.DeliveryMethods.FirstOrDefaultAsync(m => m.Id == orderDTO.deliveryMethodId);
            var subTotal = orderItems.Sum(m => m.Price * m.Quntity);
            var ship = _mapper.Map<ShippingAddress>(orderDTO.shipAddress);
            var ExisitOrder = await _dbContext.Orders.Where(m => m.PaymentIntentId == baskets.paymentIntentId).FirstOrDefaultAsync();

            if (ExisitOrder is not null)
            {
                _dbContext.Orders.Remove(ExisitOrder);
                await _paymentService.CreateOrUpdatePaymentAsync(baskets.Id!, deliverMethod!.Id!);
            }

            var order = new
               Orders(BuyerEmail, subTotal, ship, ship, orderItems, deliverMethod!,baskets.paymentIntentId!);
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            await _unitOfWork.CustomerBaskets.DeleteBasketAsync(orderDTO.basketId);
            return order;
        }

        public async Task<IReadOnlyList<Orders>> GetAllOrdersForUserAsync(string BuyerEmail)
        {
           return await _dbContext.Orders.Where(temp => temp.BuyerEmail == BuyerEmail).Include(inc => inc.orderItems)
                .Include(inc => inc.deliveryMethod).AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await _dbContext.DeliveryMethods.AsNoTracking().ToListAsync();
        }

        public async Task<Orders> GetOrderByIdAsync(int Id, string BuyerEmail)
        {
           var order = await _dbContext.Orders.Where(temp => temp.Id == Id && temp.BuyerEmail == BuyerEmail)
                .Include(inc => inc.orderItems)
                .Include(inc => inc.deliveryMethod).AsNoTracking().FirstOrDefaultAsync();
            return order!;
        }
    }
}
