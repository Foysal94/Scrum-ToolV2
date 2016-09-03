using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool;
using ASPNET5_Scrum_Tool.Controllers;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNet.TestHost;
using Microsoft.Data.Entity;
using Xunit;

namespace Scrum_Tool.IntegrationTests
{
    public class BoardControllerTests :Setup
    {
        private BoardController m_BoardController;
        private ScrumToolDB m_Context;

        public BoardControllerTests()
        {
            // Arrange
            var db = new DbContextOptionsBuilder();
            db.UseInMemoryDatabase();
            m_Context = new ScrumToolDB(db.Options);
            CreateTestData(m_Context);
            m_BoardController= new BoardController(m_Context);

            m_Server = new TestServer(TestServer.CreateBuilder()
                .UseStartup<Startup>());
             m_Client = m_Server.CreateClient();

        }

        [Fact]
        public void ReturnLoadBoard()
        {
            var response = m_Client.GetAsync("/Board/Load/1");

        }
        
    }
}
