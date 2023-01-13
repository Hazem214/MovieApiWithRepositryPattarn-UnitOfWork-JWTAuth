using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvoie.RepositryWithUOW;
using Mvoie.RepositryWithUOW.DTO;

namespace Movie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
            var RegisterInfo=    await _unitOfWork.Users.RegisterAsync(registerDTO);
                if(RegisterInfo.IsAuth)
                   return Ok(RegisterInfo);

                return BadRequest();

            }
            return BadRequest();

        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync(SignInDTO signInDTO)
        {
            if (ModelState.IsValid)
            {
                var signInInfo=await _unitOfWork.Users.SignInAsync(signInDTO);
                if(signInInfo.IsAuth)return Ok(signInInfo);

                return BadRequest(signInInfo);
            }
            return BadRequest(ModelState);

        }

        [HttpPost("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUserDTO(AddRoleToUserDTO addRoleToUserDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _unitOfWork.Users.AddRoleToUser(addRoleToUserDTO);
                if (string.IsNullOrEmpty(result)) return Ok(addRoleToUserDTO);

                return BadRequest(result); 

            }
            return BadRequest(ModelState);
        }
    }
}
