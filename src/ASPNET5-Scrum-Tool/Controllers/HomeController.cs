using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNet.Mvc;

namespace ASPNET5_Scrum_Tool.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var Board = new BoardModel();
            return View(Board);
        }

        [HttpGet]
        public IActionResult SumbitBoardForm(BoardModel model)
        {
            
            return RedirectToAction("Show", "Board", new { p_BoardName = model.BoardName} );
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
