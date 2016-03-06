using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace ASPNET5_Scrum_Tool.Models
{

    public class ColumnModel
    {
        private string m_name;
        private int m_ID;
        private List<TaskModel> m_TasksList;
        private int m_ParentBoardID;

        [Key]
        public int ID { get { return m_ID; } set { m_ID = value; } }
        public string Name { get { return m_name;} set { m_name = value; } }
        public List<TaskModel> TasksList { get {return m_TasksList;} set { m_TasksList = value; } }
        public int BoardID { get { return m_ParentBoardID; } set { m_ParentBoardID = value; } }



        public ColumnModel(string p_Name, int p_ColumnNumber)
        {
            m_name = p_Name;
            m_ID= p_ColumnNumber;
            m_TasksList = new List<TaskModel>();
        }

        public ColumnModel() { }
    }
}