using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.ViewModel
{
    public class ResponseResult<T>
    {
        public ResponseResult(bool status)
        {
            Status = status;
        }
        public ResponseResult(bool status,T data)
        {
            Status = status;
            Data = data;
        }
        public bool Status { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }

    }
}
