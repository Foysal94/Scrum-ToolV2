using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ASPNET5_Scrum_Tool.Controllers
{
    [Route("[controller]")]
    public class BoardController : Controller
    {
        //Logger<BoardController> logger;
        public BoardModel m_Board;

        public BoardController()
        {
            m_Board = new BoardModel();
            m_Board.ColumnList = new List<ColumnModel>();
            m_Board.ColumnList.Add(new ColumnModel("Something1", 0));
            m_Board.ColumnList.Add(new ColumnModel("Something2", 1));
            m_Board.ColumnList[0].TasksList.Add(new TaskModel(m_Board.ColumnList[0].ID, 0, "Task 1 "));
            m_Board.ColumnList[0].TasksList.Add(new TaskModel(m_Board.ColumnList[0].ID, 1, "Task 2 "));
            m_Board.ColumnList[0].TasksList.Add(new TaskModel(m_Board.ColumnList[0].ID, 2, "Task 3 "));
            m_Board.ColumnList[0].TasksList.Add(new TaskModel(m_Board.ColumnList[0].ID, 3, "Task 4 "));
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
            m_Board.ColumnList[model.ID].Name = model.Name; 

           // var json = JsonConvert.SerializeObject( m_Board.ColumnList[model.ColumnNumber]);

            return Json(model.Name);
        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult AddColumn(ColumnModel model)
        {
            ColumnModel tempColumn = new ColumnModel(model.Name,model.ID + 1 );
            m_Board.ColumnList.Add(tempColumn);
            return ViewComponent("Panel_Lists", tempColumn);
        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult AddNewTask(TaskModel model)
        {
            TaskModel tempTask = new TaskModel(model.ParentColumnID,model.ID, model.TaskContent); // Adding one for a new task
            if (tempTask.ID != 0)
            {
                tempTask.ID++;
            }
            m_Board.ColumnList[model.ParentColumnID ].TasksList.Add(tempTask); // -1 or else it will be out of range. List starts from 0 but website columns start from 1
            return ViewComponent("Task", tempTask);
        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult MovedTask(TaskModel model, string NewColumnID)
        {
            // Remove the task from its old column
            int oldColumnID = model.ParentColumnID;
            int newColumnID = int.Parse(NewColumnID);
            //Board.ColumnList[oldColumnID ].TasksList.RemoveAt(model.TaskID);

            // Update task parent column
            TaskModel tempTask = model;
            tempTask.ParentColumnID = newColumnID;
            m_Board.ColumnList[newColumnID ].TasksList.Add(tempTask);

            return ViewComponent("Task", tempTask);


        }

        
    }
}
