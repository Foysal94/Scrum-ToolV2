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
		// Assumptions:
	public class Bar
	{
		// Bar implementation
	}

	public interface IFoo {
		bool DoSomething();
		string DoSomethingStringy();
		bool TryParse();
		bool Submit();
		int GetCount();
		int GetCountThing();
	}
	 // This project can output the Class library as a NuGet Package.
	 // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
	public class BoardControllerTests
	{


		[Fact]
		public void PassingTest()
		{
			Mock mock = new Mock<IFoo>();
			mock.Setup(foo => foo.DoSomething("ping")).Returns(true);
			mock.Should().BeTrue();
			

		}
	}

}
