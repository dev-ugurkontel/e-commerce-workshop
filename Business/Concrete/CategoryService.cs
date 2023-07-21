using Business.Abstract;
using Core.Utils;
using DataAccess.EF.Abstract;
using Entities.Entity;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepositoryBase _categoryRepository;
        public CategoryService(CategoryRepositoryBase categoryRepository)
        {
                _categoryRepository = categoryRepository;
        }
        public IDataResult<List<CategoryResponse>> GetAll()
        {
            var categoryList = _categoryRepository.GetAll().Select(c=> new CategoryResponse()
            {
                CategoryDescription= c.CategoryDescription,
                CategoryName= c.CategoryName,
                CategoryId= c.CategoryId,
                CategoryStatus= c.CategoryStatus,
                CreateDate= c.CreateDate,
                EditDate = c.EditDate
            }).ToList();

            return new SuccessDataResult<List<CategoryResponse>>(categoryList,"Kategori bilgileri getirildi.");
        }

        public IDataResult<CategoryResponse> Get(int id)
        {
            var getCategory = _categoryRepository.Get(c => c.CategoryId == id);
            var categoryResponse = new CategoryResponse()
            {
                CategoryId = getCategory.CategoryId,
                CategoryDescription = getCategory.CategoryDescription,
                CategoryName = getCategory.CategoryName,
                CategoryStatus = getCategory.CategoryStatus,
                CreateDate = getCategory.CreateDate,
                EditDate = getCategory.EditDate
            };
            return new SuccessDataResult<CategoryResponse>(categoryResponse);
            
        }

        public IResult Add(CategoryRequest data)
        {
            var entity = new Category()
            {
                CategoryDescription = data.CategoryDescription,
                EditDate = DateTime.Now,
                CategoryName = data.CategoryName,
                CreateDate = DateTime.Now,
                CategoryStatus = data.CategoryStatus
            };
            _categoryRepository.Add(entity);
            return new SuccessResult("Kategori başarıyla kaydedildi.");
        }

        public IResult Update(int id, CategoryRequest data)
        {
            var category = _categoryRepository.Get(c=> c.CategoryId == id);
            category.CategoryDescription = data.CategoryDescription;
            category.CategoryName = data.CategoryName;
            category.CategoryStatus = data.CategoryStatus;
            category.EditDate = DateTime.Now;
            _categoryRepository.Update(category);
            return new SuccessResult("Kategori başarıyla güncellendi");
        }

        public IResult Delete(int id)
        {
           var category = _categoryRepository.Get(c=> c.CategoryId==id);
            _categoryRepository.Delete(category);
            return new SuccessResult("Kategori başarıyla silindi.");
            
        }

      

     

      
    }
}
