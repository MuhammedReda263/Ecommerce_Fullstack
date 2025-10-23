using AutoMapper;
using Ecom.API.Helpers;
using Ecom.Core.DTO;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IImageManagementService _imageService;
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, IImageManagementService imageService) : base(unitOfWork, mapper)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {

            var product = await _unitOfWork.Products.GetAllAsync(x => x.Photos, x => x.Category);

            var result = _mapper.Map<List<ProductDTO>>(product);

            if (product is null) return BadRequest(new ResponseAPI(400));

            return Ok(result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {
            
                var product = await _unitOfWork.Products.GetByIdAsync(id,
                    x => x.Category, x => x.Photos);

                var result = _mapper.Map<ProductDTO>(product);

                if (product is null) return BadRequest(new ResponseAPI(400));


                return Ok(result);
       
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddProductDTO productDTO)
        {
     
           if (!await _unitOfWork.Products.AddAsync(productDTO))
            return BadRequest(new ResponseAPI(400, "Failed to add product"));
            return Ok(new ResponseAPI(200, "Product added successfully"));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDTO updateProductDTO)
        {
            
               if(!await _unitOfWork.Products.UpdateAsync(updateProductDTO))
                return BadRequest(new ResponseAPI(400, "Failed to update product"));
                return Ok(new ResponseAPI(200, "Product updated successfully"));


        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
           
                var product = await _unitOfWork.Products
                    .GetByIdAsync(Id, x => x.Photos, x => x.Category);
                if (product is null)
                    return NotFound(new ResponseAPI(404, "Product not found"));

                 await _unitOfWork.Products.DeleteAsync(product); 
                return Ok(new ResponseAPI(200, "Product deleted successfully"));

            
            
        }


    }
}
