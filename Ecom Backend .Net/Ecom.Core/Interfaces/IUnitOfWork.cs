using System;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IPhotoRepository Photos { get; }
        
        Task<bool> Complete();
        Task<int> SaveChangesAsync();
    }
}