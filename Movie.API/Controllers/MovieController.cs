using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvoie.RepositryWithUOW;
using Mvoie.RepositryWithUOW.DTO;

namespace Movie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Movies.GetAll("Gernre"));
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int Id)
        {
            var movie=_unitOfWork.Movies.GetById(i=>i.Id==Id, "Gernre");
            if (movie != null)
            {
                return Ok(movie);
            }
            return NotFound($"No Movie Was Found With This ID : {Id}");

        }
        [HttpGet("GetByName/{Title:alpha}")]
        public IActionResult GetByName(string Title)
        {
            var movie=_unitOfWork.Movies.GetByName(i => i.Title == Title, new[] { "Gernre" }  );
            if(movie != null)
            {
                return Ok(movie);

            }
            return NotFound($"No Movie Was Found With This Title : {Title}");
        }
        [HttpPost("Add")]
        public IActionResult Add([FromForm]MovieDTO movieDTO)
        {
            if (ModelState.IsValid)
            {
                var Movie = _unitOfWork.Movies.Add(movieDTO);
                _unitOfWork.Complete();
                return Ok(Movie);
            }
            return BadRequest();
           
        }
        [HttpPut("Update/{Id:int}")]
        public IActionResult Update(int Id, [FromForm] MovieDTO MovieDTO)
        {
          var movie=  _unitOfWork.Movies.Update(Id, MovieDTO);
            _unitOfWork.Complete();
            return Ok(movie);
        }

        [HttpDelete("Deleted/{Id:int}")]
        public IActionResult Deleted(int Id)
        {
            var movie = _unitOfWork.Movies.Delete(Id);
            _unitOfWork.Complete();
            return Ok(movie);
        }

    }
}
