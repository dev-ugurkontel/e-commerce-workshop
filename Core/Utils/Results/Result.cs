using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.Results
{
    public class Result : IResult
    {
        public Result(ResultStatus status, string message) : this(status)
        {
            Message = message;
        }


        public Result(ResultStatus status)
        {
            Status = status;
        }



        public ResultStatus Status { get; }

        public string Message { get; set; }
    }
}
