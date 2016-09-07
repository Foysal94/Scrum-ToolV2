using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ASPNET5_Scrum_Tool.Models;

namespace ScrumToolV2.Migrations
{
    [DbContext(typeof(ScrumToolDB))]
    [Migration("20160907211126_InitalMigrations")]
    partial class InitalMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Name");

                    b.Property<int>("ParentBoardID");

                    b.HasKey("ID");

                    b.HasIndex("ParentBoardID");

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

                    b.HasKey("ID");

                    b.HasIndex("ParentTaskID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Labels", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Colour");

                    b.Property<int>("ParentTaskID");

                    b.HasKey("ID");

                    b.HasIndex("ParentTaskID");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Tasks", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DueDate");

                    b.Property<int>("ParentBoardID");

                    b.Property<int>("ParentColumnID");

                    b.Property<string>("ParentColumnName");

                    b.Property<string>("TaskContent");

                    b.HasKey("ID");

                    b.HasIndex("ParentColumnID");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Columns", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Boards")
                        .WithMany("ColumnList")
                        .HasForeignKey("ParentBoardID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Comments", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Tasks")
                        .WithMany("CommentList")
                        .HasForeignKey("ParentTaskID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Labels", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Tasks")
                        .WithMany("LabelList")
                        .HasForeignKey("ParentTaskID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Tasks", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Columns")
                        .WithMany("TasksList")
                        .HasForeignKey("ParentColumnID");
                });
        }
    }
}
