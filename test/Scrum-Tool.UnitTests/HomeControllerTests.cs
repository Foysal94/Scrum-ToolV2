using System.Linq;
using ASPNET5_Scrum_Tool.Controllers;
using ASPNET5_Scrum_Tool.Models;
using FluentAssertions;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Xunit;

namespace Scrum_Tool.UnitTests
{
    public class HomeControllerTests : Setup
    {
        private HomeController m_HomeController;
       
        //private DbContextOptionsBuilder<ScrumToolDB> builder;

        public HomeControllerTests() : base() //Arange
        {
            var db = new DbContextOptionsBuilder();
            db.UseInMemoryDatabase();
            m_Context = new ScrumToolDB(db.Options);
            CreateTestData(m_Context);
            m_HomeController = new HomeController(m_Context);
        }

        [Fact]
        public void SumbitBoardFormShouldReturnIndexOnErrors()
        {
            //arange
            m_HomeController.ModelState.AddModelError("", "dummy error");

            // Act
            var actionResult = m_HomeController.SumbitBoardForm(new Boards()) as RedirectToActionResult;
            // Assert
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Index");

        }

        [Fact]
        public void RedirectToLoadWhenBoardNameFound()
        {
            // Act
            var board = m_Context.Boards.First(); // Returns "HelloWorld"
            var actionResult =  m_HomeController.SumbitBoardForm(board) as RedirectToActionResult;
            
            // Assert
            board.Should().NotBeNull();
            actionResult.Should().BeOfType<RedirectToActionResult>()
                .Which.ActionName.Should().Be("Load");

        }

        /*
        [Fact]
        public void RedirectToCreateIfBoardNameNotFound()
        {
            var board = new Boards("Test");
            var actionResult = m_HomeController.SumbitBoardForm(board) as RedirectToActionResult;
            board.Should().NotBeNull();
            actionResult.Should().BeOfType<RedirectToActionResult>()
                .Which.ActionName.Should().Be("Create");
        }
        */




    }
}
