using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET5_Scrum_Tool.Models
{
    public class Tasks
    {
        private DateTime m_DueDate;
        private string m_TaskContent;
        private int m_ID;
        private int m_BoardID;
        private string m_ColumnName;
        private Columns m_ParentColumn;

        [Key]
        public int ID { get {return m_ID;} set { m_ID = value; } }

        public int BoardID { get { return m_BoardID; } set { m_BoardID = value; } }

        public string ColumnName { get {return m_ColumnName; } set { m_ColumnName = value; } }
        //public DateTime? DueDate { get { return m_DueDate; }set { m_DueDate = value; } }
        public string TaskContent { get {return m_TaskContent;} set { m_TaskContent = value; } }

        public Columns ParentColumn { get {return m_ParentColumn;} set { m_ParentColumn = value; } }

        public Tasks(int p_ID, int p_BoardID, string p_ColumnName, string p_TaskContent)
        {
            m_BoardID = p_BoardID;
            m_ID = p_ID;
            m_TaskContent = p_TaskContent;
        }

        public Tasks()
        {
            
        }

    }
}
