using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ASPNET5_Scrum_Tool.Models;

namespace ASPNET5ScrumTool.Migrations
{
    [DbContext(typeof(ScrumToolDB))]
    partial class ScrumToolDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Comments", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name");

                    b.Property<int>("TaskID");

                    b.Property<int?>("TasksID");

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

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Comments", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Tasks")
                        .WithMany()
                        .HasForeignKey("TasksID");
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
