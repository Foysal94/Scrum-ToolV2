using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ASPNET5_Scrum_Tool.Models;

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

        /*
        [Route("[Action]")]
        [HttpPost]
        public JsonResult ChangeColumnName(ColumnModel model, string p_BoardName)
        {
            //ColumnModel column = JsonConvert.DeserializeObject<ColumnModel>(newColumnData);
           // m_context.Boards.find
            // Update column name in the board model, the board model stores a list of columns
            m_context.BoardsColumnList[model.ID].ColumnName = model.Name;

            // var json = JsonConvert.SerializeObject( m_Board.ColumnList[model.ColumnNumber]);

            return Json(model.ColumnName);
        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult AddColumn(ColumnModel model)
        {
            ColumnModel tempColumn = new ColumnModel(model.ColumnName, model.ColumnID + 1);
            m_Board.ColumnList.Add(tempColumn);
            return ViewComponent("Panel_Lists", tempColumn);
        }
        */

    }
}
