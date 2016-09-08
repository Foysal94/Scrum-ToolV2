using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNET5_Scrum_Tool.Controllers
{
    [Route("[Controller]")]
    public class ColumnController : Controller
    {
        private readonly ScrumToolDB m_context;
		
        public ColumnController(ScrumToolDB p_context)
        {
            m_context = p_context;
        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult AddColumn(Columns model)
        {
           Columns tempColumn = new Columns(model.Name, model.ParentBoardID); // The model got passed the last column ID
           m_context.Columns.Add(tempColumn);
           m_context.SaveChanges();
           return ViewComponent("Column", tempColumn);
        }

        [Route("[Action]")]
        [HttpPost]
        public void DeleteColumn(int p_ColumnID)
		  {
			  var columnsList = m_context.Columns.Where(c => c.ID == p_ColumnID);
              foreach(Columns c in columnsList)
              {
                 m_context.Columns.Remove(c);
              }

			  m_context.SaveChanges();
		  }

        [Route("[Action]")]
        [HttpPost]
        public void ChangeColumnName(string p_OldColumnName, string p_NewColumnName, int p_OldID, int p_BoardID)
        {
            var columnsList = m_context.Columns.Where(c => c.Name == p_OldColumnName && c.ID == p_OldID);
            foreach (Columns c in columnsList)
            {
                c.Name = p_NewColumnName;
                              
            }
            m_context.SaveChanges(); 
        }

        [Route("[Action]")]
        [HttpGet]
        public string GetColumnHeader(int p_ColumnID)
        {
            var columnList = m_context.Columns.ToList();
            string name = "";
            int ParentBoardID = 0;
            foreach (Columns c in columnList)
            {
                if (c.ID == p_ColumnID)
                {
                    name = c.Name;
                    ParentBoardID = c.ParentBoardID;
                }
            }
            return "";
        }

    }
}
