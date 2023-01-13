using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Movie.EF;
using Movie.Models;
using Mvoie.RepositryWithUOW.DTO;
using Mvoie.RepositryWithUOW.Helper;
using Mvoie.RepositryWithUOW.IRopositry;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mvoie.RepositryWithUOW.Repositry
{
    public class UserRepositry:IUserRepositry
    {
        public readonly IMapper _mapper;
        public readonly MovieContext _context;
        UserManager<User> _userManager;
        private readonly JWT _jwt;
        SignInManager<User> _signInManager;
        RoleManager<IdentityRole> _roleManeger;
        public UserRepositry(MovieContext context,IMapper mapper, SignInManager<User> signInManeger, UserManager<User> userManager, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManeger)
        {
            _context = context;
            _mapper = mapper;
            _signInManager = signInManeger;
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManeger = roleManeger;
        }

        public async Task<AuthInfoDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            if (await _userManager.FindByEmailAsync(registerDTO.Email) != null) return new AuthInfoDTO { Message = "This Email is Already Register " };

            if (await _userManager.FindByNameAsync(registerDTO.Email) != null) return new AuthInfoDTO { Message = "This User is Already Register " };

            User user = new User
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                UserName = registerDTO.UserName
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                var error = string.Empty;
                foreach (var erroritem in result.Errors)
                {
                    error += $"{erroritem.Description},";
                }

                return new AuthInfoDTO { Message = error };
            }

            await _userManager.AddToRoleAsync(user, "User");
            var token = await CreateJwtToken(user);

            return new AuthInfoDTO
            {
                Email = registerDTO.Email,
                IsAuth = true,
                Message = "User Created Successfuly",
                Roles = new List<string> { "User" },
                ExpirationDate = token.ValidTo,
                UserName = registerDTO.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };


        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<AuthInfoDTO> SignInAsync(SignInDTO signInDTO)
        {
            AuthInfoDTO authInfoDTO = new AuthInfoDTO();

            var user = await _userManager.FindByNameAsync(signInDTO.UserName);
            if (user == null ||! await _userManager.CheckPasswordAsync(user, signInDTO.Password))
            {
                authInfoDTO.Message = "UserName Or password Not Correct ";
                return authInfoDTO;
            }

            var token = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            authInfoDTO.Message = " Sign in Successfully";
            authInfoDTO.IsAuth = true;
            authInfoDTO.ExpirationDate = token.ValidTo;
            authInfoDTO.UserName=signInDTO.UserName;
            authInfoDTO.Email = user.Email;
            authInfoDTO.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authInfoDTO.Roles = roles.ToList();


            return authInfoDTO;
        }

        public async Task<string> AddRoleToUser(AddRoleToUserDTO addRoleToUserDTO)
        {
            var user= await _userManager.FindByNameAsync(addRoleToUserDTO.UserName);
            if (user == null) return "This User Not exist";
            if (!await _roleManeger.RoleExistsAsync(addRoleToUserDTO.RoleName)) return "This Role Not Exist    ";
            if (await _userManager.IsInRoleAsync(user,addRoleToUserDTO.RoleName)) return "this User Already Asign To This Role";
            var result= await _userManager.AddToRoleAsync(user, addRoleToUserDTO.RoleName);
            return result.Succeeded ? string.Empty : "Some thing went Wrong";
        }
    }
}
