using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CusJsonResult
    {
        public bool Success { get; set; } = false;
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public CusJsonResult(bool success,string errorCode,string errorMessage)
        {
            Success = success;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }

    public class CusJsonResult<T>
    {
        public bool Success { get; set; } = false;
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }


        public CusJsonResult(bool success, string errorCode, string errorMessage)
        {
            Success = success;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public CusJsonResult(bool success, string errorCode, string errorMessage, T data)
        {
            Success = success;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Data = data;
        }
    }
}
