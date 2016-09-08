using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNET5_Scrum_Tool.Models;

namespace ASPNET5_Scrum_Tool.Controllers
{
    [Route("[Controller]")]
    public class LabelController : Controller
    {
        private ScrumToolDB m_context;
        public LabelController(ScrumToolDB p_context)
        {
            m_context = p_context;
        }

        [Route("[Action]")]
        [HttpPost]
        public void DeleteLabel(int p_LabelID)
        {
            var labelList = m_context.Labels.Where(l => l.ID == p_LabelID);

            foreach (Labels label in labelList)
            {
                m_context.Labels.Remove(label);
                
            }
            m_context.SaveChanges();
        }

        [Route("[Action]")]
        [HttpPost]
        public void AddLabel(Labels p_Model)
        {
            Labels tempLabel = p_Model;
            m_context.Labels.Add(tempLabel);
            m_context.SaveChanges();
        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult AddLabelFromTaskWindow(Labels p_Model)
        {
            Labels tempLabel = p_Model;
            m_context.Labels.Add(tempLabel);
            m_context.SaveChanges();
            return ViewComponent("Label", tempLabel);
        }

    }
}
