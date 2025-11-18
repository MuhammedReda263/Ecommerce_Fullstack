using AutoMapper;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
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
        private IAuth _auth;
        private readonly IConnectionMultiplexer _redis;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IGenerateToken _generateToken;


        public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService,IConnectionMultiplexer redis, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, IGenerateToken generateToken)
        {
            _context = context;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
            _redis = redis;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _generateToken = generateToken;
        }

        public IProductRepository Products => 
            _productRepository ??= new ProductRepository(_context,_mapper,_imageManagementService);

        public ICategoryRepository Categories => 
            _categoryRepository ??= new CategoryRepository(_context);

        public IPhotoRepository Photos => 
            _photoRepository ??= new PhotoRepository(_context);

        public ICustomerBasketRepository CustomerBaskets =>
            _CustomerBaskets ??= new CustomerBasketRepository(_redis);

        public IAuth auth =>
              _auth ??= new AuthRepository(_userManager,_signInManager,_emailService, _generateToken);


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    
    }
}