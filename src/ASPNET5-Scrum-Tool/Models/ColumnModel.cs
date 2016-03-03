using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace ASPNET5_Scrum_Tool.Models
{

    public class ColumnModel
    {
        private string m_name;
        private int m_ID;
        private List<TaskModel> m_TasksList;   

        [Key]
        public int ColumnID { get { return m_ID; } set { m_ID = value; } }

        public string ColumnName { get { return m_name;} set { m_name = value; } }
        public List<TaskModel> TasksList { get {return m_TasksList;} set { m_TasksList = value; } }

        public ColumnModel(string p_Name, int p_ColumnNumber)
        {
            m_name = p_Name;
            m_ID= p_ColumnNumber;
            m_TasksList = new List<TaskModel>();
        }

        public ColumnModel() { }
    }
}