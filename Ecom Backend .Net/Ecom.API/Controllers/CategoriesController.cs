using AutoMapper;
using Ecom.API.Helpers;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Ecom.API.Controllers
{

    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            if (categories == null || !categories.Any())
            {
                return NotFound(new ResponseAPI(404));
            }
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null) return NotFound(new ResponseAPI(404));
            return Ok(category);
            
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody]CategoryDTO categoryDTO)
        {
            await _unitOfWork.Categories.AddAsync(
               _mapper.Map<Category>(categoryDTO)
               );
            return Ok(new ResponseAPI(200,"Category added successfully"));
            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody]UpdateCategoryDTO updateCategoryDTO)
        {
            await _unitOfWork.Categories.UpdateAsync(
                _mapper.Map<Category>(updateCategoryDTO)
                );
           return Ok(new ResponseAPI(200, "Category updated successfully"));

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
           await _unitOfWork.Categories.DeleteAsync(id);
           return Ok(new ResponseAPI(200, "Category deleted successfully"));

        }


    }
}
