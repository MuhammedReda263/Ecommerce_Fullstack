using Ecom.Core.Services;
using System;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IPhotoRepository Photos { get; }
        ICustomerBasketRepository CustomerBaskets { get; }
        IAuth auth { get; }
        Task<int> SaveChangesAsync();
    }
}