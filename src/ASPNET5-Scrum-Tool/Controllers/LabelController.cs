using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool.Models;

namespace ASPNET5_Scrum_Tool.Controllers
{
    public class LabelController
    {
        private ScrumToolDB m_context;
        private Tasks m_Label;
        public LabelController(ScrumToolDB p_context)
        {
            m_context = p_context;
            m_Label = null;
        }

        public void DeleteLabel(int p_LabelID)
        {
            
        }
    }
}
