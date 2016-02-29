var ActiveTask, AddColumn, AddTaskForm, BoardName, ColumnNameForm, PanelTitleClick, SubmitColumnForm, SubmitTaskForm, TaskForm;

BoardName = $('.BoardNameHeading').text();

ColumnNameForm = "<input class='PreviousColumnName' type='hidden'  style='display: none;' /> <input name='ColumnName' class='NewColumnName'> <input type='submit' value='Continue' class='ColumnTitleSubmit'>";

TaskForm = "<input name='TaskContent' class='TaskContent'> <input type='submit' value='Continue' class='TaskFormSubmit'>";

ActiveTask = function() {
  $('#MainColumn').on('mouseenter', '.Task', function(event) {
    return $(this).addClass('ActiveCard');
  });
  return $('#MainColumn').on('mouseleave', '.Task', function(event) {
    return $(this).removeClass('ActiveCard');
  });
};

PanelTitleClick = function() {
  return $('#MainColumn').on('click', 'div.panel-heading', function() {
    var PreventFormReload, initalColumnName, selectedColumn, selectedColumnID;
    PreventFormReload = $(this).find('.NewColumnName');
    if (PreventFormReload.length !== 0) {
      return;
    }
    selectedColumn = $(this).parent();
    selectedColumnID = $(selectedColumn).attr('id');
    initalColumnName = $(this).find('.panel-title').text();
    return $.ajax({
      url: '/Board/Show/' + BoardName,
      type: 'GET',
      success: function() {
        var DoesFormExist, oldBoardName, panelHeading;
        DoesFormExist = $('#MainColumn').find('.NewColumnName');
        if (DoesFormExist.length !== 0) {
          panelHeading = DoesFormExist.parent();
          oldBoardName = $('.PreviousColumnName').val();
          panelHeading.html("<h3 class='panel-title'></h3>").text(oldBoardName);
        }
        selectedColumn.find('.panel-title').html(ColumnNameForm);
        $('.PreviousColumnName').val(initalColumnName);
        return $('.NewColumnName').val(initalColumnName);
      }
    });
  });
};

SubmitColumnForm = function() {
  return $('.panel-heading').on('click', 'input.ColumnTitleSubmit', function(event) {
    var columnName, columnNumber;
    event.preventDefault();
    columnName = $('.NewColumnName').val().trim();
    columnNumber = $(this).parent().parent().attr('id');
    return $.ajax({
      url: '/Board/ChangeColumnName',
      type: 'POST',
      data: {
        ColumnName: columnName,
        ColumnNumber: columnNumber
      },
      dataType: 'json',
      success: function(data) {
        var panelHeading;
        panelHeading = $('.panel-heading').find('.NewColumnName').parent();
        return $(panelHeading).html("<h3 class='panel-title'> <h3>").text(columnName);
      },
      error: function(error) {
        return alert("no good " + JSON.stringify(error));
      }
    });
  });
};

AddColumn = function() {
  return $('#AddColumnButton').on('click', function(event) {
    var newColumnDataID, newColumnName;
    event.preventDefault();
    newColumnDataID = $('#MainColumn').children().last().prev().attr('id');
    newColumnName = 'Something';
    return $.ajax({
      url: '/Board/AddColumn',
      type: 'POST',
      data: {
        ColumnName: newColumnName,
        ColumnID: newColumnDataID
      },
      success: function(data) {
        return $('#AddColumnButton').before(data);
      },
      error: function() {
        return alert("Hit the error part");
      }
    });
  });
};

AddTaskForm = function() {
  return $('#MainColumn').on('click', '.AddTask', function(event) {
    var PreventFormReload, prevTask, selectedColumn, selectedColumnID;
    event.preventDefault();
    selectedColumn = $(this).parent().parent();
    PreventFormReload = $(selectedColumn).find('TaskContent');
    if (PreventFormReload.length !== 0) {
      return;
    }
    selectedColumnID = $(selectedColumn).attr('id');
    prevTask = $(this).prev();
    return $.ajax({
      url: '/Board/Show/' + BoardName,
      type: 'GET',
      success: function() {
        var DoesFormExist, column;
        DoesFormExist = $('#MainColumn').find('.TaskContent');
        if (DoesFormExist.length !== 0) {
          column = DoesFormExist.parent();
          column.find('.TaskContent').replaceWith("<a class='AddTask'> Add a task.... </a>");
          column.find('.TaskFormSubmit').remove();
        }
        return $(selectedColumn).find('.AddTask').replaceWith(TaskForm);
      }
    });
  });
};

SubmitTaskForm = function() {
  return $('#MainColumn').on('click', '.TaskFormSubmit', function(event) {
    var selectedColumnID, taskContent, taskID;
    event.preventDefault();
    selectedColumnID = $('.TaskContent').parent().parent().attr('id');
    taskContent = $('.TaskContent').val().trim();
    taskID = $('.TaskContent').prev().attr('id');
    if (typeof TaskID !== "undefined" && TaskID !== null) {
      taskID === 0;
    }
    return $.ajax({
      url: '/Board/AddNewTask',
      type: 'POST',
      data: {
        ParentColumnID: selectedColumnID,
        TaskID: taskID,
        TaskContent: taskContent
      },
      success: function(data) {
        $('.TaskContent').replaceWith(data);
        return $('.TaskFormSubmit').replaceWith('<a class="AddTask"> Add a task.... </a>');
      },
      error: function(error) {
        return alert("no good " + JSON.stringify(error));
      }
    });
  });
};

$(document).ready(PanelTitleClick(), SubmitColumnForm(), AddTaskForm(), SubmitTaskForm(), AddColumn(), ActiveTask());
