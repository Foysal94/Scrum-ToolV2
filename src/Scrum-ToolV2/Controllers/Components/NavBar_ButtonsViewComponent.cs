using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET5_Scrum_Tool.Controllers.Components
{
    public class Navbar_Buttons : ViewComponent 
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Navbar_Buttons");
        }
        
    }
}