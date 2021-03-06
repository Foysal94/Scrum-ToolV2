﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNET5_Scrum_Tool.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNET5_Scrum_Tool.Controllers
{
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private ScrumToolDB m_context;
        private Tasks m_Task;

        public TaskController(ScrumToolDB p_context)
        {
            m_context = p_context;
            m_Task = null;
        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult AddTask(Tasks model)
        {
            m_Task = new Tasks(model.ParentBoardID, model.ParentColumnID, model.ParentColumnName, model.TaskContent);
            m_context.Add(m_Task);
            m_context.SaveChanges();

            return ViewComponent("Task", m_Task);
        }

        [Route("[Action]")]
        [HttpPost]
        public void DeleteTask(int p_TaskID)
        {
            var taskList = m_context.Tasks.Where(t => t.ID == p_TaskID);

            foreach (Tasks t in taskList)
            {
                m_context.Tasks.Remove(t);
               
            }
            
            m_context.SaveChanges(); 
        }

        [Route("[Action]")]
        [HttpPost]
        public void UpdateTaskContent(int p_TaskID, string p_NewTaskContent)
        {
            var tasks = m_context.Tasks.ToList();

            foreach (Tasks t in tasks)
            {
                if (t.ID == p_TaskID)
                {
                    t.TaskContent = p_NewTaskContent;
                    m_context.SaveChanges();
                    break;
                }
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public string GetTaskContent(int p_TaskID)
        {
            var tasks = m_context.Tasks.ToList();

            foreach (Tasks t in tasks)
            {
                if (t.ID == p_TaskID)
                {
                    return t.TaskContent;
                }
            }

            return "Error, no content found";
        }

        [Route("[Action]")]
        [HttpPost]
        public ViewComponentResult MoveTaskToNewColumn(string p_NewColumnName, int p_NewColumnID, int p_TaskID)
        {
            var tasks = m_context.Tasks.ToList();

            foreach (Tasks t in tasks)
            {
                if (t.ID == p_TaskID)
                {
                    t.ParentColumnName = p_NewColumnName;
                    t.ParentColumnID = p_NewColumnID;
                    m_Task = t;
                    m_context.SaveChanges();
                    break;
                }
            }

            return ViewComponent("Task", m_Task);
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult GetTaskInformationWhenClicked(int p_TaskID)
        {
            var taskList = m_context.Tasks.ToList();
            var labelList = m_context.Labels.ToList();
            var commentList = m_context.Comments.ToList();
            foreach (Tasks t in taskList)
            {
                if (t.ID == p_TaskID)
                {
                    t.LabelList = new List<Labels>();
                    foreach (Labels label in labelList)
                    {
                        if (t.ID == label.ParentTaskID)
                        {
                            t.LabelList.Add(label);
                        }
                    }

                    t.CommentList = new List<Comments>();
                    foreach (Comments comment in commentList)
                    {
                        if (t.ID == comment.ParentTaskID)
                        {
                            t.CommentList.Add(comment);
                        }

                    }
                    m_Task = t;
                    break;
                }
            }
            return PartialView("_Information", m_Task);
        }


        [Route("[Action]")]
        [HttpPost]
        public void UpdateTaskDate(int p_TaskID, string p_Date)
        {
            var tasks = m_context.Tasks.ToList();
            DateTime dateTime = DateTime.Parse(p_Date);

            foreach (Tasks t in tasks)
            {
                if (t.ID == p_TaskID)
                {
                    t.DueDate = dateTime.Date;
                    m_context.SaveChanges();
                    break;
                }
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public IActionResult EditTaskForm(int p_TaskID)
        {
            return PartialView("_EditTaskForm");
        }



    }
}