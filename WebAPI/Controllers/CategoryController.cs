using Business.Abstract;
using Core.Utils;
using Entities.Entity;
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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _categoryService.Get(id);
            if(result.Status == ResultStatus.Success)
            { 
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryRequest category)
        {
            var result = _categoryService.Add(category);
            if (result.Status == ResultStatus.Success)
            {
                return Ok(result); 
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryRequest updatedCategory) 
        {
            var result = _categoryService.Update(id, updatedCategory);
            if(result.Status == ResultStatus.Success)
            { 
                return Ok(result); 
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id) 
        {
            var result = _categoryService.Delete(id);
            if (result.Status == ResultStatus.Success)
            {
                return Ok(result); 
            }
            return BadRequest(result);
        }
    }
}
