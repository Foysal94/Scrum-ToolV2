var BoardName, ColumnNameForm, PanelTitleClick, SumbitColumnForm;

BoardName = $('.BoardNameHeading').text();

ColumnNameForm = "<input class='PreviousColumnName' type='hidden'  style='display: none;' /> <input name='ColumnName' class='NewColumnName'> <input type='submit' value='Continue' class='ColumnTitleSumbit'>";

PanelTitleClick = function() {
  return $('.panel-heading').on('click', function() {
    var PreventFormReload, initalColumnName, selectedColumn, selectedColumnID;
    PreventFormReload = $(this).find('.NewColumnName');
    if (PreventFormReload.length !== 0) {
      return;
    }
    selectedColumn = $(this).parent();
    selectedColumnID = $(selectedColumn).attr('id');
    initalColumnName = $(this).find('.panel-title').text();
    return $.ajax({
      url: '/Board/Show',
      type: 'GET',
      dataType: 'HTML',
      success: function() {
        var DoesFormExist, oldBoardName, panelHeading;
        DoesFormExist = $('#MainColumn').find('.NewColumnName');
        if (DoesFormExist.length !== 0) {
          panelHeading = DoesFormExist.parent();
          oldBoardName = $('.PreviousColumnName').val();
          DoesFormExist.remove();
          $('.ColumnTitleSumbit').remove();
          panelHeading.append("<h3 class='panel-title'></h3>");
          panelHeading.find('.panel-title').text(oldBoardName);
        }
        selectedColumn.find('.panel-title').remove();
        selectedColumn.find('.panel-heading').append(ColumnNameForm);
        $('.PreviousColumnName').val(initalColumnName);
        return $('.NewColumnName').val(initalColumnName);
      }
    });
  });
};

SumbitColumnForm = function() {
  return $('.panel-heading').on('click', 'input.ColumnTitleSumbit', function(event) {
    var columnName, columnNumber, newColumnData, object;
    event.preventDefault();
    columnName = $('.NewColumnName').val().trim();
    columnNumber = $(this).parent().parent().attr('id');
    newColumnData = {
      'ColumnName': columnName,
      'ColumnNumber': columnNumber
    };
    object = JSON.stringify(newColumnData);
    return $.ajax({
      url: '/Board/ChangeColumnName',
      type: 'POST',
      dataType: 'html',
      contentType: 'application/json; charset=UTF-8',
      data: object,
      success: function(data) {
        alert("Hit the Success part");
        return alert(data);
      },
      error: function(xhr, err) {
        alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
        return alert("responseText: " + xhr.responseText);
      }
    });
  });
};


/*
ChangeHTML = () -> 
    $('.AddTask').text 'Hello World'
 */

$(document).ready(PanelTitleClick(), SumbitColumnForm());
