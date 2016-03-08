using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ASPNET5_Scrum_Tool.Models;

namespace ASPNET5_Scrum_Tool.Controllers.Components
{
    public class Panel_ListsViewComponent : ViewComponent
    {
        private int m_ColumnID = 0;

        public IViewComponentResult Invoke(Columns model)
        {
            m_ColumnID++;
            
            ViewData["ColumnID"] = m_ColumnID;
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