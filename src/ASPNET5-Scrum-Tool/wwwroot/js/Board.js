var AddColumn, BoardName, ColumnNameForm, PanelTitleClick, SumbitColumnForm;

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
      success: function(data) {
        var panelHeading, panelTitleHTML;
        panelTitleHTML = "<h3 class='panel-title'></h3>";
        panelHeading = $('.panel-heading').find('.NewColumnName').parent();
        $(panelHeading).empty();
        $(panelHeading).append(panelTitleHTML);
        return $(panelHeading).find('.panel-title').text(columnName);
      },
      error: function(xhr, err) {
        alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
        return alert("responseText: " + xhr.responseText);
      }
    });
  });
};

AddColumn = function() {
  return $('#AddColumnButton').on('click', function(event) {
    event.preventDefault();
    return $.ajax({
      url: '/Board/AddColumn',
      type: 'POST',
      success: function(data) {
        alert("Hit the success part");
        return alert(data);
      },
      error: function() {
        return alert("Hit the error part");
      }
    });
  });
};

$(document).ready(PanelTitleClick(), SumbitColumnForm(), AddColumn());
