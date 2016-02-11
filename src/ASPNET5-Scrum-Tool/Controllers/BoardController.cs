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
        
<<<<<<< HEAD
        public IActionResult Index(string pBoardName)
        {
            ViewData["Title"] = pBoardName;
=======
        public IActionResult Index(string p_BoardName)
        {
            ViewData["Title"] = p_BoardName;
>>>>>>> 80e0797f1f6c68d1e9e129fc830cd262303c3682
            return View();
        }
        
        public IActionResult Create()
        {
            return View();
        }
    }
}
