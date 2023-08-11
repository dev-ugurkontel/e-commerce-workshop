using Core.Utils.Results;

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
