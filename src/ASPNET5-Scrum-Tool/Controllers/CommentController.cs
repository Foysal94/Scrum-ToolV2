using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create(Comments p_Model)
        {
            Comments tempComment = p_Model;
            m_context.Comments.Add(tempComment);
            m_context.SaveChanges();
            return ViewComponent("Comment", tempComment);
        }

        [Route("[Action]")]
        [HttpPost]
        public void Delete(int p_CommentID)
        {
            var commentList = m_context.Comments.ToList();

            foreach (Comments c in commentList)
            {
                if (c.ID == p_CommentID)
                {
                    m_context.Comments.Remove(c);
                    m_context.SaveChanges();
                    break;
                }
            }
        }
    }
}
