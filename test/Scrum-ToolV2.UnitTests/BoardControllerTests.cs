using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET5_Scrum_Tool;
using ASPNET5_Scrum_Tool.Controllers;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using FluentAssertions;
using Xunit;
using Moq;

namespace Scrum_Tool.UnitTests
{
	public interface IUser
	{

		int CalculateAge();
		DateTime DateOfBirth { get; set; }
		string Name { get; set; }
	}
	public class ConsumerOfIUser
	{
		public int Consume(IUser user)
		{
			return user.CalculateAge() + 10;
		}
	}

	public class User : IUser
	{
		public DateTime DateOfBirth { get; set; }
		public string Name { get; set; }

		public int CalculateAge()
		{
			return DateTime.Now.Year - DateOfBirth.Year;
		}
	}
	// This project can output the Class library as a NuGet Package.
	// To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
	public class BoardControllerTests
	{


		[Fact]
		public void PassingTest()
		{
			var userMock = new Mock<IUser>();
			userMock.Setup(u => u.CalculateAge()).Returns(10);
			var consumer = new ConsumerOfIUser();
			var result = consumer.Consume(userMock.Object);

			result.Should().BeGreaterOrEqualTo(20);
		}
	}
}
