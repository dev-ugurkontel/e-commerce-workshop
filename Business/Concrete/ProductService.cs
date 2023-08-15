using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Utils;
using Core.Validation;
using Core.Utils.Results;
using DataAccess.EF.Abstract;
using DataAccess.EF.Concrete;
using Entities.Entity;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly ProductRepositoryBase _productRepository;
        private readonly ICategoryService _categoryService;
        private readonly ICampaignService _campaignService;

        public ProductService(ProductRepositoryBase productRepository, ICategoryService categoryService, ICampaignService campaignService)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
            _campaignService = campaignService;
        }

        public IDataResult<List<ProductResponse>> GetAll()
        {


            
            var productList = _productRepository.GetAll().Select(p=> new ProductResponse()
            {
                ProductId = p.ProductId,
                CreateDate = p.CreateDate,
                ProductPrice = p.ProductPrice,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                ProductStock        = p.ProductStock,
                ProductImagePath = p.ProductImagePath,  
                ProductStatus = p.ProductStatus,    
                ProductUrl  = p.ProductUrl,
                EditDate = p.EditDate,  
                Campaign = _campaignService.Get(p.ProductCampaignId).Data,
                Category = _categoryService.Get(p.ProductCategoryId).Data
                
            }).ToList();

            return new SuccessDataResult<List<ProductResponse>>(productList, "Ürün bilgileri getirildi.");
        }

        public IDataResult<List<ProductResponse>> GetByCategoryId(int id)
        {

            var productList = _productRepository.GetAll(p=> p.ProductCategoryId == id).Select(p => new ProductResponse()
            {
                ProductId = p.ProductId,
                CreateDate = p.CreateDate,
                ProductPrice = p.ProductPrice,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                ProductStock = p.ProductStock,
                ProductImagePath = p.ProductImagePath,
                ProductStatus = p.ProductStatus,
                ProductUrl = p.ProductUrl,
                EditDate = p.EditDate,
                Campaign = _campaignService.Get(p.ProductCampaignId).Data,
                Category = _categoryService.Get(p.ProductCategoryId).Data

            }).ToList();

            return new SuccessDataResult<List<ProductResponse>>(productList, "Kategoriye ait tüm ürün bilgileri getirildi.");
        }

        public IDataResult<ProductResponse> Get(int id)
        {
            var product = _productRepository.Get(p => p.ProductId == id);
            // Kategori nesnesi boş gelme durumu değerlendirilecek.
            var productResponse = new ProductResponse() 
            {
                ProductStatus = product.ProductStatus,                    
                ProductUrl = product.ProductUrl,
                ProductImagePath = product.ProductImagePath,
                CreateDate  = product.CreateDate,
                ProductPrice = product.ProductPrice,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                EditDate= product.EditDate,
                ProductDescription = product.ProductDescription,
                ProductStock = product.ProductStock,
                Campaign = _campaignService.Get(product.ProductCampaignId).Data,
                Category = _categoryService.Get(product.ProductCategoryId).Data 
            };

            return new SuccessDataResult<ProductResponse>(productResponse, "Ürün bilgisi getirildi.");
        }


        public IDataResult<ProductResponse> Add(ProductRequest data)
        {
            string fileName = "";
            string fileExtension = "";
            string filePath = "";

            if (data.File != null && data.File.Length > 0)
            {
                fileExtension = Path.GetExtension(data.File.FileName);
                fileName = Guid.NewGuid() + fileExtension;
                filePath = Path.Combine("Files", fileName);

                using(FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    data.File.CopyTo(fileStream);
                }
            }


            var entity = new Product()
            {
                ProductCategoryId = data.ProductCategoryId, 
                ProductDescription= data.ProductDescription,
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now,
                ProductPrice = data.ProductPrice,
                ProductCampaignId = data.ProductCampaignId,
                ProductStatus = data.ProductStatus,
                ProductImagePath = filePath,
                ProductStock = data.ProductStock,
                ProductName= data.ProductName,
                ProductUrl = Guid.NewGuid().ToString()
            };

            _productRepository.Add(entity);

            ProductResponse productResponse = new()
            {
                ProductStatus = entity.ProductStatus,
                ProductUrl = entity.ProductUrl,
                ProductImagePath = entity.ProductImagePath,
                CreateDate = entity.CreateDate,
                ProductPrice = entity.ProductPrice,
                ProductId = entity.ProductId,
                ProductName = entity.ProductName,
                EditDate = entity.EditDate,
                ProductDescription = entity.ProductDescription,
                ProductStock = entity.ProductStock,
                Campaign = _campaignService.Get(entity.ProductCampaignId).Data,
                Category = _categoryService.Get(entity.ProductCategoryId).Data
            };

            return new SuccessDataResult<ProductResponse>(productResponse, "Ürün kaydedildi.");
        }


        public IResult Update(int id, ProductRequest data)
        {
            var product = _productRepository.Get(p => p.ProductId == id);
                product.ProductCategoryId = data.ProductCategoryId; 
                product.ProductDescription = data.ProductDescription;                
                product.EditDate = DateTime.Now;
                product.ProductPrice = data.ProductPrice;
                product.ProductCampaignId = data.ProductCampaignId;
                product.ProductStatus = data.ProductStatus;
                product.ProductImagePath = data.ProductImagePath;
                product.ProductStock = data.ProductStock;
                product.ProductName = data.ProductName;
            _productRepository.Update(product);
            return new SuccessResult("Ürün bilgileri güncellendi.");

        }

        public IResult Delete(int id)
        {
            var product = _productRepository.Get(p => p.ProductId == id);
            _productRepository.Delete(product);
            return new SuccessResult("Ürün Silindi.");

        }

       

    }
}
