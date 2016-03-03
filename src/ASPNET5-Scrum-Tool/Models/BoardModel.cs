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
        private List<ColumnModel> m_ColumnList;
        private int m_ID;
        private DateTime m_CreationDate;

        
        public int ID { get {return m_ID;} set { m_ID = value; } }
        public DateTime CreationDate  { get { return m_CreationDate; } set { m_CreationDate = value; } }

        [Required]
        public string BoardName { get { return m_BoardName; } set { m_BoardName = value; } }

        public List<ColumnModel> ColumnList { get { return m_ColumnList; } set { m_ColumnList = value; } }

    }
}