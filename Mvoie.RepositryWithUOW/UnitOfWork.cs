using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Movie.EF;
using Movie.Models;
using Mvoie.RepositryWithUOW.Helper;
using Mvoie.RepositryWithUOW.IRopositry;
using Mvoie.RepositryWithUOW.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mvoie.RepositryWithUOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;
       

        public IGenreRepositry Genres { get; set; }
        public IMovieRepositry Movies { get; set; }
        public IUserRepositry Users { get; set; }


     

        public UnitOfWork( MovieContext context, IMapper mapper, IOptions<JWT> jwt,UserManager<User> userManager,SignInManager<User > signInManager,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            Genres = new GenreRepositry(_context,_mapper);
            Movies=new MovieRepositry(_context,_mapper);
            Users = new UserRepositry(_context, _mapper,signInManager,userManager,jwt,roleManager);

        }
       
      
        public int Complete()
        {
             return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
