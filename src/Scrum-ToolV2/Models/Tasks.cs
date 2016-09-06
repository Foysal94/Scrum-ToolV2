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
        private int m_ParentBoardID;
        private string m_ParentColumnName;
        private int m_ParentColumnID;
        private Columns m_ParentColumn;
        private List<Labels> m_LabelList;
        private List<Comments> m_CommentList;

        [Key]
        public int ID { get {return m_ID;} set { m_ID = value; } }

        public int ParentBoardID { get { return m_ParentBoardID; } set { m_ParentBoardID = value; } }

        public string ParentColumnName { get {return m_ParentColumnName; } set { m_ParentColumnName = value; } }
        public int ParentColumnID { get {return m_ParentColumnID;} set {m_ParentColumnID = value;} }

        [DataType(DataType.Date)]
        public DateTime DueDate { get { return m_DueDate; }set { m_DueDate = value; } }
        public string TaskContent { get {return m_TaskContent;} set { m_TaskContent = value; } }
        public List<Labels> LabelList { get { return m_LabelList; } set { m_LabelList = value; } }
        public List<Comments> CommentList { get { return m_CommentList; } set { m_CommentList = value; } }

        //public Columns ParentColumn { get {return m_ParentColumn;} set { m_ParentColumn = value; } }

        public Tasks(int p_ParentBoardID, int p_ParentColumnID,string p_ParentColumnName, string p_TaskContent)
        {
            m_ParentBoardID = p_ParentBoardID;
            m_ParentColumnID = p_ParentColumnID;
            m_ParentColumnName = p_ParentColumnName;
            m_TaskContent = p_TaskContent;
            m_DueDate = DateTime.Now.AddDays(1);
	        m_LabelList = new List<Labels>();
			m_CommentList = new List<Comments>();
        }

        public Tasks()
        {
            
        }

    }
}
