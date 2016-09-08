var ActiveColumn, ActiveTask, ActiveTaskClick, AddColumnButtonClick, AddTaskForm, BoardDropOptions, BoardName, CancelColumnForm, CancelColumnNameChange, CancelTaskForm, ColumnNameFormMouseEvents, DeleteColumnLinkClick, DeleteTaskLinkClick, EditTaskEnter, LoadLabels, PanelTitleClick, SubmitAddColumn, SubmitColumNameChange, SubmitTaskForm, TaskDragOptions, TaskDropOptions, TaskSortOptions, m_BoardID;

var BoardName = $('.BoardNameHeading').text();

var m_BoardID = $('.BoardNameHeading').attr('id');

var loadLabels = function() {
  return $('.LabelListDiv').children('.TaskLabels').each(function() {
    let button = $(this).find('.btn');
    let color = $(button).html();
    return $(button).css('background', color);
  });
};

var taskDragOptions = {
  delay: 300,
  revert: true
};

var taskDropOptions = {
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
			success: function(data) {
				return alert('Label was added successfully');
			},
			error: function(error) {
				alert('Error dropping label, TaskDropOptions');
				return alert("no good " + JSON.stringify(error));
			}
    	});

  }

};

var taskSortOptions = {};

var boardDropOptions = {
  accept: function(element) {
	  if (element.hasClass('ActiveTask')) {
			let task = $(element);
			let newColumnID = $(this).attr('id');
			let currentColumnID = $(element).parent().parent().attr('id');
			if (newColumnID === currentColumnID) {
			return false;
			}
			return true;
	  }
     return false;
  },

  drop: function(event, ui) {
    let column = $(this);
    let selectedTaskDiv = $(ui.draggable);
    $(selectedTaskDiv).parent().css('list-style-type', 'none');
    let newColumnName = $(column).find('.panel-title').text();
    let taskID = $(selectedTaskDiv).find('.Task').attr('id');
    let currentColumnID = $(selectedTaskDiv).parent().parent().attr('id');
    return $.ajax({
      url: '/Task/MoveTaskToNewColumn',
      type: 'POST',
      data: {
        p_ColumnName: newColumnName,
        p_TaskID: taskID
      },
      success: function(data) {
        $(selectedTaskDiv).remove();
        return $(column).find('.AddTask').before(function() {
          return $(data).draggable(TaskDragOptions);
        });
      },
      error: function(error) {
        return alert("no good " + JSON.stringify(error));
      }
    });
  }
};

$('.TaskParentDiv').draggable(TaskDragOptions);

$('.TaskParentDiv').droppable(TaskDropOptions);

$('.BoardColumn').droppable(BoardDropOptions);

$('.ColourLabel').draggable({
  revert: true
});

var activeTask = function() {
  $('#MainColumn').on('mouseenter', '.TaskParentDiv', function(event) {
    $(this).addClass('ActiveTask');
    return $(this).find('.EditTask').removeClass('Hidden');
  });
  return $('#MainColumn').on('mouseleave', '.TaskParentDiv', function(event) {
    let taskWindowOpen = $('body').find('.TaskWindow');
    if (taskWindowOpen.length === 0) {
      $(this).removeClass('ActiveTask');
    }
    return $(this).find('.EditTask').addClass('Hidden');
  });
};

var editTaskEnter = function() {
  $('#MainColumn').on('mouseenter', '.EditTask', function(event) {
    let task = $('.ActiveTask');
    return $(task).removeClass('ActiveTask');
  });
  return $('#MainColumn').on('mouseleave', '.EditTask', function(event) {
    let task = $(this).parent();
    return $(task).addClass('ActiveTask');
  });
};

var ActiveColumn = function() {
  $('#MainColumn').on('mouseenter', '.panel-heading', function(event) {
    return $(this).addClass('ActivePanel');
  });
  return $('#MainColumn').on('mouseleave', '.panel-heading', function(event) {
    return $(this).removeClass('ActivePanel');
  });
};

var activeTaskClick = function() {
  return $('#MainColumn').on('click', '.ActiveTask', function(event) {
    event.preventDefault();
    let task = $(this).find('.Task');
    let taskID = $(task).attr('id');
    return $.ajax({
      url: '/Task/GetTaskInformationWhenClicked/',
      type: 'GET',
      dataType: 'html',
      data: {
        p_TaskID: taskID
      },
      success: function(data) {
        let dialog = data;
        $(data).dialog({
          height: 700,
          width: 900,
          modal: true,
          resizable: false,
          open: function(event, ui) {
            return $('.ui-widget-overlay').bind('click', function(event, ui) {
              return $(data).dialog('close');
            });
          },
          close: function(event, ui) {
            return $('.ActiveTask').removeClass('ActiveTask');
          }
        }).siblings('.ui-dialog-titlebar').removeClass('ui-widget-header');
        return LoadLabels();
      },

      error: function(error) {
        alert("ActiveTask Click method, error");
        return alert(JSON.stringify(error));
      }

    });
  });
};

var panelTitleClick = function() {
  return $('#MainColumn').on('click', '.ActivePanel', function() {
    let PreventFormReload = $(this).find('.NewColumnName');
    if (PreventFormReload.length !== 0) {
      return;
    }
    let selectedColumn = $(this).parent();
    let initalColumnName = $(this).find('.panel-title').text();
    let DoesFormExist = $('#MainColumn').find('.AddColumnForm');
    $('.AddColumnForm').replaceWith('<a id="AddColumnButton">Add a List</a>');

    if (DoesFormExist.length !== 0) {
      let panelHeading = DoesFormExist.parent();
      let oldBoardName = $('.PreviousColumnName').val();
      $('.ColumnNameForm').replaceWith("<h3 class='panel-title'> </h3>");
      panelHeading.find('.panel-title').text(oldBoardName);
    }

    return $.get('/Board/ColumnNameChangeForm', function(data) {
      selectedColumn.find('.panel-title').replaceWith(data);
      $('.PreviousColumnName').val(initalColumnName);
      return $('.NewColumnName').val(initalColumnName);
    });
  });

};

var SubmitColumNameChange = function() {
  return $('#MainColumn').on('click', '.ColumnTitleSubmit', function(event) {
    event.preventDefault();
    let newColumnName = $('.NewColumnName').val().trim();
    let oldColumnName = $('.PreviousColumnName').val().trim();
    if (newColumnName === null || newColumnName === "") {
      return alert('Please enter a name');
    }
    let found = false;
    $('.panel-title').each(function(index, element) {
      let columnName = $(element).text();
      if (columnName === newColumnName) {
        found = true;
        return false;
      }
    });

    if (found === true) {
      return alert('That ColumnName is already in use');
    }
    return $.ajax({
      url: '/Column/ChangeColumnName',
      type: 'POST',
      data: {
        p_OldColumnName: oldColumnName,
        p_NewColumnName: newColumnName,
        p_BoardID: m_BoardID
      },
      success: function(data) {
        let panelHeading = $('.ColumnNameForm').parent();
        $('.ColumnNameForm').replaceWith("<h3 class='panel-title'> </h3>");
        return panelHeading.find('.panel-title').text(newColumnName);
      },
      error: function(error) {
        alert("SubmitColumnForm Method, error");
        return alert(JSON.stringify(error));
      }
    });
  });
};

var CancelColumnNameChange = function() {
  return $('#MainColumn').on('click', '.ColumnTitleCancel', function(event) {
    event.preventDefault();
    let oldColumnName = $('.PreviousColumnName').val().trim();
    let panelheading = $(this).parents('.panel-heading');
    $('.ColumnNameForm').replaceWith('<h3 class="panel-title"></h3>');
    return panelheading.find('.panel-title').html(oldColumnName);
  });
};

var ColumnNameFormMouseEvents = function() {
  $('#MainColumn').on('mouseenter', '.ColumnTitleCancel', function(event) {
    return $('.ActivePanel').removeClass('ActivePanel');
  });
  $('#MainColumn').on('mouseleave', '.ColumnTitleCancel', function(event) {
    let panelheading = $('.ColumnTitleCancel').parents('.panel-heading');
    return $(panelheading).addClass('ActivePanel');
  });
  $('#MainColumn').on('mouseenter', '.ColumnTitleSubmit', function(event) {
    return $('.ActivePanel').removeClass('ActivePanel');
  });
  return $('#MainColumn').on('mouseleave', '.ColumnTitleSubmit ', function(event) {
    let panelheading = $('.ColumnTitleSubmit').parents('.panel-heading');
    return $(panelheading).addClass('ActivePanel');
  });
};

var DeleteColumnLinkClick = function() {
  return $('#MainColumn').on('click', '.DeleteColumnLink', function(event) {
    event.preventDefault();
    let column = $(this).parents('.BoardColumn');
    let columnID = $(column).attr('id');
    return $.ajax({
      url: '/Column/DeleteColumn',
      type: 'POST',
      data: {
        p_ColumnID: columnID
      },
      success: function(data) {
        return $(column).remove();
      },
      error: function(error) {
        alert("DeleteColumn method, error");
        return alert(JSON.stringify(error));
      }
    });
  });
};

var DeleteTaskLinkClick = function() {
  return $('#MainColumn').on('click', '.DeleteTaskLink', function(event) {
    event.preventDefault();
    let task = $(this).parents('.EditTask').prev();
    let taskID = $(task).attr('id');
    return $.ajax({
      url: '/Task/DeleteTask',
      type: 'POST',
      data: {
        p_TaskID: taskID
      },
      success: function(data) {
        return $(task).parent().remove();
      },
      error: function(error) {
        alert("DeleteTask method, error");
        return alert(JSON.stringify(error));
      }
    });
  });
};

var AddColumnButtonClick = function() {
  return $('#MainColumn').on('click', '#AddColumnButton', function(event) {
    event.preventDefault();
    $.get('/Board/AddColumnForm', function(data) {
      return $('#AddColumnButton').replaceWith(data);
    });
    let DoesFormExist = $('#MainColumn').find('.ColumnNameForm');
    if (DoesFormExist.length !== 0) {
      let panelHeading = DoesFormExist.parent();
      let oldBoardName = $('.PreviousColumnName').val();
      return panelHeading.html("<h3 class='panel-title'></h3>").text(oldBoardName);
    }
  });
};

var SubmitAddColumn = function() {
  return $('#MainColumn').on('click', '.AddColumnSubmit', function(event) {
    event.preventDefault();
    let newColumnName = $('.NewColumnName').val().trim();
    if (newColumnName === null || newColumnName === "") {
      return alert('Please enter a name');
    }
    let found = false;
    $('.panel-title').each(function(index, element) {
      let columnName = $(element).text();
      if (columnName === newColumnName) {
        found = true;
        return false;
      }
    });
    if (found === true) {
      return alert('That ColumnName is already in use');
    }
    return $.ajax({
      url: '/Column/AddColumn',
      type: 'POST',
      data: {
        p_Name: newColumnName,
        p_BoardID: m_BoardID
      },
      success: function(data) {
        $('.AddColumnForm').after('<a id="AddColumnButton">Add a List</a>');
        return $('.AddColumnForm').replaceWith(function() {
          return $(data).droppable(BoardDropOptions);
        });
      },
      error: function(error) {
        alert("SubmitAddColumn method error");
        return alert(JSON.stringify(error));
      }
    });
  });
};

var CancelColumnForm = function() {
  return $('#MainColumn').on('click', '.AddColumnCancel', function(event) {
    event.preventDefault();
    return $('.AddColumnForm').replaceWith("<a id='AddColumnButton'>Add a List</a>");
  });
};

var AddTaskForm = function() {
  return $('#MainColumn').on('click', '.AddTask', function(event) {
    let selectedColumn = $(this).parent().parent();
    let PreventFormReload = $(selectedColumn).find('.AddTaskForm');
    if (PreventFormReload.length !== 0) {
      return;
    }
    let selectedColumnID = $(selectedColumn).attr('id');
    let prevTask = $(this).prev();
    let DoesFormExist = $('#MainColumn').find('.AddTaskForm');
    if (DoesFormExist.length !== 0) {
      let column = DoesFormExist.parent();
      column.find('.AddTaskForm').replaceWith("<a class='AddTask'> Add a task.... </a>");
      column.find('.TaskFormSubmit').remove();
    }
    return $.get('/Board/AddTaskForm', function(data) {
      return $(selectedColumn).find('.AddTask').replaceWith(data);
    });
  });
};

var SubmitTaskForm = function() {
  return $('#MainColumn').on('click', '.TaskFormSubmit', function(event) {
    event.preventDefault();
    let selectedColumnName = $('.AddTaskForm').parent().parent().find('.panel-title').text();
    let taskContent = $('.BoardTaskContentInput').val().trim();

    return $.ajax({
      url: '/Task/AddTask',
      type: 'POST',
      data: {
        p_BoardID: m_BoardID,
        p_ColumnName: selectedColumnName,
        p_TaskContent: taskContent
      },
      success: function(data) {
        $('.AddTaskForm').after('<a class="AddTask"> Add a task.... </a>');
        return $('.AddTaskForm').replaceWith(function() {
          $(data).draggable(TaskDragOptions);
          $(data).droppable(TaskDropOptions);
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

var CancelTaskForm = function() {
  return $('#MainColumn').on('click', '.TaskFormCancel', function(event) {
    event.preventDefault();
    return $('.AddTaskForm').replaceWith("<a class='AddTask'> Add a task.... </a>");
  });
};

$(document).ready(
	PanelTitleClick(), SubmitColumNameChange(), AddTaskForm(), SubmitTaskForm(), AddColumnButtonClick(), ActiveTask(), SubmitAddColumn(), ActiveColumn(), DeleteColumnLinkClick(), DeleteTaskLinkClick(), ActiveTaskClick(), CancelTaskForm(), CancelColumnNameChange(), CancelColumnForm(), EditTaskEnter(),ColumnNameFormMouseEvents()
	);
