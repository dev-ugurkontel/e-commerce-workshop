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
        private readonly IProductFileService _productFileService;

        public ProductService(ProductRepositoryBase productRepository, ICategoryService categoryService, ICampaignService campaignService, IProductFileService productFileService)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
            _campaignService = campaignService;
            _productFileService = productFileService;
        }

        public IDataResult<PageResponse<ProductResponse>> GetAll(int page = 0, int pageSize = 0)
        {
            if (page != 0 && pageSize != 0)
            {
                var totalCount = _productRepository.GetAll().Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);



                var productList = _productRepository.GetAll().Select(p => new ProductResponse()
                {
                    ProductId = p.ProductId,
                    CreateDate = p.CreateDate,
                    ProductPrice = p.ProductPrice,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductStock = p.ProductStock,
                    ProductStatus = p.ProductStatus,
                    ProductUrl = p.ProductUrl,
                    EditDate = p.EditDate,
                    Campaign = _campaignService.Get(p.ProductCampaignId).Data,
                    Category = _categoryService.Get(p.ProductCategoryId).Data,
                    Files = _productFileService.GetByProductId(p.ProductId).Data

                }).Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();


                var pageResponse = new PageResponse<ProductResponse>()
                {
                    ActivePage = page,
                    PageSize = pageSize,
                    Data = productList,
                    TotalPages = totalPages,
                    TotalRecords = totalCount
                };

               
                return new SuccessDataResult<PageResponse<ProductResponse>>(pageResponse, "Ürün bilgileri getirildi.");
            }
            else
            {
                var productList = _productRepository.GetAll().Select(p => new ProductResponse()
                {
                    ProductId = p.ProductId,
                    CreateDate = p.CreateDate,
                    ProductPrice = p.ProductPrice,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductStock = p.ProductStock,
                    Files = _productFileService.GetByProductId(p.ProductId).Data,
                    ProductStatus = p.ProductStatus,
                    ProductUrl = p.ProductUrl,
                    EditDate = p.EditDate,
                    Campaign = _campaignService.Get(p.ProductCampaignId).Data,
                    Category = _categoryService.Get(p.ProductCategoryId).Data

                }).ToList();

                var pageResponse = new PageResponse<ProductResponse>()
                {
                    ActivePage = page,
                    PageSize = pageSize,
                    Data = productList,
                    TotalPages = 0,
                    TotalRecords = 0
                };

                return new SuccessDataResult<PageResponse<ProductResponse>>(pageResponse, "Ürün bilgileri getirildi.");

            }

        }

        public IDataResult<List<ProductResponse>> GetByCategoryId(int id)
        {

            var productList = _productRepository.GetAll(p => p.ProductCategoryId == id).Select(p => new ProductResponse()
            {
                ProductId = p.ProductId,
                CreateDate = p.CreateDate,
                ProductPrice = p.ProductPrice,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                Files = _productFileService.GetByProductId(p.ProductId).Data,
                ProductStock = p.ProductStock,
                ProductStatus = p.ProductStatus,
                ProductUrl = p.ProductUrl,
                EditDate = p.EditDate,
                Campaign = _campaignService.Get(p.ProductCampaignId).Data,
                Category = _categoryService.Get(p.ProductCategoryId).Data,

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
                Files = _productFileService.GetByProductId(product.ProductId).Data,
                CreateDate = product.CreateDate,
                ProductPrice = product.ProductPrice,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                EditDate = product.EditDate,
                ProductDescription = product.ProductDescription,
                ProductStock = product.ProductStock,
                Campaign = _campaignService.Get(product.ProductCampaignId).Data,
                Category = _categoryService.Get(product.ProductCategoryId).Data
            };

            return new SuccessDataResult<ProductResponse>(productResponse, "Ürün bilgisi getirildi.");
        }

        public IDataResult<ProductResponse> Add(ProductRequest data)
        {
 
            var entity = new Product()
            {
                ProductCategoryId = data.ProductCategoryId,
                ProductDescription = data.ProductDescription,
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now,
                ProductPrice = data.ProductPrice,
                ProductCampaignId = data.ProductCampaignId,
                ProductStatus = data.ProductStatus,
                ProductStock = data.ProductStock,
                ProductName = data.ProductName,
                ProductUrl = Guid.NewGuid().ToString()
            };

            
            _productRepository.Add(entity);

            var fileRequest = new ProductFileRequest()
            {
                ProductId = entity.ProductId,
                File = data.Files
            };

            _productFileService.Add(fileRequest);

            ProductResponse productResponse = new()
            {
                ProductStatus = entity.ProductStatus,
                ProductUrl = entity.ProductUrl,
                Files = _productFileService.GetByProductId(entity.ProductId).Data,
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
            var oldProduct = _productRepository.Get(p => p.ProductId == id);

            //foreach (var path in oldProduct.ProductImagePaths)
            //{
            //    if (File.Exists(path))
            //        File.Delete(path);
            //}


            string fileName = "";
            string fileExtension = "";
            string filePath = "";
            string[] paths = { };

            if (data.Files != null)
            {
                foreach (var file in data.Files)
                {
                    if (file.Length > 0)
                    {
                        fileExtension = Path.GetExtension(file.FileName);
                        fileName = Guid.NewGuid() + fileExtension;
                        filePath = Path.Combine("Files", fileName);
                        paths.Append(filePath);


                        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }


                    }

                }

            }




            var product = _productRepository.Get(p => p.ProductId == id);
            product.ProductCategoryId = data.ProductCategoryId;
            product.ProductDescription = data.ProductDescription;
            product.EditDate = DateTime.Now;
            product.ProductPrice = data.ProductPrice;
            product.ProductCampaignId = data.ProductCampaignId;
            product.ProductStatus = data.ProductStatus;
            //product.ProductImagePaths = paths;
            product.ProductStock = data.ProductStock;
            product.ProductName = data.ProductName;
            _productRepository.Update(product);
            return new SuccessResult("Ürün bilgileri güncellendi.");

        }

        public IResult Delete(int id)
        {
            var product = _productRepository.Get(p => p.ProductId == id);
            _productFileService.Delete(id);

            _productRepository.Delete(product);
            return new SuccessResult("Ürün Silindi.");

        }

    }
}
