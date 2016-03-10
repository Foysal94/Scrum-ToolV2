using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ASPNET5_Scrum_Tool.Models;

namespace ASPNET5_Scrum_Tool.Controllers.Components
{
    public class Panel_ListsViewComponent : ViewComponent
    {
        private ScrumToolDB m_context;

        public Panel_ListsViewComponent(ScrumToolDB p_context)
        {
            m_context = p_context;
        }

        public IViewComponentResult Invoke(Columns model)
        {
            var TaskList = m_context.Tasks.ToList();

            foreach (Tasks t in TaskList)
            {
                if (t.ColumnName == model.Name && t.BoardID == model.BoardID)
                {
                    model.TasksList.Add(t);
                    t.ParentColumn = model;
                }
            }
            
            return View(model);
            
        }

        /*
        public Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
        */
    }
}