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
        public void AddLabel(int p_TaskID, string p_LabelColour)
        {
            Labels tempLabel = new Labels(p_TaskID,p_LabelColour);
            m_context.Labels.Add(tempLabel);
            m_context.SaveChanges();
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult Information(int p_TaskID)
        {
            var taskList = m_context.Tasks.ToList();
            foreach (Tasks t in taskList)
            {
                if (t.ID == p_TaskID)
                {
                    m_Task = t;
                    break;
                }
            }     
            return PartialView("_Information", m_Task);
        }

       


    }
}
