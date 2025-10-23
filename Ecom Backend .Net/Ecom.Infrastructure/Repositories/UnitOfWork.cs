using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;

namespace Ecom.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IPhotoRepository _photoRepository;
        private IMapper _mapper;
        private IImageManagementService _imageManagementService;

        public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService)
        {
            _context = context;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
        }

        public IProductRepository Products => 
            _productRepository ??= new ProductRepository(_context,_mapper,_imageManagementService);

        public ICategoryRepository Categories => 
            _categoryRepository ??= new CategoryRepository(_context);

        public IPhotoRepository Photos => 
            _photoRepository ??= new PhotoRepository(_context);


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    
    }
}