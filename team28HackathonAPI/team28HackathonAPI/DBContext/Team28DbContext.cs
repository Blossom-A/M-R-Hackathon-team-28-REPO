using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using team28HackathonAPI.Models;

namespace team28HackathonAPI.DBContext
{
    public class Team28DbContext : IdentityDbContext<AppUser>
    {
        public Team28DbContext(DbContextOptions<Team28DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
