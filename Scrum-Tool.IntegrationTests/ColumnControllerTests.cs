using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity.Migrations.Operations;
using ASPNET5_Scrum_Tool.Controllers;
using Xunit;

namespace Scrum_Tool.IntegrationTests
{
    public class ColumnControllerTests : Setup
    {
        private ColumnController m_ColumnsController;

        public ColumnControllerTests() : base() //Arrange
        {
            m_ColumnsController = new ColumnController(m_Context);
        }

      
  
    }

}
