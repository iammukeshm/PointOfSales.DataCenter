using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSales.DataCenter.Application.DTOs
{
    public class Result<T>
    {
        public Result()
        {

        }
        internal Result(bool succeeded, IEnumerable<string> errors,T data)
        {
            Succeeded = succeeded;
            Messages = errors.ToArray();
            Data = data;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }

        public string[] Messages { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T>(true, new string[] { }, data);
        }
        public static Result<T> Success(string message,T data)
        {
            return new Result<T>(true, new string[] { message }, data);
        }

        public static Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T>(false, errors,default);
        }
        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, new string[] { error }, default);
        }
    }
}
