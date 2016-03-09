using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ASPNET5_Scrum_Tool.Models;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNET5_Scrum_Tool.Controllers
{
    public class TaskController : Controller
    {
        private ScrumToolDB m_context;
        public TaskController(ScrumToolDB p_context)
        {
            m_context = p_context;
        }


        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult AddNewTask(Tasks model)
        {
            Tasks tempTask = new Tasks(model.ID + 1,model.BoardID, model.ColumnName, model.TaskContent); // Adding one to the ID because the model has the last task ID.
            m_context.Tasks.Add(tempTask);

            return ViewComponent("Task", tempTask);
        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult MovedTask(string p_ColumnName, int p_TaskID)
        {
            var tasks = m_context.Tasks.ToList();
            Tasks tempTask = null;
            foreach (Tasks t in tasks)
            {
                if (t.ID == p_TaskID)
                {
                    t.ColumnName = p_ColumnName;
                    tempTask = t;
                    m_context.SaveChanges();
                    
                    break;
                }
            }

            return ViewComponent("Task", tempTask);


        }
        
    }
}
