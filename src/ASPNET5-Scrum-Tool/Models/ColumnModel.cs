using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace ASPNET5_Scrum_Tool.Models
{
    public class BoardModel
    {
        private string m_BoardName;

        [Required]
        public string BoardName { get { return m_BoardName; } set { m_BoardName = value; } }

    }
}