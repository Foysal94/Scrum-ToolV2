using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace ASPNET5_Scrum_Tool.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult GoToBoardPage([FromBody] string Name)
        {
            //string Name = "Hello";
            return RedirectToAction("Show", "Board", new { boardName = Name});
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
