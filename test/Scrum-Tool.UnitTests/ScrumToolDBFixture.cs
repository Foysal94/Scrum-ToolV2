using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ASPNET5_Scrum_Tool.Models;
using Xunit;

namespace Scrum_Tool.UnitTests
{
	
	public class ScrumToolDBFixture 
	{
		private ScrumToolDB m_ScrumToolDB;
		private const int m_FirstBoardID = 1;
		private const int m_FirstTaskID = 1;
		private const string m_FirstColumnName = "TestColumn1";
		public ScrumToolDB ScrumToolDB { get { return m_ScrumToolDB;} }
		public int FirstBoardID { get {return m_FirstBoardID;} }
		public int FirstTaskID { get {return m_FirstTaskID;} }
		public string ColumnName { get {return m_FirstColumnName;} }
		
		public ScrumToolDBFixture() 
		{
			var dbOptions = CreateFakeDatabaseOptions();
			m_ScrumToolDB = new ScrumToolDB(dbOptions);
			AddTestData();
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
		private void AddTestData()
		{
			var boardData = CreateBoardData();
			var columnData = CreateColumnData();
			var taskData = CreateTaskData();

			m_ScrumToolDB.Boards.AddRange(boardData);
			m_ScrumToolDB.Columns.AddRange(columnData);
			m_ScrumToolDB.Tasks.AddRange(taskData);
			m_ScrumToolDB.SaveChanges();
		}
		private IQueryable<Boards> CreateBoardData()
		{
			List<Boards> board = new List<Boards>()
			{
				new Boards("TestBoard1")
			};

			return board.AsQueryable();
		}
		private IQueryable<Columns> CreateColumnData()
		{
			List<Columns> columns = new List<Columns>()
			{
				new Columns(m_FirstColumnName, m_FirstBoardID),
				new Columns("TestColumn2", m_FirstBoardID),
				new Columns("TestColumn3", m_FirstBoardID), 
			};

			return columns.AsQueryable();
		}
		private IQueryable<Tasks> CreateTaskData()
		{
			List<Tasks> tasks = new List<Tasks>()
			{
				new Tasks(m_FirstBoardID, m_FirstColumnName, "TaskContent1"),
				new Tasks(m_FirstBoardID, m_FirstColumnName, "TaskContent2"),
				new Tasks(m_FirstBoardID, m_FirstColumnName, "TaskContent3"),
			};

			return tasks.AsQueryable();
		}

		


	}
}