using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET5_Scrum_Tool.Controllers;
using ASPNET5_Scrum_Tool.Models;
using FluentAssertions;
using Xunit;
using Moq;
using GenFu;

namespace Scrum_Tool.UnitTests
{

    public interface ITestRespository<T> 
    {
        void Add(T model);
        void Remove(T model);
        void Edit(T model);
        void Delete(T model);
        T FindById(int id);
        T FindObject(T model);
        void ReturnCount();
        IQueryable<T> ReturnAll();
        void Save();

    }
    
    public class ColumnControllerTests 
    {
        private const int m_BoardID = 0;
		  private ScrumToolDB m_ScrumToolDB;
		  private ColumnController m_ColumnController;
        public ColumnControllerTests() 
        {
			  var dbOptions = CreateFakeDatabaseOptions();
			  m_ScrumToolDB = new ScrumToolDB(dbOptions);
			  m_ColumnController = new ColumnController(m_ScrumToolDB);
        }

        private IQueryable<Columns> GenerateMockData() 
        {
            int id = 0;
            int numberOfColumns = 3;
            string Name = "TestColumn";

            A.Configure<Columns>()
                .Fill(c => c.ID,
                     () => { return id++; })
                .Fill(c => c.Name, () => { return $"{Name}{id++}"; })
                .Fill(c => c.BoardID, m_BoardID);

            var columns = A.ListOf<Columns>(numberOfColumns);
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

    }
}

