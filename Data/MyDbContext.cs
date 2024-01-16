using System;
using HMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HMS.Data
{
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }
        // List of Tables to be operated by DbContext

        public DbSet<HMS.Models.ContactUs>? ContactUs { get; set; }
        public DbSet<HMS.DTO.RoleStore>? RoleStore { get; set; }
        public DbSet<HMS.Models.Doctor>? Doctor { get; set; }
        public DbSet<HMS.Models.Patient>? Patient { get; set; }
        public DbSet<HMS.Models.Booking>? Booking { get; set; }
        // Join-Table Queries DTO are also registered here

        // ModelBuilder: 
    }
}

