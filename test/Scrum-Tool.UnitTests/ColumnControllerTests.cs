using System.Linq;
using ASPNET5_Scrum_Tool.Controllers;
using ASPNET5_Scrum_Tool.Models;
using FluentAssertions;
using Microsoft.Data.Entity;
using Xunit;

namespace Scrum_Tool.UnitTests
{
    public class ColumnControllerTests : Setup
    {
        private ColumnController m_ColumnController;
       
        // private DbContextOptionsBuilder<ScrumToolDB> builder;

        public ColumnControllerTests()
        {
            //Arrange
            var db = new DbContextOptionsBuilder();
            db.UseInMemoryDatabase();
            m_Context = new ScrumToolDB(db.Options);
            CreateTestData(m_Context);
            m_ColumnController = new ColumnController(m_Context);

        }

        [Fact]
        public void ColumnNameChanged()
        {
            // Act
            var column = m_Context.Columns.First();
            m_ColumnController.ChangeColumnName(column.Name, "Test", 1);
            // Assert
            column.Should().NotBeNull();
            column.Name.Should().Be("Test");
        }
        
        [Fact]
        public void ColumnAdded()
        {
            // Act
            var column = new Columns("TestColumnToAdd", 1);
            var viewComponentResult = m_ColumnController.AddColumn(column);
            //column.ID = m_Context.Columns.Last().ID;
            // Assert
            viewComponentResult.Should().NotBeNull();
            m_Context.Columns.Should().Contain(column)
                .Which.Name.Should().Be("TestColumnToAdd");


        }

        [Fact]
        public void ColumnDeleted()
        {
            int firstColumnID = m_Context.Columns.First().ID;
            m_ColumnController.Delete(firstColumnID);
            m_Context.Columns.First().ID.Should().NotBe(firstColumnID);
        }           


    }

}

