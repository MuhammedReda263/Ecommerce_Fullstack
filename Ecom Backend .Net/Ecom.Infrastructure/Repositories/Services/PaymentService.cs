using Ecom.Core.Entities;
using Ecom.Core.Entities.Order;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public PaymentService(IUnitOfWork unitOfWork, IConfiguration configuration, AppDbContext appDbContext)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _appDbContext = appDbContext;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentAsync(string basketId, int? delivertMethodId)
        {
            var basket = await _unitOfWork.CustomerBaskets.GetBasketAsync(basketId);
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            decimal shippingPrice = 0m;
            if (delivertMethodId.HasValue)
            {
                var delivery = await _appDbContext.DeliveryMethods.FirstOrDefaultAsync(temp => temp.Id == delivertMethodId);
                shippingPrice = delivery!.Price;
            }

            foreach (var item in basket!.basketItems)
            {
                var product = await _appDbContext.Products.FirstOrDefaultAsync(temp => temp.Id == item.Id);
                item.Price = product!.NewPrice;
            }

            PaymentIntentService paymentIntentService = new();
            PaymentIntent _intent;
            if (string.IsNullOrEmpty(basket.paymentIntentId))
            {
                var option = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.basketItems.Sum(m => m.Quantity * (m.Price * 100)) + (long)(shippingPrice * 100),

                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                _intent = await paymentIntentService.CreateAsync(option);
                basket.paymentIntentId = _intent.Id;
                basket.clientSecret = _intent.ClientSecret;
            }
            else
            {
                var option = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.basketItems.Sum(m => m.Quantity * (m.Price * 100)) + (long)(shippingPrice * 100),

                };
                await paymentIntentService.UpdateAsync(basket.paymentIntentId, option);
            }
            await _unitOfWork.CustomerBaskets.UpdateBasketAsync(basket);
            return basket;


        }

        public async Task<Orders> UpdateOrderFaild(string PaymentInten)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(m => m.PaymentIntentId == PaymentInten);
            if (order is null)
            {
                return null!;
            }
            order.status = Status.PaymentFaild;
            _appDbContext.Orders.Update(order);
            await _appDbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Orders> UpdateOrderSuccess(string PaymentInten)
        {
            var order = await _appDbContext.Orders.FirstOrDefaultAsync(m => m.PaymentIntentId == PaymentInten);
            if (order is null)
            {
                return null!;
            }
            order.status = Status.PaymentReceived;
            _appDbContext.Orders.Update(order);
            await _appDbContext.SaveChangesAsync();
            return order;
        }
    }
}
