using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET5_Scrum_Tool.Models
{

    public class Columns
    {
        private string m_name;
        private int m_ID;
        private List<Tasks> m_TasksList;
        private int m_ParentBoardID;
        private Boards m_board;

        [Key]
        public int ID { get { return m_ID; } set { m_ID = value; } }
        public int ParentBoardID { get { return m_ParentBoardID; } set { m_ParentBoardID = value; } }
        public string Name { get { return m_name;} set { m_name = value; } }

        public List<Tasks> TasksList { get {return m_TasksList;} set { m_TasksList = value; } }
  
        //public Boards ParentBoard { get {return m_board;} set { m_board = value; } }


        public Columns(string p_Name, int p_ParentBoardID)
        {
            m_name = p_Name;
            m_ParentBoardID = p_ParentBoardID;
            m_TasksList = new List<Tasks>();
        }

        public Columns() { }
    }
}