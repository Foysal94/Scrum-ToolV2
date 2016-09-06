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

        public IViewComponentResult Invoke(Labels model)
        {
            return View("Label", model);
        }

        /*
        public Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
        */


    }
}
