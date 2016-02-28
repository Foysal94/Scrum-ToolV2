using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET5_Scrum_Tool.Models
{
    public class TaskModel
    {
        private ColumnModel m_ParentColumn;
        private DateTime m_DueDate;
        private string m_TaskContent;
        private int m_ID;

        public ColumnModel ParentColumn { get {return m_ParentColumn;} set { m_ParentColumn = value; } }
        public DateTime DueDate { get { return m_DueDate; }set { m_DueDate = value; } }
        public string TaskContent { get {return m_TaskContent;} set { m_TaskContent = value; } }
        public int TaskID { get {return m_ID;} set { m_ID = value; } }

        public TaskModel(ColumnModel p_column , int p_ID, string p_TaskContent)
        {
            m_ParentColumn = p_column;
            m_ID = p_ID;
            m_TaskContent = p_TaskContent;
        }

        public TaskModel()
        {
            
        }

    }
}
