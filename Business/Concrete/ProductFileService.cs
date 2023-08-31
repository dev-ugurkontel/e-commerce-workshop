using Business.Abstract;
using Core.Utils.Results;
using DataAccess.EF.Abstract;
using DataAccess.EF.Concrete;
using Entities.Entity;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductFileService : IProductFileService
    {
        private readonly ProductFileRepositoryBase _productFileRepository;

        
        public ProductFileService(ProductFileRepositoryBase productFileRepository)
        {
            _productFileRepository = productFileRepository;
        }

        public IDataResult<List<ProductFileResponse>> Add(ProductFileRequest data)
        {
            string fileName = "";
            string fileExtension = "";
            string filePath = "";

            if(data.File == null)
            {
                return new ErrorDataResult<List<ProductFileResponse>>(default, "Fotoğralar getirilemedi.");
            }

            List<ProductFile> addProductFiles = new();

            if (data.File != null)
            {
                foreach (var file in data.File)
                {
                    if(file.Length > 0)
                    {

                        fileExtension = Path.GetExtension(file.FileName);
                        fileName = Guid.NewGuid() + fileExtension;
                        filePath = Path.Combine("B:/merga-soft/staj/ws1/ECommerceWSFrontend/src/assets/images", fileName); //değişecek
                        ProductFile newFile = new()
                        {
                            ProductId = data.ProductId,
                            FileName = fileName,
                            FileExtension = fileExtension,
                            ProductImagePath = filePath,
                            CreateDate = DateTime.Now,
                            EditDate = DateTime.Now
                        };
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        addProductFiles.Add(newFile);
                    }
                }

            }       

            if(addProductFiles.Count > 0)
            {
                _productFileRepository.AddRange(addProductFiles);
            }


            var productFileResponse = _productFileRepository.GetAll().Select(f => new ProductFileResponse()
            {
                ProductId = f.ProductId,
                FileExtension = f.FileExtension,
                FileName = f.FileName,
                ProductImagePath = f.ProductImagePath,
                CreateDate = f.CreateDate,
                EditDate = f.EditDate
            }).ToList();

            return new SuccessDataResult<List<ProductFileResponse>>(productFileResponse, "Dosyalar başarıyla getirildi.");

        }

        public IResult Delete(int id)
        {
            var files = _productFileRepository.GetAll(f => f.ProductId == id);
            foreach (var file in files)
            {
                if (File.Exists(file.ProductImagePath))
                    File.Delete(file.ProductImagePath);


            }
            files?.ForEach(x => _productFileRepository.Delete(x));

            return new SuccessResult();

        }


        public IDataResult<List<ProductFileResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }


        public IResult Update(int id, ProductFileRequest data)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<ProductFileResponse>> GetByProductId(int id)
        {

            var productFileList = _productFileRepository.GetAll(p => p.ProductId == id).Select(p => new ProductFileResponse()
            {
                ProductId = p.ProductId,
                CreateDate = p.CreateDate,
                FileExtension= p.FileExtension,
                FileName = p.FileName,
                ProductImagePath = p.ProductImagePath,
                EditDate = p.EditDate

            }).ToList();

            return new SuccessDataResult<List<ProductFileResponse>>(productFileList, "Ürüne ait tüm dosyalar getirildi.");
        }

        public IDataResult<List<ProductFileResponse>> GetAll(int page = 0, int pageSize = 0)
        {
            throw new NotImplementedException();
        }
    }
}
