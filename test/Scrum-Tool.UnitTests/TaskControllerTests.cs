using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using ASPNET5_Scrum_Tool.Controllers;
using ASPNET5_Scrum_Tool.Models;
using FluentAssertions;
using Xunit;


namespace Scrum_Tool.UnitTests
{
	 [Collection("ScrumToolDB Collection")]
	 public class TaskControllerTests 
	 {
		 private TaskController m_TaskController;
		 private ScrumToolDBFixture m_ScrumToolDBFixture;
		 private ScrumToolDB m_ScrumToolDBContext;
		 private string m_ColumnName;
		 private int m_BoardID;
		 public TaskControllerTests(ScrumToolDBFixture p_ScrumToolDBFixture) 
		 {
			 m_ScrumToolDBFixture = p_ScrumToolDBFixture;

			 m_ColumnName = m_ScrumToolDBFixture.FirstColumnName;
			 m_BoardID = m_ScrumToolDBFixture.FirstBoardID;
			 m_ScrumToolDBContext = m_ScrumToolDBFixture.ScrumToolDB;
			 m_TaskController = new TaskController(m_ScrumToolDBContext);
		 }

		 [Fact]
		 public void AddTask() 
		 {
			 // Act
			 Tasks testTask = new Tasks(m_BoardID,m_ColumnName, "New testTask content");
			 int initalTaskCount = m_ScrumToolDBContext.Tasks.Count();
			 // Arrange
			 m_TaskController.AddNewTask(testTask);
			 // Assert
			 m_ScrumToolDBContext.Tasks.Should().NotBeNullOrEmpty()
			 		.And.HaveCount(initalTaskCount + 1, "Number of inital coloumns plus one");
			 m_ScrumToolDBContext.Tasks.Last().ShouldBeEquivalentTo(testTask, options =>
					options.Excluding(t => t.ID)
							 .Excluding(t=> t.DueDate));
		 }

		 [Fact]
		 public void DeleteTask()
		 {
			//Act
			Tasks lastTask = m_ScrumToolDBContext.Tasks.Last();
			int initalTaskCount = m_ScrumToolDBContext.Tasks.Count();
			//Arrange
			m_TaskController.Delete(lastTask.ID);
			//Assert
			m_ScrumToolDBContext.Tasks.Should().HaveCount(initalTaskCount - 1, "Number of inital tasks minus one")
					.And.NotContain(lastTask);
		 }

		 [Fact]
		 public void UpdateTaskContent()
		 {
			 //Act
			 string newTaskContent = "Content for the test task";
			 Tasks lastTask = m_ScrumToolDBContext.Tasks.Last();
			 //Arrange
			 m_TaskController.UpdateContent(lastTask.ID,newTaskContent);
			 //Assert
			 m_ScrumToolDBContext.Tasks.Last().TaskContent.ShouldBeEquivalentTo(newTaskContent);

		 }

	 }
}
