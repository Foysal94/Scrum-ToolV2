using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

namespace ASPNET5_Scrum_Tool.Controllers
{
    [Route("[controller]")]
    public class BoardController : Controller
    {
        Logger<BoardController> logger;

        
        [Route("{BoardName}")]
        public IActionResult Show(string BoardName)
        {
            ViewData["Title"] = BoardName;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
