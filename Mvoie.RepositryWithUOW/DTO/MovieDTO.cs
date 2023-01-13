using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvoie.RepositryWithUOW.DTO
{
    public class MovieDTO
    {

        public string Title { get; set; }

        public int Year { get; set; }
        public double Rate { get; set; }
        public string StoreLine { get; set; }
        public IFormFile Image { get; set; }
        public int GenreId { get; set; }

    }
}
