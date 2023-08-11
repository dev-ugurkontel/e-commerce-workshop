using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, ResultStatus.Error, message)
        {

        }

        public ErrorDataResult(T data) : base(data, ResultStatus.Error)
        {

        }
    }
}
