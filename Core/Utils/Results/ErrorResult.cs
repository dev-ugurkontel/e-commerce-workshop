using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(ResultStatus.Error, message)
        {

        }

        public ErrorResult() : base(ResultStatus.Error)
        {

        }
    }
}
