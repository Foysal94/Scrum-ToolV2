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
				new Columns("TestColumn0", m_BoardID),
				new Columns("TestColumn1", m_BoardID),
				new Columns("TestColumn2", m_BoardID), 
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
			m_ScrumToolDB.SaveChanges();
			m_ColumnController = new ColumnController(m_ScrumToolDB);
		}

		[Fact]
		public void DeleteColumn()
		{
			// Arrange
			Columns testColumn = m_ScrumToolDB.Columns.Last();
			// Act
			m_ColumnController.Delete(testColumn.ID);
			// Assert
			m_ScrumToolDB.Columns.Should().HaveCount(2, "Inital column list count is 3, and deleted one")
					.And.NotContain(testColumn);
					 
		}

		[Fact]
		public void AddColumn()
		{
			// Arrange
			Columns testColumn = new Columns("Something", m_BoardID);
			//Act
			m_ColumnController.AddColumn(testColumn);
			// Assert
			m_ScrumToolDB.Columns.Should().NotBeNullOrEmpty()
					.And.HaveCount(4, "Number of inital columns created plus one");
			m_ScrumToolDB.Columns.Last().ShouldBeEquivalentTo(testColumn, options =>
					options.Excluding(c => c.ID));
		}



	 }

}

