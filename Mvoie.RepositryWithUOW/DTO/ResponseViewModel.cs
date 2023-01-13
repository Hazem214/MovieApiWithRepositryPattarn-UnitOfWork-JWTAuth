using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvoie.RepositryWithUOW.DTO
{
    public class ResponseViewModel<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Issuccess { get; set; }

        public string ErrorCode { get; set; } = "0";
    }
}
