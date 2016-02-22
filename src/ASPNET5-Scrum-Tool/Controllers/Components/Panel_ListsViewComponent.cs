using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ASPNET5_Scrum_Tool.Models;

namespace ASPNET5_Scrum_Tool.Controllers.Components
{
    public class Panel_ListsViewComponent : ViewComponent
    {
        

        public IViewComponentResult Invoke(ColumnModel model)
        {
            ViewData["ColumnNumber"] = model.ColumnNumber;
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