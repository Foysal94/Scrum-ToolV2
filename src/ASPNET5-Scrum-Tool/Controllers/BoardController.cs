using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ASPNET5_Scrum_Tool.Controllers
{
    [Route("[controller]")]
    public class BoardController : Controller
    {
        //Logger<BoardController> logger;
        public Boards m_Board;
        private ScrumToolDB m_context;
        public BoardController(ScrumToolDB p_context)
        {
            m_Board = null;
            m_context = p_context;
        }
        
        [Route("[Action]/{p_BoardName}")]
        public IActionResult Index(string p_BoardName)
        {
            m_Board.Name = p_BoardName;
            ViewData["BoardName"] = m_Board.Name;
            return View(m_Board);
        }

        [Route("[Action]/{p_BoardID}")]
        public IActionResult Load(int p_BoardID)
        {
            var boardList = m_context.Boards.ToList();

            foreach (Boards b in boardList)
            {
                if (b.ID == p_BoardID)
                {
                    m_Board = b;
                    m_Board.ColumnList  = new List<Columns>();
                    break;
                }
            }


            var columnList = m_context.Columns.ToList();
            foreach (Columns c in columnList)
            {
                if (c.BoardID == m_Board.ID)
                {
                    m_Board.ColumnList.Add(c);
                    c.ParentBoard = m_Board;
                }
            }
            
            return View("Show", m_Board);
        }

        [Route("[Action]")]
        public IActionResult Create()
        {
            string boardName = (string) TempData["BoardName"];
            //int boardID = (int) TempData["BoardID"];
            m_Board = new Boards(boardName);
            
            m_context.Boards.Add(m_Board);
            m_context.SaveChanges();
            return View("Show", m_Board);
        }
        
    }
}
