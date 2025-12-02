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

        public async Task<CustomerBasket> CreateOrUpdatePaymentAsync(string basketId, int? deliveryMethodId)
        {
            var basket = await _unitOfWork.CustomerBaskets.GetBasketAsync(basketId);
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            decimal shippingPrice = 0m;

            if (deliveryMethodId.HasValue)
            {
                var delivery = await _appDbContext.DeliveryMethods
                    .FirstOrDefaultAsync(m => m.Id == deliveryMethodId.Value);

                shippingPrice = delivery!.Price;
            }

            // Update basket item prices
            foreach (var item in basket!.basketItems)
            {
                var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == item.Id);
                item.Price = product!.NewPrice;
            }

            var amount = (long)basket.basketItems.Sum(m => m.Quantity * (m.Price * 100))
                         + (long)(shippingPrice * 100);

            PaymentIntentService paymentIntentService = new();

            PaymentIntent? intent = null;

            if (string.IsNullOrEmpty(basket.paymentIntentId))
            {
                // Create new PaymentIntent
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await paymentIntentService.CreateAsync(options);
                basket.paymentIntentId = intent.Id;
                basket.clientSecret = intent.ClientSecret;
            }
            else
            {
                // Fetch existing intent
                intent = await paymentIntentService.GetAsync(basket.paymentIntentId);

                // Check if allowed to update
                if (intent.Status == "requires_payment_method")
                {
                    var updateOptions = new PaymentIntentUpdateOptions
                    {
                        Amount = amount
                    };

                    await paymentIntentService.UpdateAsync(intent.Id, updateOptions);
                }
                else
                {
                    // Not allowed to update → create new one
                    var options = new PaymentIntentCreateOptions
                    {
                        Amount = amount,
                        Currency = "usd",
                        PaymentMethodTypes = new List<string> { "card" }
                    };

                    var newIntent = await paymentIntentService.CreateAsync(options);
                    basket.paymentIntentId = newIntent.Id;
                    basket.clientSecret = newIntent.ClientSecret;
                }
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
