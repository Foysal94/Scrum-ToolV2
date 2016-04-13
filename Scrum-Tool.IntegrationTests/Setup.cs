using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool;
using ASPNET5_Scrum_Tool.Models;
using GenFu;
using Microsoft.AspNet.TestHost;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using GenFu = GenFu.GenFu;


namespace Scrum_Tool.IntegrationTests
{
    public class Setup
    {
        private readonly TestServer m_Server;
        private readonly HttpClient m_Client;
        protected ScrumToolDB m_Context;

        private DbContextOptionsBuilder<ScrumToolDB> builder;

        private readonly IServiceProvider m_ServiceProvider;
        public Setup()
        {
            // Arrange
            
            //m_Server = new TestServer(TestServer.CreateBuilder()
            //    .UseStartup<Startup>());
           // m_Client = m_Server.CreateClient();

           //var services = new ServiceCollection();

           //services.AddEntityFramework()
            //.AddInMemoryDatabase()
           // .AddDbContext<db>(options => options.UseInMemoryDatabase());


            //m_ServiceProvider = services.BuildServiceProvider();
            //m_Context = new ScrumToolDB();
            var db = new DbContextOptionsBuilder();
            db.UseInMemoryDatabase();
            m_Context = new ScrumToolDB(db.Options);

            CreateTestData(m_Context);

        }

        public void CreateTestData(ScrumToolDB dbContext)
        {
            
            // CreateBoards(dbContext);
             //CreateColumns(dbContext);
            // CreateTasks(dbContext);

            dbContext.Boards.Add(new Boards()
            {
                Name = "HelloWorld"
            });

            dbContext.Columns.Add(new Columns()
            {
                Name = "Something1",
                BoardID = 1
            });

            dbContext.Columns.Add(new Columns()
            {
                Name = "Something2",
                BoardID = 1
            });

            dbContext.Tasks.Add(new Tasks()
            {
                ColumnName = "Something1",
                BoardID = 1,
                TaskContent = "Stuff to do1"
            });

            dbContext.Tasks.Add(new Tasks()
            {
                ColumnName = "Something1",
                BoardID = 1,
                TaskContent = "Stuff to do2"
            });

            dbContext.Tasks.Add(new Tasks()
            {
                ColumnName = "Something1",
                BoardID = 1,
                TaskContent = "Stuff to do3"
            });

            dbContext.SaveChanges();

        }

        public void CreateBoards(ScrumToolDB dbContext)
        {
            int id = 0;
            string Name = "HelloWorld";

            A.Configure<Boards>()
                .Fill(b => b.ID, 
                     () => { return id++; });

            A.Configure<Boards>()
                .Fill(b => b.CreationDate, DateTime.Now);

              A.Configure<Boards>()
                .Fill(b => b.Name, () => { return $"{Name}{id++}"; } );

            var boards = A.ListOf<Boards>(3);
 
            dbContext.Boards.AddRange(boards);
            dbContext.SaveChanges();

        }

        private void CreateColumns(ScrumToolDB dbContext)
        {
            int id = 0;
            string Name = "Something";

            A.Configure<Columns>()
                .Fill(c => c.ID, 
                     () => { return id++; });

            A.Configure<Columns>()
                .Fill(c => c.Name, () => { return $"{Name}{id++}"; } );

            A.Configure<Columns>()
                .Fill(c => c.BoardID, 1 );

            var columns = A.ListOf <Columns>(3);

            dbContext.Columns.AddRange(columns);
            dbContext.SaveChanges();

        }

        private void CreateTasks(ScrumToolDB dbContext)
        {
            int id = 0;
            string content = "Something To Do Tomorrow";

            A.Configure<Tasks>()
                .Fill(t => t.ID, 
                     () => { return id++; });

            A.Configure<Tasks>()
                .Fill(t => t.BoardID,1);

            A.Configure<Tasks>()
                .Fill(t => t.ColumnName,"Something1");
    
        }


    }
}
