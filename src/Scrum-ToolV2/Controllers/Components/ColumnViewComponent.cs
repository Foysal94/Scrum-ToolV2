using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNET5_Scrum_Tool.Models;

namespace ASPNET5_Scrum_Tool.Controllers.Components
{
    public class ColumnViewComponent : ViewComponent
    {
        private ScrumToolDB m_context;

        public ColumnViewComponent(ScrumToolDB p_context)
        {
            m_context = p_context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Columns p_Model)
        {
            return View("Column", p_Model);
        }

    }
}