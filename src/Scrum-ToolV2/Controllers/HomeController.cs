using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            if (!ModelState.IsValid) //not valid
            {
                return RedirectToAction("Index");
            }

            //var query = from board in m_context.Boards where board.Name.Equals(model.Name) select board;
            var boards = m_context.Boards.ToList();
            foreach (var boardModel in boards)
            {
                if (boardModel.Name == model.Name)
                {
                    return RedirectToAction("Load", "Board", new { p_BoardID = boardModel.ID}); // Board Found
                }
            }
            //model.ID = boards.Count + 1;
            TempData["BoardName"] = model.Name;
            //TempData["BoardID"] = model.ID;
            return RedirectToAction("Create", "Board", new { p_BoardID = boards.Count + 1} );
        }
        

    }
}
