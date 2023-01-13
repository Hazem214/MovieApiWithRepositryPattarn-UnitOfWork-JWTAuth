using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvoie.RepositryWithUOW.DTO
{
    public class MovieWithGenreDTO
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public int Year { get; set; }
        public double Rate { get; set; }
        public string StoreLine { get; set; }
        public byte[] Image { get; set; }
        public string ImageName { get; set; }

        public int GenreId { get; set; }
        public string Genre { get; set; }

    }
}
