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
		private const int m_FirstColumnID = 1;
		private const int m_FirstTaskID = 1;
		private const string m_FirstColumnName = "TestColumn1";
		public ScrumToolDB ScrumToolDB { get { return m_ScrumToolDB;} }
		public int FirstBoardID { get {return m_FirstBoardID;} }
		public int FirstColumnID { get {return m_FirstColumnID;} }
		public int FirstTaskID { get {return m_FirstTaskID;} }
		public string FirstColumnName { get {return m_FirstColumnName;} }
		
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
			var boardList = CreateBoardData();
			var columnList = CreateColumnData(ref boardList);
			var taskList = CreateTaskData(ref columnList);
			var labelList = CreateLabelData(ref taskList);
			var commentList = CreateCommentData(ref taskList);

			m_ScrumToolDB.Boards.AddRange(boardList);
			m_ScrumToolDB.Columns.AddRange(columnList);
			m_ScrumToolDB.Tasks.AddRange(taskList);
			m_ScrumToolDB.Labels.AddRange(labelList);
			m_ScrumToolDB.Comments.AddRange(commentList);
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
		private IQueryable<Columns> CreateColumnData(ref IQueryable<Boards> p_BoardsList)
		{
			List<Columns> columns = new List<Columns>()
			{
				new Columns(m_FirstColumnName, m_FirstBoardID),
				new Columns("TestColumn2", m_FirstBoardID),
				new Columns("TestColumn3", m_FirstBoardID), 
			};

			foreach(Columns c in columns) 
			{
				p_BoardsList.First().ColumnList.Add(c);
			}
			

			return columns.AsQueryable();
		}
		private IQueryable<Tasks> CreateTaskData(ref IQueryable<Columns> p_ColumnsList)
		{
			List<Tasks> tasks = new List<Tasks>()
			{
				new Tasks(m_FirstBoardID, m_FirstColumnID, m_FirstColumnName, "TaskContent1"),
				new Tasks(m_FirstBoardID, m_FirstColumnID, m_FirstColumnName, "TaskContent2"),
				new Tasks(m_FirstBoardID, m_FirstColumnID, m_FirstColumnName, "TaskContent3"),
			};

			foreach(Tasks t in tasks) 
			{
				p_ColumnsList.First().TasksList.Add(t);
			}

			return tasks.AsQueryable();
		}
		private IQueryable<Labels> CreateLabelData(ref IQueryable<Tasks> p_TasksList)
		{
			List<Labels> labels = new List<Labels>()
			{
				new Labels(m_FirstTaskID,"Blue"),
				new Labels(m_FirstTaskID,"Green"),
				new Labels(m_FirstTaskID,"Purple")
			};

			foreach(Labels l in labels) 
			{
				p_TasksList.First().LabelList.Add(l);
			}

			return labels.AsQueryable();
		}
		private IQueryable<Comments> CreateCommentData(ref IQueryable<Tasks> p_TasksList)
		{
			List<Comments> comments = new List<Comments>()
			{
				new Comments("Foysal Ahmed","Comment Content 1", m_FirstTaskID),
				new Comments("Foysal Ahmed","Comment Content 2", m_FirstTaskID),
				new Comments("Foysal Ahmed","Comment Content 3", m_FirstTaskID),
			};

			foreach(Comments c in comments) 
			{
				p_TasksList.First().CommentList.Add(c);
			}

			return comments.AsQueryable();
		}


	}
}