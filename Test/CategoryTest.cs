using Business.Concrete;
using Core.Utils;
using Entities.Surrogate.Request;

namespace Test
{
    public class CategoryTest : BaseTest
    {
        private readonly CategoryService _categoryService;

        public CategoryTest()
        {
            _categoryService = new CategoryService(_categoryRepository);
        }

        [Fact]
        public void Add_Success()
        {
            CategoryRequest request = new()
            {
                CategoryName = GetRandomString(),
                CategoryDescription = GetRandomString(),
                CategoryStatus = 1
            };

            var result = _categoryService.Add(request);

            Assert.NotNull(result);
            Assert.True(result.Status == ResultStatus.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void Add_Exception()
        {
            CategoryRequest request = new();

            var result = _categoryService.Add(request);

            Assert.Null(result.Data);
            Assert.True(result.Status == ResultStatus.Exception);
        }

        [Fact]
        public void GetAll_Success()
        {
            CategoryRequest request = new()
            {
                CategoryName = GetRandomString(),
                CategoryDescription = GetRandomString(),
                CategoryStatus = 1
            };

            var result = _categoryService.Add(request);

            Assert.NotNull(result);
            Assert.True(result.Status == ResultStatus.Success);
            Assert.NotNull(result.Data);

            var categories = _categoryService.GetAll();

            Assert.NotNull(categories);
            Assert.True(categories.Status == ResultStatus.Success);
            Assert.NotNull(categories.Data);
        }

        [Fact]
        public void Get_Success()
        {
            CategoryRequest request = new()
            {
                CategoryName = GetRandomString(),
                CategoryDescription = GetRandomString(),
                CategoryStatus = 1
            };

            var result = _categoryService.Add(request);

            Assert.NotNull(result);
            Assert.True(result.Status == ResultStatus.Success);
            Assert.NotNull(result.Data);

            var category = _categoryService.Get(result.Data.CategoryId);

            Assert.NotNull(category);
            Assert.True(category.Status == ResultStatus.Success);
            Assert.NotNull(category.Data);
        }

        [Fact]
        public void Get_Error()
        {
            var category = _categoryService.Get(-1);

            Assert.Null(category.Data);
            Assert.True(category.Status == ResultStatus.Error);
        }

        [Fact]
        public void Update_Success()
        {
            CategoryRequest request = new()
            {
                CategoryName = GetRandomString(),
                CategoryDescription = GetRandomString(),
                CategoryStatus = 1
            };

            var result = _categoryService.Add(request);

            Assert.NotNull(result);
            Assert.True(result.Status == ResultStatus.Success);
            Assert.NotNull(result.Data);

            CategoryRequest updateRequest = new()
            {
                CategoryName = $"UPDATE_{GetRandomString()}",
                CategoryDescription = $"UPDATE_{GetRandomString()}",
                CategoryStatus = 0
            };

            var resultUpdate = _categoryService.Update(result.Data.CategoryId, updateRequest);

            Assert.NotNull(resultUpdate);
            Assert.True(resultUpdate.Status == ResultStatus.Success);
        }

        [Fact]
        public void Update_Exception()
        {
            CategoryRequest request = new()
            {
                CategoryName = GetRandomString(),
                CategoryDescription = GetRandomString(),
                CategoryStatus = 1
            };

            var result = _categoryService.Add(request);

            Assert.NotNull(result);
            Assert.True(result.Status == ResultStatus.Success);
            Assert.NotNull(result.Data);

            CategoryRequest updateRequest = new();

            var resultUpdate = _categoryService.Update(result.Data.CategoryId, updateRequest);

            Assert.NotNull(resultUpdate);
            Assert.True(resultUpdate.Status == ResultStatus.Exception);
        }

        [Fact]
        public void GetCategory_Success()
        {
            CategoryRequest request = new()
            {
                CategoryName = GetRandomString(),
                CategoryDescription = GetRandomString(),
                CategoryStatus = 1
            };

            var result = _categoryService.Add(request);

            Assert.NotNull(result);
            Assert.True(result.Status == ResultStatus.Success);
            Assert.NotNull(result.Data);

            var category = _categoryService.Get(result.Data.CategoryId);

            Assert.NotNull(result);
            Assert.True(category.Status == ResultStatus.Success);
            Assert.NotNull(category.Data);
        }

        [Fact]
        public void GetCategory_Error()
        {
            var category = _categoryService.Get(-1);
            Assert.Null(category.Data);
            Assert.True(category.Status == ResultStatus.Error);
        }
    }
}
