using Azure.Core;
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
    public interface IProductService
    {
        IDataResult<List<ProductResponse>> GetByCategoryId(int id);
        IDataResult<PageResponse<ProductResponse>> GetAll(int page = 0, int pageSize = 0);
        //IDataResult<List<TResponse>> GetAll();
        IDataResult<ProductResponse> Get(int id);
        IDataResult<ProductResponse> Add(ProductRequest data);
        IResult Update(int id, ProductRequest data);
        IResult Delete(int id);
    }
}
