using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class ClsResponseModel
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ClsResponseModel<T> : ClsResponseModel
    {
        public T Data { get; set; }
    }
}
