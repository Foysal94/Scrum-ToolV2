using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace ASPNET5_Scrum_Tool.Models
{
    public class Comments
    {
        private int m_ID;
        private string m_Name;
        private int m_TaskID;
        private string m_Content;
        private DateTime m_CreationDate;

        public int ID { get { return m_ID; } set { m_ID = value; } }
        public int TaskID { get { return m_TaskID; } set { m_TaskID = value; } }
        public DateTime CreationDate { get { return m_CreationDate; } set { m_CreationDate = value; } }
        public string Name { get; set; }
        public string Content { get { return m_Content;} set { m_Content = value; } }

        public Comments(string p_Name, string p_Content, int p_TaskID)
        {
            m_Name = p_Name;
            m_Content = p_Content;
            m_TaskID = p_TaskID;
        }

        public Comments() { }

    }


}
