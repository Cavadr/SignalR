using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.DAL
{
    public class Context:IdentityDbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<AppUser> MyProperty { get; set; }
    }
}
