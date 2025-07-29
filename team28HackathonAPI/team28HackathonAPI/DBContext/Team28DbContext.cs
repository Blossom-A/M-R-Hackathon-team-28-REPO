using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using team28HackathonAPI.Models;

namespace team28HackathonAPI.DBContext
{
    public class Team28DbContext : DbContext
    {
        public Team28DbContext(DbContextOptions<Team28DbContext> options) : base(options) {}
        
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<DiagnosticTest> DiagnosticTests { get; set; }
        public DbSet<MonitoredDestination> MonitoredDestinations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
