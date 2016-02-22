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
        private int m_columnNumber;

        public int ColumnNumber { get { return m_columnNumber; } }
        public string ColumnName { get { return m_name;} set { m_name = value; } }

        public ColumnModel(string p_Name, int p_ColumnNumber)
        {
            m_name = p_Name;
            m_columnNumber = p_ColumnNumber;
        }
    }
}