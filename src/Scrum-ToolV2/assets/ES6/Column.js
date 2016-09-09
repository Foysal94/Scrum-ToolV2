const activeColumn = function() {
  $('#MainColumn').on('mouseenter', '.panel-heading', function(event) {
    return $(this).addClass('ActivePanel');
  });
  return $('#MainColumn').on('mouseleave', '.panel-heading', function(event) {
    return $(this).removeClass('ActivePanel');
  });
};

const columnTitleClick = function() {
  return $('#MainColumn').on('click', '.ActivePanel', function() {
    const PreventFormReload = $(this).find('.NewColumnName');
    if (PreventFormReload.length !== 0) {
      return;
    }
    const selectedColumn = $(this).parent();
    const initalColumnName = $(this).find('.panel-title').text();
    const DoesFormExist = $('#MainColumn').find('.AddColumnForm');
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

const submitColumNameChange = function() {
  return $('#MainColumn').on('click', '.ColumnTitleSubmit', function(event) {
    event.preventDefault();
    const newColumnName = $('.NewColumnName').val().trim();
    const oldColumnName = $('.PreviousColumnName').val().trim();
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

const cancelColumnNameChange = function() {
  return $('#MainColumn').on('click', '.ColumnTitleCancel', function(event) {
    event.preventDefault();
    const oldColumnName = $('.PreviousColumnName').val().trim();
    const panelheading = $(this).parents('.panel-heading');
    $('.ColumnNameForm').replaceWith('<h3 class="panel-title"></h3>');
    return panelheading.find('.panel-title').html(oldColumnName);
  });
};

const columnNameFormMouseEvents = function() {
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

const deleteColumnLinkClick = function() {
  return $('#MainColumn').on('click', '.DeleteColumnLink', function(event) {
    event.preventDefault();
    const column = $(this).parents('.BoardColumn');
    const columnID = $(column).attr('id');
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

const addColumnButtonClick = function() {
  return $('#MainColumn').on('click', '#AddColumnButton', function(event) {
    event.preventDefault();
    $.get('/Board/AddColumnForm', function(data) {
      return $('#AddColumnButton').replaceWith(data);
    });
    const DoesFormExist = $('#MainColumn').find('.ColumnNameForm');
    if (DoesFormExist.length !== 0) {
      let panelHeading = DoesFormExist.parent();
      let oldBoardName = $('.PreviousColumnName').val();
      return panelHeading.html("<h3 class='panel-title'></h3>").text(oldBoardName);
    }
  });
};

const submitAddColumn = function() {
  return $('#MainColumn').on('click', '.AddColumnSubmit', function(event) {
    event.preventDefault();
    const newColumnName = $('.NewColumnName').val().trim();
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

const cancelColumnForm = function() {
  return $('#MainColumn').on('click', '.AddColumnCancel', function(event) {
    event.preventDefault();
    return $('.AddColumnForm').replaceWith("<a id='AddColumnButton'>Add a List</a>");
  });
};


$(document).ready(
	 activeColumn(),
	 columnTitleClick(), 
	 submitColumNameChange(), 
	 cancelColumnNameChange(), 
	 columnNameFormMouseEvents(),
	 deleteColumnLinkClick(),
	 addColumnButtonClick(),
	 submitAddColumn(),
	 cancelColumnForm()
)