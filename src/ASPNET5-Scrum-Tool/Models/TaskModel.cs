using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET5_Scrum_Tool.Models
{
    public class TaskModel
    {
        private int m_ParentColumnID;
        private DateTime m_DueDate;
        private string m_TaskContent;
        private int m_ID;

        public int ParentColumnID { get {return m_ParentColumnID;} set { m_ParentColumnID = value; } }
        public DateTime DueDate { get { return m_DueDate; }set { m_DueDate = value; } }
        public string TaskContent { get {return m_TaskContent;} set { m_TaskContent = value; } }
        public int TaskID { get {return m_ID;} set { m_ID = value; } }

        public TaskModel(int p_columnID , int p_ID, string p_TaskContent)
        {
            m_ParentColumnID = p_columnID;
            m_ID = p_ID;
            m_TaskContent = p_TaskContent;
        }

        public TaskModel()
        {
            
        }

    }
}
