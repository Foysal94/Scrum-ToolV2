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
            m_context = p_context;
        }
        
        [Route("[Action]/{p_BoardName}")]
        public IActionResult Index(string p_BoardName)
        {
            m_Board.Name = p_BoardName;
            ViewData["BoardName"] = m_Board.Name;
            return View(m_Board);
        }

        [Route("[Action]/{p_BoardName}")]
        public IActionResult Load(string p_BoardName)
        {
            return View();
        }

        public IActionResult Create(string p_BoardName)
        {
            return View();
        }
        
    }
}
