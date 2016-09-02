using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET5_Scrum_Tool.Models
{
    public class Boards
    {

        private string m_BoardName;
        private List<Columns> m_ColumnList;
        private int m_ID;
        private DateTime m_CreationDate;

        [Key]
        public int ID { get {return m_ID;} set { m_ID = value; } }

        [DataType(DataType.Date)]
        public DateTime CreationDate  { get { return m_CreationDate; } set { m_CreationDate = value; } }

        public string Name { get { return m_BoardName; } set { m_BoardName = value; } }

        public List<Columns> ColumnList { get { return m_ColumnList; } set { m_ColumnList = value; } }

        public Boards(string p_Name )
        {
            m_BoardName = p_Name;
            m_ColumnList = new List<Columns>();
            m_CreationDate = DateTime.Now;
        }

        public Boards(string p_Name, int p_ID)
        {
            m_BoardName = p_Name;
            m_ID = p_ID;
            m_ColumnList = new List<Columns>();
            m_CreationDate = DateTime.Now;
        }

        public Boards() { }

    }
}