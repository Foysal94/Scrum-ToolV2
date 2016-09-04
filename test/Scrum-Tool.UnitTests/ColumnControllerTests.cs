using System.Linq;
using ASPNET5_Scrum_Tool.Controllers;
using ASPNET5_Scrum_Tool.Models;
using FluentAssertions;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GenFu;

namespace Scrum_Tool.UnitTests
{
    
    public class ColumnControllerTests 
    {
        private const int m_BoardID = 0;
        public ColumnControllerTests() 
        {
           
        }

        private IQueryable<Columns> GenerateMockData() 
        {
            int id = 0;
            int numberOfColumns = 3;
            string Name = "TestColumn";

            A.Configure<Columns>()
                .Fill(c => c.ID,
                     () => { return id++; })
                .Fill(c => c.Name, () => { return $"{Name}{id++}"; })
                .Fill(c => c.BoardID, m_BoardID);

            var columns = A.ListOf<Columns>(numberOfColumns);
            return columns.AsQueryable();
        }

        

    }
}

