using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvoie.RepositryWithUOW.DTO
{
    public class GenreWithMovie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Movies { get; set; } = new List<string>();


    }
}
