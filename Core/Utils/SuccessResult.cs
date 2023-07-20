using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message): base(ResultStatus.Success,message)
        {
                
        }

        public SuccessResult():base(ResultStatus.Success)
        {
                
        }
    }
}
