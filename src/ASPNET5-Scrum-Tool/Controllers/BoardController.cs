using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

namespace ASPNET5_Scrum_Tool.Controllers
{
    public class BoardController : Controller
    {
        Logger<BoardController> logger;

        [Route("{boardName}")]
        public IActionResult Show(string boardName)
        {
            ViewData["Title"] = boardName;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
