using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public  class DataResult<T> : Result, IDataResult<T>
    {
        public T Data { get; set; }

        public DataResult(T data,ResultStatus status, string message):base(status,message)
        {
            Data = data;
        }

        public DataResult(T data , ResultStatus status): base(status)
        {
            Data = data;
        }

    }
}
