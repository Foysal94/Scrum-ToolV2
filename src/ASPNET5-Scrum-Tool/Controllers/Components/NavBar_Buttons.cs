using System.Linq;
using Microsoft.AspNet.Mvc;

namespace ASPNET5_Scrum_Tool.Comonents
{
    public class NavBar_Buttons : ViewComponent 
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}