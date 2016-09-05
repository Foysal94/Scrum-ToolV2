using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using ASPNET5_Scrum_Tool.Controllers;
using ASPNET5_Scrum_Tool.Models;
using FluentAssertions;
using Xunit;
using Moq;
using GenFu;

namespace Scrum_Tool.UnitTests
{

    public class ColumnControllerTests 
    {
		  private const int m_BoardID = 0;
		  private ScrumToolDB m_ScrumToolDB;
		  private ColumnController m_ColumnController;

		  private IQueryable<Columns> GenerateTestData() 
          {
            /*
            int id = 0;
            int numberOfColumns = 3;
            string Name = "TestColumn";
            
            A.Configure<Columns>()
                .Fill(c => c.ID,() => { return id++; })
                .Fill(c => c.Name, () => { return $"{Name}{id}"; })
                .Fill(c => c.BoardID,() => { return m_BoardID; });
            var columns = A.ListOf<Columns>(numberOfColumns);
            return columns.AsQueryable();
            */

              List<Columns> columns = new List<Columns>()
              {
                  new Columns("TestColumn0", m_BoardID) {ID = 0},
                  new Columns("TestColumn1", m_BoardID) {ID = 1},
                  new Columns("TestColumn2", m_BoardID) {ID = 2},
              };

              return columns.AsQueryable();
          }

        private DbContextOptions<ScrumToolDB> CreateFakeDatabaseOptions() 
		  {
			  // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ScrumToolDB>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
		  }

        public ColumnControllerTests() 
        {
			  var dbOptions = CreateFakeDatabaseOptions();
			  var testColumnList = GenerateTestData();

			  m_ScrumToolDB = new ScrumToolDB(dbOptions);
			  m_ScrumToolDB.Columns.AddRange(testColumnList);
			  m_ColumnController = new ColumnController(m_ScrumToolDB);
        }





    }
}

