using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvoie.RepositryWithUOW.DTO
{
    public class AuthInfoDTO
    {
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public bool IsAuth { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
