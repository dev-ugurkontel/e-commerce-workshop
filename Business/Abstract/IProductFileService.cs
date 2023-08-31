using Azure;
using Core.Business.Abstract;
using Core.Utils.Results;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductFileService 
    {
        IDataResult<List<ProductFileResponse>> GetByProductId(int id);
        IDataResult<List<ProductFileResponse>> GetAll(int page = 0, int pageSize = 0);
        IDataResult<List<ProductFileResponse>> Get(int id);
        IDataResult<List<ProductFileResponse>> Add(ProductFileRequest data);
        IResult Update(int id, ProductFileRequest data);
        IResult Delete(int id);
    }
}
