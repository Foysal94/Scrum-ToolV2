using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
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

                    b.Property<string>("Name");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Columns", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BoardID");

                    b.Property<string>("ColumnName")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Tasks", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BoardID");

                    b.Property<string>("ColumnName");

                    b.Property<string>("TaskContent");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Columns", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Boards")
                        .WithMany()
                        .HasForeignKey("BoardID");
                });

            modelBuilder.Entity("ASPNET5_Scrum_Tool.Models.Tasks", b =>
                {
                    b.HasOne("ASPNET5_Scrum_Tool.Models.Columns")
                        .WithMany()
                        .HasForeignKey("ColumnName")
                        .HasPrincipalKey("ColumnName");
                });
        }
    }
}
