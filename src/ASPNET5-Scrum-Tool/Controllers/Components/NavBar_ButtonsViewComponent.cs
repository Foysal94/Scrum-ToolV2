using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET5_Scrum_Tool.Controllers.Components
{
    public class NavBar_Buttons : ViewComponent 
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

        /*
        public Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
        */
    }
}