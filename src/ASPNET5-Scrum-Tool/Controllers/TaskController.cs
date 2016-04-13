using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ASPNET5_Scrum_Tool.Models;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNET5_Scrum_Tool.Controllers
{
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private ScrumToolDB m_context;
        private Tasks m_Task;

        public TaskController(ScrumToolDB p_context)
        {
            m_context = p_context;
            m_Task = null;
        }


        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult AddNewTask(Tasks model)
        {
            m_Task = new Tasks(model.BoardID, model.ColumnName, model.TaskContent); // Adding one to the ID because the model has the last task ID.
            m_context.Tasks.Add(m_Task);
            m_context.SaveChanges();

            return ViewComponent("Task", m_Task);
        }


        [Route("[Action]")]
        [HttpPost]
        public void Delete(int p_TaskID)
        {
            var taskList = m_context.Tasks.ToList();

            foreach (Tasks t in taskList)
            {
                if (t.ID == p_TaskID)
                {
                    m_context.Tasks.Remove(t);
                    m_context.SaveChanges();
                    break;
                }
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public void UpdateContent(int p_TaskID, string p_NewTaskContent)
        {
            var tasks = m_context.Tasks.ToList();

            foreach (Tasks t in tasks)
            {
                if (t.ID == p_TaskID)
                {
                    t.TaskContent = p_NewTaskContent;
                    m_context.SaveChanges();
                    break;
                }
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public string GetTaskContent(int p_TaskID)
        {
            var tasks = m_context.Tasks.ToList();

            foreach (Tasks t in tasks)
            {
                if (t.ID == p_TaskID)
                {
                    return t.TaskContent;
                }
            }

            return "Error, no content found";
        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult MovedTask(string p_ColumnName, int p_TaskID)
        {
            var tasks = m_context.Tasks.ToList();

            foreach (Tasks t in tasks)
            {
                if (t.ID == p_TaskID)
                {
                    t.ColumnName = p_ColumnName;
                    m_Task = t;
                    m_context.SaveChanges();

                    break;
                }
            }

            return ViewComponent("Task", m_Task);
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult Information(int p_TaskID)
        {
            var taskList = m_context.Tasks.ToList();
            var labelList = m_context.Labels.ToList();
            foreach (Tasks t in taskList)
            {
                if (t.ID == p_TaskID)
                {
                    t.LabelList = new List<Labels>();
                    foreach (Labels label in labelList)
                    {
                        if (t.ID == label.TaskID)
                        {
                            t.LabelList.Add(label);
                        }
                    }
                    m_Task = t;
                    break;
                }
            }
            return PartialView("_Information", m_Task);
        }


        [Route("[Action]")]
        [HttpGet]
        public IActionResult EditTaskForm(int p_TaskID)
        {
            return PartialView("_EditTaskForm");
        }



    }
}