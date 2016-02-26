using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ASPNET5_Scrum_Tool.Controllers
{
    [Route("[controller]")]
    public class BoardController : Controller
    {
        Logger<BoardController> logger;
        private BoardModel m_Board;

        public BoardController()
        {
            m_Board = new BoardModel();
            m_Board.ColumnList = new List<ColumnModel>();
            m_Board.ColumnList.Add(new ColumnModel("Something1", 1));
            m_Board.ColumnList.Add(new ColumnModel("Something2", 2));
        }
        
        [Route("{p_BoardName}")]
        public IActionResult Show(string p_BoardName)
        {
            m_Board.BoardName = p_BoardName;
            ViewData["BoardName"] = m_Board.BoardName;
            return View(m_Board);
        }

        [HttpPost]
        public JsonResult ChangeColumnName(ColumnModel model)
        {
           // ColumnModel column = JsonConvert.DeserializeObject<ColumnModel>(newColumnData);
            // Update column name in the board model, the board model stores a list of columns
            m_Board.ColumnList[model.ColumnNumber].ColumnName = model.ColumnName;

            //var json = JsonConvert.SerializeObject(m_Board.ColumnList[column.ColumnNumber]);

            return Json(model.ColumnName);
        }

        [HttpPost]
        public IActionResult AddColumn()
        {
            return ViewComponent("Panel_Lists");
        }
    }
}
