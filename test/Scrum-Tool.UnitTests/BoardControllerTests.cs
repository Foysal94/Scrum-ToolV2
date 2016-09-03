using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool;
using ASPNET5_Scrum_Tool.Controllers;
using ASPNET5_Scrum_Tool.Models;
using FluentAssertions;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Xunit;

namespace Scrum_Tool.UnitTests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class BoardControllerTests : Setup
    {
        private BoardController m_BoardController;
        public BoardControllerTests() 
        {
            var db = new DbContextOptionsBuilder();
            db.UseInMemoryDatabase();
            m_Context = new ScrumToolDB(db.Options);
            CreateTestData(m_Context);
            m_BoardController = new BoardController(m_Context);
  
        }


        [Fact]
        public void BoardIsLoaded()
        {
            var viewResult = m_BoardController.Load(1) as ViewResult;
            viewResult.Should().NotBeNull();
            viewResult.Should().BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().BeOfType<Boards>()
                .Which.Name.Should().Be("HelloWorld");

        }

        [Fact]
        public void BoardIsCreated()
        {
            var viewResult = m_BoardController.Create(2);
            viewResult.Should().NotBeNull();
            viewResult.Should().BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().BeOfType<Boards>();

        }

    }
    
}
