/* global loadLabels */
const taskDragOptions = {
  delay: 300,
  revert: true
};

const taskDropOptions = {
  accept: function(element) {
    if (element.hasClass('ColourLabel')) {
      return true;
    }
    return false;
  },
  drop: function(event, ui) {
    let task = $(this);
    $(task).css('list-style-type', 'none');
    let taskID = $(task).find('.Task').attr('id');
    let label = $(ui.draggable);
    let labelColour = $(label).attr('id');
    let colour = labelColour.slice(0, -5);

    return $.ajax({
      url: '/Label/AddLabel',
      type: 'POST',
      data: {
        p_TaskID: taskID,
        p_LabelColour: colour
      },
      success: function() {
        return alert('Label was added successfully');
      },
      error: function(error) {
        alert('Error dropping label, TaskDropOptions');
        return alert("no good " + JSON.stringify(error));
      }
    });
  }
}

const activeTask = function() {
  $('#MainColumn').on('mouseenter', '.TaskParentDiv', function() {
    $(this).addClass('ActiveTask');
    return $(this).find('.EditTask').removeClass('Hidden');
  });
  return $('#MainColumn').on('mouseleave', '.TaskParentDiv', function() {
    let taskWindowOpen = $('body').find('.TaskWindow');
    if (taskWindowOpen.length === 0) {
      $(this).removeClass('ActiveTask');
    }
    return $(this).find('.EditTask').addClass('Hidden');
  });
};

const editTaskEnter = function() {
  $('#MainColumn').on('mouseenter', '.EditTask', function() {
    const task = $('.ActiveTask');
    return $(task).removeClass('ActiveTask');
  });
  return $('#MainColumn').on('mouseleave', '.EditTask', function() {
    const task = $(this).parent();
    return $(task).addClass('ActiveTask');
  });
};

const activeTaskClick = function() {
  return $('#MainColumn').on('click', '.ActiveTask', function(event) {
    event.preventDefault();
    const task = $(this).find('.Task');
    const taskID = $(task).attr('id');
    return $.ajax({
      url: '/Task/GetTaskInformationWhenClicked/',
      type: 'GET',
      dataType: 'html',
      data: {
        p_TaskID: taskID
      },
      success: function(data) {
        $(data).dialog({
          height: 700,
          width: 900,
          modal: true,
          resizable: false,
          open: function() {
            return $('.ui-widget-overlay').bind('click', function() {
              return $(data).dialog('close');
            });
          },
          close: function() {
            return $('.ActiveTask').removeClass('ActiveTask');
          }
        }).siblings('.ui-dialog-titlebar').removeClass('ui-widget-header');
        return loadLabels();
      },

      error: function(error) {
        alert("ActiveTask Click method, error");
        return alert(JSON.stringify(error));
      }

    });
  });
};

const addTaskForm = function() {
  return $('#MainColumn').on('click', '.AddTask', function() {
    const selectedColumn = $(this).parent().parent();
    const preventFormReload = $(selectedColumn).find('.AddTaskForm');
    if (preventFormReload.length !== 0) {
      return;
    }
    //const selectedColumnID = $(selectedColumn).attr('id');
    //const prevTask = $(this).prev();
    const doesFormExist = $('#MainColumn').find('.AddTaskForm');
    if (doesFormExist.length !== 0) {
      let column = doesFormExist.parent();
      column.find('.AddTaskForm').replaceWith("<a class='AddTask'> Add a task.... </a>");
      column.find('.TaskFormSubmit').remove();
    }
    return $.get('/Board/AddTaskForm', function(data) {
      return $(selectedColumn).find('.AddTask').replaceWith(data);
    });
  });
};

const submitTaskForm = function() {
  return $('#MainColumn').on('click', '.TaskFormSubmit', function(event) {
    event.preventDefault();
    const selectedColumnName = $('.AddTaskForm').parent().parent().find('.panel-title').text();
    const taskContent = $('.BoardTaskContentInput').val().trim();
    const boardID = $('.BoardNameHeading').attr('id');
    return $.ajax({
      url: '/Task/AddTask',
      type: 'POST',
      data: {
        p_BoardID: boardID,
        p_ColumnName: selectedColumnName,
        p_TaskContent: taskContent
      },
      success: function(data) {
        $('.AddTaskForm').after('<a class="AddTask"> Add a task.... </a>');
        return $('.AddTaskForm').replaceWith(function() {
          $(data).draggable(taskDragOptions);
          $(data).droppable(taskDropOptions);
          return $(data).css('list-style-type', 'none');
        });
      },
      error: function(error) {
        alert('SubmitTaskForm Error');
        return alert("no good " + JSON.stringify(error));
      }
    });

  });
};

const deleteTaskLinkClick = function() {
  return $('#MainColumn').on('click', '.DeleteTaskLink', function(event) {
    event.preventDefault();
    const task = $(this).parents('.EditTask').prev();
    const taskID = $(task).attr('id');
    return $.ajax({
      url: '/Task/DeleteTask',
      type: 'POST',
      data: {
        p_TaskID: taskID
      },
      success: function() {
        return $(task).parent().remove();
      },
      error: function(error) {
        alert("DeleteTask method, error");
        return alert(JSON.stringify(error));
      }
    });
  });
};

const cancelAddTaskForm = function() {
  return $('#MainColumn').on('click', '.TaskFormCancel', function(event) {
    event.preventDefault();
    return $('.AddTaskForm').replaceWith("<a class='AddTask'> Add a task.... </a>");
  });
};




const taskJS = function() {
  $(document).ready(
     $('.TaskParentDiv').draggable(taskDragOptions),
     $('.TaskParentDiv').droppable(taskDropOptions),
     activeTask(),
     editTaskEnter(),
     activeTaskClick(),
     addTaskForm(),
     submitTaskForm(),
     deleteTaskLinkClick(),
     cancelAddTaskForm()
  )
}

export default taskJS;