using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ASPNET5_Scrum_Tool.Models;

namespace ASPNET5ScrumTool.Migrations
{
    [DbContext(typeof(ScrumToolDB))]
    [Migration("20160313233201_InitalMigration")]
    partial class InitalMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Boards", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Columns", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BoardID");

                    b.Property<int?>("BoardsID");

                    b.Property<string>("Name");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Labels", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Colour");

                    b.Property<int>("TaskID");

                    b.Property<int?>("TasksID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Tasks", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BoardID");

                    b.Property<string>("ColumnName");

                    b.Property<int?>("ColumnsID");

                    b.Property<DateTime>("DueDate");

                    b.Property<string>("TaskContent");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Columns", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Boards")
                        .WithMany()
                        .HasForeignKey("BoardsID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Labels", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Tasks")
                        .WithMany()
                        .HasForeignKey("TasksID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Tasks", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Columns")
                        .WithMany()
                        .HasForeignKey("ColumnsID");
                });
        }
    }
}
