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
        private ScrumToolDB m_context;
        private int m_ColumnNameIncrement;

        public ColumnController(ScrumToolDB p_context)
        {
            m_context = p_context;
        }

        [Route("[Action]")]
        [HttpPost]
        public void ChangeColumnName(string p_OldColumnName, string p_NewColumnName, int p_BoardID)
        {
            var columns = m_context.Columns.ToList();
            //string query = "from column in m_context.Columns where column.Name.Equals(p_OldBoardName) select column";

            foreach (Columns c in columns)
            {
                if (c.Name == p_OldColumnName)
                {
                    //m_context.Columns.Update(c)
                    c.Name = p_NewColumnName;
                    m_context.SaveChanges();
                    break;
                }
            }


        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult AddColumn(Columns model)
        {
            Columns tempColumn = new Columns(model.Name, model.BoardID); // The model got passed the last column ID

            m_context.Columns.Add(tempColumn);
            m_context.SaveChanges();
            tempColumn.TasksList = new List<Tasks>();
            return ViewComponent("Panel_Lists", tempColumn);
        }

        [Route("[Action]")]
        [HttpPost]
        public void Delete(int p_ColumnID)
        {
            var columnList = m_context.Columns.ToList();

            foreach (Columns c in columnList)
            {
                if (c.ID == p_ColumnID)
                {
                    m_context.Columns.Remove(c);
                    m_context.SaveChanges();
                    break;
                }
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public string GetColumnHeader(int p_ColumnID)
        {
            var columnList = m_context.Columns.ToList();
            string name = "";
            int boardID = 0;
            foreach (Columns c in columnList)
            {
                if (c.ID == p_ColumnID)
                {
                    name = c.Name;
                    boardID = c.BoardID;
                }
            }


            return "";
        }

    }
}
