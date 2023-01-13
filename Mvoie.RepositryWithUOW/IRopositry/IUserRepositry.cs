using Mvoie.RepositryWithUOW.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvoie.RepositryWithUOW.IRopositry
{
    public interface IUserRepositry
    {
        Task<AuthInfoDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<AuthInfoDTO> SignInAsync(SignInDTO signInDTO);
        Task<string> AddRoleToUser(AddRoleToUserDTO addRoleToUserDTO);
    }
}
