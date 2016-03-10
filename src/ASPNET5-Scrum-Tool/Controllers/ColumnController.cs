using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Scaffolding.Metadata;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNET5_Scrum_Tool.Controllers
{
    [Route("[Controller]")]
    public class ColumnController : Controller
    {
        private ScrumToolDB m_context;

        public ColumnController(ScrumToolDB p_context)
        {
            m_context = p_context;
        }

        
        [Route("[Action]")]
        [HttpPost]
        public void ChangeColumnName(string p_OldColumnName, string p_NewColumnName, int p_BoardID)
        {
            var columns = m_context.Columns.ToList();
            string query = "from column in m_context.Columns where column.Name.Equals(p_OldBoardName) select column";

            foreach (Columns c in columns)
            {
                if (c.Name == p_OldColumnName)
                {
                    //m_context.Columns.Update(c)
                    c.Name = p_NewColumnName;
                    m_context.SaveChanges();
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
            return ViewComponent("Panel_Lists", tempColumn);
        }
        

    }
}
