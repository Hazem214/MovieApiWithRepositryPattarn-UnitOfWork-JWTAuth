using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Models;
using Mvoie.RepositryWithUOW;
using Mvoie.RepositryWithUOW.DTO;
using System.Xml.Linq;

namespace Movie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenresController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenresController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {

            return Ok(_unitOfWork.Genres.GetAll("Movies"));
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("GetById")]
        public IActionResult GetById(int Id)
        {
            if (ModelState.IsValid)
            {
                var Genre = _unitOfWork.Genres.GetById( i => i.Id == Id, "Movies");
                if (Genre != null)
                    return Ok(Genre);

                return NotFound($"No Genre Was Found With This ID : {Id}");

            }

                return BadRequest();
        }
        [HttpGet("GetByName/{Name:alpha}")]
        public IActionResult GetByName(String Name)
        {
         
            if (ModelState.IsValid)
            {
                var genre = _unitOfWork.Genres.GetByName(i => i.Name == Name, new[] { "Movies" } );
                if (genre == null) return NotFound($"No Genre Was Found With This Name : {Name}");

                return Ok(genre);
            }
            return BadRequest();
           
        }
        [HttpPost("Add")]
        public IActionResult Add(GenreDTO Entity)
        {
           var Genre= _unitOfWork.Genres.Add(Entity);
            _unitOfWork.Complete();
            return Ok(Genre);
        }

        [HttpPut("Update")]
        public IActionResult Update(int id , GenreDTO genre)
        {
         var NewGenre=   _unitOfWork.Genres.Update(id, genre);
            _unitOfWork.Complete();
            return Ok(NewGenre);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int Id)
        {
            var result=_unitOfWork.Genres.Delete(Id);
            _unitOfWork.Complete();
            return Ok(result);
        }
    }
}
