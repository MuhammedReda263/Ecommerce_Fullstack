using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using StackExchange.Redis;

namespace Ecom.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IPhotoRepository _photoRepository;
        private ICustomerBasketRepository _CustomerBaskets;
        private IMapper _mapper;
        private IImageManagementService _imageManagementService;
        private readonly IConnectionMultiplexer _redis;

        public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService,IConnectionMultiplexer redis)
        {
            _context = context;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
            _redis = redis;
        }

        public IProductRepository Products => 
            _productRepository ??= new ProductRepository(_context,_mapper,_imageManagementService);

        public ICategoryRepository Categories => 
            _categoryRepository ??= new CategoryRepository(_context);

        public IPhotoRepository Photos => 
            _photoRepository ??= new PhotoRepository(_context);

        public ICustomerBasketRepository CustomerBaskets =>
            _CustomerBaskets ??= new CustomerBasketRepository(_redis);



        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    
    }
}