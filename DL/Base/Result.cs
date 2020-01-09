using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Base
{
    public class Result<T>
    {
        public Result()
        {

        }

        public T Data
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public bool Success
        {
            get;
            set;
        }

        public void Pass()
        {
            Success = true;
        }

        public void Pass(T data)
        {
            Data = data;
            Success = true;
        }

        public void Fail(Exception ex)
        {
            Fail(ex.Message);
        }

        public void Fail(string error)
        {
            Message = error;
            Success = false;
        }
    }

    public class ActionResult : Result<string>
    {

    }

    public class DynamicListResult : Result<dynamic>
    {
    }
}
