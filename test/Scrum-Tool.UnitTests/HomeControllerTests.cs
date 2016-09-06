using System.Linq;
using ASPNET5_Scrum_Tool.Controllers;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Xunit;

namespace Scrum_Tool.UnitTests
{ 
    [Collection("ScrumToolDB Collection")]
    public class HomeControllerTests 
    {
        HomeController m_HomeController;
        private ScrumToolDBFixture m_ScrumToolDBFixture;
		private ScrumToolDB m_ScrumToolDBContext;

        public HomeControllerTests(ScrumToolDBFixture p_ScrumToolDBFixture)
		{
			m_ScrumToolDBFixture = p_ScrumToolDBFixture;
			m_ScrumToolDBContext = m_ScrumToolDBFixture.ScrumToolDB;
            m_HomeController = new HomeController(m_ScrumToolDBContext);
        }

        [Fact]
        public void Redirect_To_Create_If_Board_Does_Not_Exist()
        {
            //Act
            Boards newBoard =  new Boards("New Board to Create");
            //Arrange
            RedirectToActionResult result =  m_HomeController.SumbitBoardForm(newBoard)
                                                as RedirectToActionResult;

            //Assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("Load");
            result.ControllerName.Should().Be("Board");
                
        }
        
        [Fact]
        public void Redirect_To_Load_Action_If_Board_Exists()
        {
            //Act
            Boards loadedBoard = m_ScrumToolDBContext.Boards.First();
            //Arrange
            RedirectToActionResult result =  m_HomeController.SumbitBoardForm(loadedBoard)
                                                as RedirectToActionResult;
            
            //Assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("Load");
            result.ControllerName.Should().Be("Board");

        }


    }
}
