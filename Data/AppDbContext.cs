using Microsoft.EntityFrameworkCore;
using TodoTemplateService.Models;

namespace TodoTemplateService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){}

        public DbSet<TodoTemplate> todoTemplate { get; set; }

        public DbSet<Specialization> Specialization { get; set; }

        public DbSet<ProjectType> ProjectType { get; set; }
    }
}