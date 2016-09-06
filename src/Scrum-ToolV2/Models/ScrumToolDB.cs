using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ASPNET5_Scrum_Tool.Models
{
    public class ScrumToolDB : DbContext
    {
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boards>(entity =>
            {
                entity.HasMany(b => b.ColumnList).WithOne(c => c.ParentBoard).HasForeignKey("BoardID");
            });

            modelBuilder.Entity<Columns>(entity =>
            {
                entity.HasMany(c => c.TasksList).WithOne(t => t.ParentColumn).HasForeignKey("ColumnName");
            });

    
        }
        */

        public ScrumToolDB(DbContextOptions<ScrumToolDB> options) 
            : base(options)
        {
            
        }
        public DbSet<Boards> Boards { get; set; }

        public DbSet<Columns> Columns { get; set; }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<Labels> Labels { get; set; } 
        
        public DbSet<Comments> Comments { get; set; } 

    }
}
