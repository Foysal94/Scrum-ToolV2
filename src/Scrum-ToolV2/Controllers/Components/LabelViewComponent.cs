using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET5_Scrum_Tool.Controllers.Components
{
    public class LabelViewComponent : ViewComponent
    {   
        public async Task<IViewComponentResult> InvokeAsync(Labels p_Model)
        {
            return View("Label", p_Model);
        }

    }
}
