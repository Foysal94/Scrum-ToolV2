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
        private int m_ColumnID;
        private Columns m_ParentColumn;
        private List<Labels> m_LabelList;
        private List<Comments> m_CommentList;

        [Key]
        public int ID { get {return m_ID;} set { m_ID = value; } }

        public int BoardID { get { return m_BoardID; } set { m_BoardID = value; } }

        public string ColumnName { get {return m_ColumnName; } set { m_ColumnName = value; } }
        public int ColumnID { get {return m_ColumnID;} set {m_ColumnID = value;} }

        [DataType(DataType.Date)]
        public DateTime DueDate { get { return m_DueDate; }set { m_DueDate = value; } }

        public string TaskContent { get {return m_TaskContent;} set { m_TaskContent = value; } }
        public List<Labels> LabelList { get { return m_LabelList; } set { m_LabelList = value; } }
        public List<Comments> CommentList { get { return m_CommentList; } set { m_CommentList = value; } }

        //public Columns ParentColumn { get {return m_ParentColumn;} set { m_ParentColumn = value; } }

        public Tasks(int p_BoardID, int p_ColumnID,string p_ColumnName, string p_TaskContent)
        {
            m_BoardID = p_BoardID;
            m_ColumnID = p_ColumnID;
            m_ColumnName = p_ColumnName;
            m_TaskContent = p_TaskContent;
            m_DueDate = DateTime.Now.AddDays(1);
        }

        public Tasks()
        {
            
        }

    }
}
