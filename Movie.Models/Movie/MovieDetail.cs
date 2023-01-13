using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Models
{
    public class MovieDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int Year { get; set; }
        public double Rate { get; set; }
        public string StoreLine { get; set; }
        public byte[]  Image { get; set; }
        public string ImageName { get; set; }

        [ForeignKey("Gernre")]
        public int GenreId { get; set; }

        //Navigation Proparity
        public Genre Gernre { get; set; }


    }
}
