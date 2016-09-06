using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET5_Scrum_Tool.Models
{
    public class Labels
    {
        private int m_ID;
        private int m_ParentTaskID;
        private string m_Colour;

        public int ID { get {return m_ID;} set { m_ID = value; } }

        public int ParentTaskID { get { return m_ParentTaskID; } set { m_ParentTaskID = value; } }

        public string Colour { get { return m_Colour;} set { m_Colour = value; } }

        public Labels(int p_TaskID, string p_Colour)
        {
            m_ParentTaskID = p_TaskID;
            m_Colour = p_Colour;
        }

        public Labels() { }
    }
}