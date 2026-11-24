using Microsoft.EntityFrameworkCore;
using Clinic.API.Features.Posts;
using Clinic.API.Entities;
using Clinic.API.Features.Auth.Models;

namespace Clinic.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ClinicPost> ClinicPosts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<StaffUser> StaffUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}