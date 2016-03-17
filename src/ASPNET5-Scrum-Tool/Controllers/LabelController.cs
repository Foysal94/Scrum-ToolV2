using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNet.Mvc;

namespace ASPNET5_Scrum_Tool.Controllers
{
    [Route("[Controller]")]
    public class LabelController
    {
        private ScrumToolDB m_context;
        private Tasks m_Label;
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
        public void Add(int p_TaskID, string p_LabelColour)
        {
            Labels tempLabel = new Labels(p_TaskID, p_LabelColour);
            m_context.Labels.Add(tempLabel);
            m_context.SaveChanges();
        }


    }
}
