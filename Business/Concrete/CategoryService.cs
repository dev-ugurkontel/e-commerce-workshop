using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Utils;
using Core.Validation;
using Core.Utils.Results;
using DataAccess.EF.Abstract;
using Entities.Entity;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;

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

            try
            {
                var getCategory = _categoryRepository.Get(c => c.CategoryId == id);

                if (getCategory == null)
                {
                    return new ErrorDataResult<CategoryResponse>(default,"Kayıt bulunamadı.");
                }
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
            catch (Exception ex)
            {
                return new ExceptionDataResult<CategoryResponse>();
            }
            
            
        }
        public IDataResult<CategoryResponse> Add(CategoryRequest data)
        {
            try
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

                CategoryResponse categoryResponse = new()
                {
                    CategoryDescription = entity.CategoryDescription,
                    CategoryName = entity.CategoryName,
                    CategoryId = entity.CategoryId,
                    CategoryStatus = entity.CategoryStatus,
                    CreateDate = entity.CreateDate,
                    EditDate = entity.EditDate
                };

                return new SuccessDataResult<CategoryResponse>(categoryResponse, "Kategori başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                return new ExceptionDataResult<CategoryResponse>(default, ex);
            }            
        }

        public IResult Update(int id, CategoryRequest data)
        {
            try
            {
                var category = _categoryRepository.Get(c => c.CategoryId == id);
                category.CategoryDescription = data.CategoryDescription;
                category.CategoryName = data.CategoryName;
                category.CategoryStatus = data.CategoryStatus;
                category.EditDate = DateTime.Now;
                _categoryRepository.Update(category);
                return new SuccessResult("Kategori başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return new ExceptionResult(ex);
            }            
        }

        public IResult Delete(int id)
        {
           var category = _categoryRepository.Get(c=> c.CategoryId==id);
            _categoryRepository.Delete(category);
            return new SuccessResult("Kategori başarıyla silindi.");
            
        }           
    }
}
