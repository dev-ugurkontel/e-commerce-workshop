using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public class ExceptionResult : Result
    {
        public ExceptionResult(Exception exception, bool showException = true)
            : base(ResultStatus.Exception,
                showException == true ?
                    (exception != null ? "Exception: " + exception.Message +
                                       (exception.InnerException != null ? " | Inner Exception: " + exception.InnerException.Message +
                                                                          (exception.InnerException.InnerException != null ? " | " + exception.InnerException.InnerException.Message : "")
                                           : "")
                                       : "")
                    : "Exception")
        {

        }
        public ExceptionResult() : base(ResultStatus.Exception, "Exception")
        {

        }
    }
}
