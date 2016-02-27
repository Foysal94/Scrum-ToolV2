var AddColumn, BoardName, ColumnNameForm, PanelTitleClick, SumbitColumnForm;

BoardName = $('.BoardNameHeading').text();

ColumnNameForm = "<input class='PreviousColumnName' type='hidden'  style='display: none;' /> <input name='ColumnName' class='NewColumnName'> <input type='submit' value='Continue' class='ColumnTitleSumbit'>";

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

SumbitColumnForm = function() {
  return $('.panel-heading').on('click', 'input.ColumnTitleSumbit', function(event) {
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
        ColumnNumber: newColumnDataID
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

$(document).ready(PanelTitleClick(), SumbitColumnForm(), AddColumn());
