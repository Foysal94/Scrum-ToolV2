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
        private int m_TaskID;
        private string m_Colour;

        public int ID { get {return m_ID;} set { m_ID = value; } }

        public int TaskID { get { return m_TaskID; } set { m_TaskID = value; } }

        public string Colour { get { return m_Colour;} set { m_Colour = value; } }

        public Labels(int p_TaskID, string p_Colour)
        {
            m_TaskID = p_TaskID;
            m_Colour = p_Colour;
        }

        public Labels() { }
    }
}