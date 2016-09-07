using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET5_Scrum_Tool.Controllers.Components
{
    public class CommentViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Comments p_Model)
        {
            return View("Comment", p_Model);
        }
    }
}
