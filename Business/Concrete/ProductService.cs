using Business.Abstract;
using Core.Utils;
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

        public ProductService(ProductRepositoryBase productRepository, ICategoryService categoryService)
        {
            _productRepository = productRepository;
            _categoryService = categoryService; 
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
                ProductSKU    = p.ProductSKU,
                ProductStock        = p.ProductStock,
                ProductImagePath = p.ProductImagePath,  
                ProductStatus = p.ProductStatus,    
                ProductUrl  = p.ProductUrl,
                EditDate = p.EditDate,  
                ProductCampaingId = p.ProductCampaingId,
                Category = _categoryService.Get(p.ProductCategoryId).Data
                
            }).ToList();

            return new SuccessDataResult<List<ProductResponse>>(productList, "Ürün bilgileri getirildi.");
        }

        public IDataResult<ProductResponse> Get(int id)
        {
            var product = _productRepository.Get(p => p.ProductId == id);
            // Kategori nesnesi boş gelme durumu değerlendirilecek.
            var productResponse = new ProductResponse() 
            {
                ProductCampaingId= product.ProductCampaingId,
                ProductStatus = product.ProductStatus,
                    
                ProductUrl = product.ProductUrl,
                ProductImagePath = product.ProductImagePath,
                CreateDate  = product.CreateDate,
                ProductPrice = product.ProductPrice,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                EditDate= product.EditDate,
                ProductDescription = product.ProductDescription,
                ProductSKU  = product.ProductSKU,
                ProductStock = product.ProductStock,
                Category = _categoryService.Get(product.ProductCategoryId).Data 
            };

            return new SuccessDataResult<ProductResponse>(productResponse, "Ürün bilgisi getirildi.");
        }

        public IResult Add(ProductRequest data)
        {
            var entity = new Product()
            {
                ProductCategoryId = data.ProductCategoryId, 
                ProductDescription= data.ProductDescription,
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now,
                ProductPrice = data.ProductPrice,
                ProductCampaingId = data.ProductCampaingId,
                ProductStatus = data.ProductStatus,
                ProductImagePath = data.ProductImagePath,
                ProductStock = data.ProductStock,
                ProductUrl    = data.ProductUrl,
                ProductName= data.ProductName,
                ProductSKU= data.ProductSKU
                
            };
            _productRepository.Add(entity);
            return new SuccessResult("Ürün kaydedildi.");
        }



        public IResult Update(int id, ProductRequest data)
        {
            var product = _productRepository.Get(p => p.ProductId == id);
                product.ProductCategoryId = data.ProductCategoryId; 
                product.ProductDescription = data.ProductDescription;                
                product.EditDate = DateTime.Now;
                product.ProductPrice = data.ProductPrice;
                product.ProductCampaingId = data.ProductCampaingId;
                product.ProductStatus = data.ProductStatus;
                product.ProductImagePath = data.ProductImagePath;
                product.ProductStock = data.ProductStock;
                product.ProductUrl = data.ProductUrl;
                product.ProductName = data.ProductName;
                product.ProductSKU = data.ProductSKU;
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
