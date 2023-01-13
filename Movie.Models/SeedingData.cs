using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Models
{
   public static class SeedingData
    {
        public static void SeedData(this ModelBuilder Builder) {

            Builder.Entity<Genre>().HasData(new Genre
            {
                Id = 1,
                Name="Action"

            });

            Builder.Entity<IdentityRole>().HasData(new[] {
             new IdentityRole {Id=Guid.NewGuid().ToString(),Name="User",NormalizedName="User".ToUpper(),ConcurrencyStamp=Guid.NewGuid().ToString() }
            ,new IdentityRole { Id=Guid.NewGuid().ToString(),Name="Admin",NormalizedName="Admin".ToUpper(),ConcurrencyStamp=Guid.NewGuid().ToString()}
            } 
            );

        }
    }
}
