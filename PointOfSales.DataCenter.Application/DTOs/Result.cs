using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSales.DataCenter.Application.DTOs
{
    public class Result<T>
    {
        internal Result(bool succeeded, IEnumerable<string> errors,T data)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Data = data;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T>(true, new string[] { }, data);
        }

        public static Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T>(false, errors,default);
        }
    }
}
