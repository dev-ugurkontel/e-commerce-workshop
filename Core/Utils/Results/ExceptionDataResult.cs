using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.Results
{
    public class ExceptionDataResult<T> : DataResult<T>
    {
        public ExceptionDataResult(T data, Exception exception, bool showException = true)
            : base(data, ResultStatus.Exception,
                showException == true ?
                    exception != null ? "Exception: " + exception.Message +
                                         (exception.InnerException != null ? " | Inner Exception: " + exception.InnerException.Message +
                                                                             (exception.InnerException.InnerException != null ? " | " + exception.InnerException.InnerException.Message : "")
                                             : "")
                        : ""
                    : "Exception"
                )
        {

        }
        public ExceptionDataResult() : base(default, ResultStatus.Exception, "Exception")
        {

        }


    }
}
