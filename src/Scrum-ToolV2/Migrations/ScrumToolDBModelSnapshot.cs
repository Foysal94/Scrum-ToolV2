using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ASPNET5_Scrum_Tool.Models;

namespace ScrumToolV2.Migrations
{
    [DbContext(typeof(ScrumToolDB))]
    partial class ScrumToolDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Boards", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Columns", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BoardsID");

                    b.Property<string>("Name");

                    b.Property<int>("ParentBoardID");

                    b.HasKey("ID");

                    b.HasIndex("BoardsID");

                    b.ToTable("Columns");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Comments", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name");

                    b.Property<int>("ParentTaskID");

                    b.Property<int?>("TasksID");

                    b.HasKey("ID");

                    b.HasIndex("TasksID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Labels", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Colour");

                    b.Property<int>("ParentTaskID");

                    b.Property<int?>("TasksID");

                    b.HasKey("ID");

                    b.HasIndex("TasksID");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Tasks", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ColumnsID");

                    b.Property<DateTime>("DueDate");

                    b.Property<int>("ParentBoardID");

                    b.Property<int>("ParentColumnID");

                    b.Property<string>("ParentColumnName");

                    b.Property<string>("TaskContent");

                    b.HasKey("ID");

                    b.HasIndex("ColumnsID");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Columns", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Boards")
                        .WithMany("ColumnList")
                        .HasForeignKey("BoardsID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Comments", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Tasks")
                        .WithMany("CommentList")
                        .HasForeignKey("TasksID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Labels", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Tasks")
                        .WithMany("LabelList")
                        .HasForeignKey("TasksID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Tasks", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Columns")
                        .WithMany("TasksList")
                        .HasForeignKey("ColumnsID");
                });
        }
    }
}
