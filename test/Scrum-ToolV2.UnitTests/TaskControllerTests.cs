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
		 private int m_ParentBoardID;
		 private int m_ParentColumnID;
		 public TaskControllerTests(ScrumToolDBFixture p_ScrumToolDBFixture) 
		 {
			 m_ScrumToolDBFixture = p_ScrumToolDBFixture;
			 m_ColumnName = m_ScrumToolDBFixture.FirstColumnName;
			 m_ParentBoardID = m_ScrumToolDBFixture.FirstBoardID;
			 m_ScrumToolDBContext = m_ScrumToolDBFixture.ScrumToolDB;
			 m_TaskController = new TaskController(m_ScrumToolDBContext);
		 }

		 [Fact]
		 public void Add_Task() 
		 {
			 // Act
			 Tasks testTask = new Tasks(m_ParentBoardID, m_ParentColumnID, m_ColumnName, "New testTask content");
			 int initalTaskCount = m_ScrumToolDBContext.Tasks.Count();
			 // Arrange
			 m_TaskController.AddTask(testTask);
			 // Assert
			 m_ScrumToolDBContext.Tasks.Should().NotBeNullOrEmpty()
			 		.And.HaveCount(initalTaskCount + 1, "Number of inital coloumns + 1");
			 m_ScrumToolDBContext.Tasks.Last().ShouldBeEquivalentTo(testTask, options =>
					options.Excluding(t => t.ID)
							 .Excluding(t=> t.DueDate), "The last Task object in the table should be the testTask");
		 }

		 [Fact]
		 public void Delete_Task()
		 {
			//Act
			Tasks lastTask = m_ScrumToolDBContext.Tasks.Last();
			int initalTaskCount = m_ScrumToolDBContext.Tasks.Count();
			//Arrange
			m_TaskController.DeleteTask(lastTask.ID);
			//Assert
			m_ScrumToolDBContext.Tasks.Should().HaveCount(initalTaskCount - 1, "Number of inital tasks - 1")
					.And.NotContain(lastTask);
		 }

		 [Fact]
		 public void Update_Task_Content()
		 {
			 //Act
			 string newTaskContent = "Content for the test task";
			 Tasks lastTask = m_ScrumToolDBContext.Tasks.Last();
			 //Arrange
			 m_TaskController.UpdateTaskContent(lastTask.ID,newTaskContent);
			 //Assert
			 m_ScrumToolDBContext.Tasks.Last().TaskContent.ShouldBeEquivalentTo(newTaskContent);
		 }
		 
		 [Fact]
		 public void Update_Task_Date() 
		 {
			 //Arrange
			 Tasks lastTask = m_ScrumToolDBContext.Tasks.Last();
          string originalDate = lastTask.DueDate.ToString();
			 string newDate = System.DateTime.Now.AddHours(3).ToString();
			 //Act
			 m_TaskController.UpdateTaskDate(lastTask.ID, newDate);
			 //Assert
			 m_ScrumToolDBContext.Tasks.Last().DueDate.ToString().Should().NotBe(originalDate);
		 }

		 [Fact]
		 public void Update_Task_Column_When_Moved()
		 {
			 //Arrange
			 Tasks lastTask = m_ScrumToolDBContext.Tasks.Last(); 
			 int originalColumnID = lastTask.ParentColumnID;
			 int newColumnID = 100;
			 string originalColumnName = lastTask.ParentColumnName;
			 string newColumnName = "New Column For task";
			 //Act
			 m_TaskController.MoveTaskToNewColumn(newColumnName, newColumnID, lastTask.ID);
			 //Assert
			 m_ScrumToolDBContext.Tasks.Last().ParentColumnID.ShouldBeEquivalentTo(newColumnID);
			 m_ScrumToolDBContext.Tasks.Last().ParentColumnName.ShouldBeEquivalentTo(newColumnName);
			 		

		 }

		 [Fact]
		 public void Correct_View_Returned_When_Clicked_For_PopupWindow()
		 {
			 //Arrange
			 int ParentTaskID = m_ScrumToolDBFixture.FirstTaskID;
			 int labelListCount = m_ScrumToolDBContext.Tasks.First().LabelList.Count();
			 int commentListCount = m_ScrumToolDBContext.Tasks.First().CommentList.Count();
			 //Act
			 PartialViewResult result = (PartialViewResult) m_TaskController.GetTaskInformationWhenClicked(ParentTaskID);
			 //Assert
			 result.Should().NotBeNull()
			 	.And.BeOfType<PartialViewResult>();
			 result.ViewName.Should().Be("_Information");
			 result.ViewData.Model.Should().BeOfType<Tasks>()
				 .Which.LabelList.Should().NotBeNullOrEmpty();
		 }

	 }
}
