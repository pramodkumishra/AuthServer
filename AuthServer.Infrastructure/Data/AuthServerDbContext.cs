using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Infrastructure.Data
{
    public class AuthServerDbContext : IdentityDbContext<User>
    {
        public AuthServerDbContext(DbContextOptions<AuthServerDbContext> options) : base(options) { 

        }
        

        
    }
}