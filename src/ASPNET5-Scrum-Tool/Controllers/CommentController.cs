using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNet.Mvc;

namespace ASPNET5_Scrum_Tool.Controllers
{
    [Route("[Controller]")]
    public class CommentController : Controller
    {
        private ScrumToolDB m_context;
        
        public CommentController(ScrumToolDB p_context)
        {
            m_context = p_context;
        }


        [Route("[Action]")]
        [HttpPost]
        public IActionResult Create(int p_TaskID, string p_Name, string p_Content)
        {
            Comments tempComment = new Comments(p_Name, p_Content, p_TaskID);
            m_context.Comments.Add(tempComment);
            m_context.SaveChanges();
            return ViewComponent("Comment", tempComment);
        }
    }
}
