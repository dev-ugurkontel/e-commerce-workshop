using Business.Abstract;
using Core.Utils.Results;
using Entities.Surrogate.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v1/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("List_Category")]
        public IActionResult List_Category()
        {
            var result = _categoryService.GetAll();
            if (result.Status == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Route("Save_Category")]
        [HttpPost]
        public IActionResult Save_Category(CategoryRequest category)
        {
            var result = _categoryService.Add(category);
            return Ok(result);
        }

        [Route("Find_Category/{id}")]
        [HttpGet]
        public IActionResult Find_Category(int id)
        {
            var category = _categoryService.Get(id);
            if (category.Status == ResultStatus.Error)
            {
                return NotFound();  
            }
            else if(category.Status == ResultStatus.Success)
            {
                return Ok(category); 
            }
            else
            {
                return BadRequest();
            }
           
        }

        [Route("Update_Category/{id}")]
        [HttpPut]
        public IActionResult Update_Category(int id, CategoryRequest category)
        {
            _categoryService.Update(id, category);
            return Ok(category);
        }

        [Route("Delete_Category/{id}")]
        [HttpDelete]
        public IActionResult Delete_Category(int id)
        {
            _categoryService.Delete(id);
            return NoContent();
        }
    }
}
