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
        private Labels m_Label;
        public LabelController(ScrumToolDB p_context)
        {
            m_context = p_context;
            m_Label = null;
        }

        [Route("[Action]")]
        [HttpPost]
        public void Delete(int p_LabelID)
        {
            var labelList = m_context.Labels.ToList();

            foreach (Labels label in labelList)
            {
                if (label.ID == p_LabelID)
                {
                    m_context.Labels.Remove(label);
                    m_context.SaveChanges();
                    break;
                }
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public void Add(Labels p_Model)
        {
            m_Label = p_Model;
            m_context.Labels.Add(m_Label);
            m_context.SaveChanges();
        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult TaskAddLabel(int p_TaskID, string p_LabelColour)
        {
            m_Label = new Labels(p_TaskID, p_LabelColour);
            m_context.Labels.Add(m_Label);
            m_context.SaveChanges();
            return ViewComponent("Label", m_Label);
        }

    }
}
