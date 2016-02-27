using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET5_Scrum_Tool.Models
{
    public class Tasks
    {
        private ColumnModel m_ParentColumn;
        private DateTime m_DueDate;
        private string m_TaskContent;

        public ColumnModel ParentColumn { get {return m_ParentColumn;} set { m_ParentColumn = value} }
        public DateTime DueDate { get { return DueDate; }set { DueDate = value; } }
        public string TaskContent { get {return m_TaskContent;} set { m_TaskContent = value; } }

    }
}
