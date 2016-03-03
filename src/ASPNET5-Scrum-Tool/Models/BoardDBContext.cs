using Microsoft.Data.Entity;

namespace ASPNET5_Scrum_Tool.Models
{
    public class BoardDBContext : DbContext
    {
        public DbSet<BoardModel> Boards { get; set; }

        //public DbSet<ColumnModel> Columns { get; set; }

        //public DbSet<TaskModel> Tasks { get; set; }
    }
}
