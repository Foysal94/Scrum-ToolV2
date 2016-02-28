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
        //Logger<BoardController> logger;
        private BoardModel m_Board;

        public BoardController()
        {
            m_Board = new BoardModel();
            m_Board.ColumnList = new List<ColumnModel>();
            m_Board.ColumnList.Add(new ColumnModel("Something1", 1));
            m_Board.ColumnList.Add(new ColumnModel("Something2", 2));
            m_Board.ColumnList[1].TasksList.Add(new TaskModel(m_Board.ColumnList[1].ColumnID, 1, "asdasdasdas vvxcvd vd fsdfdsfsdf sdhfhdfgdfg "));
            m_Board.ColumnList[1].TasksList.Add(new TaskModel(m_Board.ColumnList[1].ColumnID, 2, "asdasdasdas vvxcvd vd fsdfdsfsdf sdhfhdfgdfg "));
            m_Board.ColumnList[1].TasksList.Add(new TaskModel(m_Board.ColumnList[1].ColumnID, 3, "asdasdasdas vvxcvd vd fsdfdsfsdf sdhfhdfgdfg "));
            m_Board.ColumnList[1].TasksList.Add(new TaskModel(m_Board.ColumnList[1].ColumnID, 4, "asdasdasdas vvxcvd vd fsdfdsfsdf sdhfhdfgdfg "));
        }
        
        [Route("[Action]/{p_BoardName}")]
        public IActionResult Show(string p_BoardName)
        {
            m_Board.BoardName = p_BoardName;
            ViewData["BoardName"] = m_Board.BoardName;
            return View(m_Board);
        }


        [Route("[Action]")]
        [HttpPost]
        public JsonResult ChangeColumnName(ColumnModel model)
        {
            //ColumnModel column = JsonConvert.DeserializeObject<ColumnModel>(newColumnData);
            // Update column name in the board model, the board model stores a list of columns
            m_Board.ColumnList[model.ColumnID].ColumnName = model.ColumnName; 

           // var json = JsonConvert.SerializeObject( m_Board.ColumnList[model.ColumnNumber]);

            return Json(model.ColumnName);
        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult AddColumn(ColumnModel model)
        {
            ColumnModel tempColumn = new ColumnModel(model.ColumnName,model.ColumnID + 1);
            m_Board.ColumnList.Add(tempColumn);
            return ViewComponent("Panel_Lists", tempColumn);
        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult AddNewTask(TaskModel model)
        {
            TaskModel tempTask = new TaskModel(model.ParentColumnID,model.TaskID + 1, model.TaskContent); // Adding one for a new task
            
            m_Board.ColumnList[model.ParentColumnID - 1].TasksList.Add(tempTask); // -1 or else it will be out of range. List starts from 0 but website columns start from 1
            return ViewComponent("Task", tempTask);
        }

        
    }
}
