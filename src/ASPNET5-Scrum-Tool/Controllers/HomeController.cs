using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace ASPNET5_Scrum_Tool.Controllers
{
    public class HomeController : Controller
    {
        private ScrumToolDB m_context;

        public HomeController(ScrumToolDB p_context)
        {
            m_context = p_context;
        }

        public IActionResult Index()
        {
            var Board = new Boards();
            return View(Board);
        }

        [HttpPost]
        public IActionResult SumbitBoardForm(Boards model)
        {
            var query = from board in m_context.Boards where board.Name.Equals(model.Name) select board;
            var boards = m_context.Boards.ToList();
            bool found = false;
            foreach (var boardModel in boards)
            {
                if (boardModel.Name == model.Name)
                {
                    found = true;
                    break;
                }
            }

            return RedirectToAction("Index", "Board", new { p_BoardName = model.Name} );
        }


    }
}
