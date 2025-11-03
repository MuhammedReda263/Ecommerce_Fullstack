using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Core.Sharing;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ecom.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IImageManagementService _imageManagementService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this._mapper = mapper;
            this._context = context;
            this._imageManagementService = imageManagementService;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync(ProductParams productParams)
        {
            var query = _context.Products
                .Include(m => m.Category)
                .Include(m => m.Photos)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(productParams.Search))
            {
                var searchWords = productParams.Search.Split(' ');
                query = query.Where(m => searchWords.All(word =>

                m.Name.ToLower().Contains(word.ToLower()) ||
                m.Description.ToLower().Contains(word.ToLower())

                ));
            }

            // Apply category filter only when a valid CategoryId is provided
            if (productParams.CategoryId is int cid && cid > 0)
            {
                query = query.Where(p => p.CategoryId == cid);
            }

            //filtering by word
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort.ToLower())
                {
                    case "priceace":
                        query = query.OrderBy(p => p.NewPrice);
                        break;
                    case "pricedce":
                        query = query.OrderByDescending(p => p.NewPrice);
                        break;
                    default:
                        query = query.OrderByDescending(p => p.Name);
                        break;
                }
            }
            
            query = query.Skip((productParams.PageNumber - 1) * productParams.pageSize).Take(productParams.pageSize);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(await query.ToListAsync());
        }
        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if (productDTO == null) return false;
            var product = _mapper.Map<Product>(productDTO);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var imagePath = await _imageManagementService.AddImageAsync(productDTO.Photo, productDTO.Name);

            var phots = imagePath.Select(path => new Photo
            {
                ProductId = product.Id,
                ImageName = path

            }).ToList();

            await _context.Photos.AddRangeAsync(phots);
            await _context.SaveChangesAsync();
            return true;


        }


        public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO is null)
            {
                return false;
            }
            var findProduct = _context.Products.Include(m => m.Category)
                .Include(m => m.Photos)
                .FirstOrDefault(p => p.Id == updateProductDTO.Id);

            if (findProduct is null) return false;
            _mapper.Map(updateProductDTO, findProduct);

            var findPhotos = _context.Photos.Where(p => p.ProductId == updateProductDTO.Id).ToList();  
            
            foreach (var item in findPhotos)
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }

            _context.Photos.RemoveRange(findPhotos);
            var imagePath = await _imageManagementService.AddImageAsync(updateProductDTO.Photo, updateProductDTO.Name);

            var photo = imagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = updateProductDTO.Id,
            }).ToList();

            await _context.Photos.AddRangeAsync(photo);

            await _context.SaveChangesAsync();
            return true;

        }

        public async Task DeleteAsync(Product product)
        {
            var photos = await _context.Photos.Where(p => p.ProductId == product.Id).ToListAsync();
            foreach (var item in photos)
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }
            _context.Products.Remove(product);
             await _context.SaveChangesAsync();
        }
    }
}
