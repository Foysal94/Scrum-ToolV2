using Microsoft.Data.Entity;

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
        public DbSet<Boards> Boards { get; set; }

        public DbSet<Columns> Columns { get; set; }

        public DbSet<Tasks> Tasks { get; set; }
    }
}
