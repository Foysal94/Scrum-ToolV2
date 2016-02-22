var BoardName, ColumnNameForm, PanelTitleClick, SumbitColumnForm;

BoardName = $('.BoardNameHeading').text();

ColumnNameForm = "<form class='ColumnTitleForm' asp-controller='Board' asp-action='ChangeColumnName' method='POST'> <input class='PreviousColumnName' type='hidden'  style='display: none;' /> <input asp-for='ColumnName' class='NewColumnName'> <input type='submit' value='Continue' class='ColumnTitleSumbit'> </form>";

PanelTitleClick = function() {
  return $('.panel-heading').on('click', function() {
    var initalColumnName, selectedColumn, selectedColumnID;
    selectedColumn = $(this).parent();
    selectedColumnID = $(selectedColumn).attr('id');
    initalColumnName = $(this).find('.panel-title').text();
    return $.ajax({
      url: '/Board/Show',
      type: 'GET',
      dataType: 'HTML',
      success: function() {
        var DoesFormExist, oldBoardName, panelHeading;
        DoesFormExist = $('#MainColumn').find('.ColumnTitleForm');
        if (DoesFormExist.length !== 0) {
          panelHeading = DoesFormExist.parent();
          oldBoardName = $('.PreviousColumnName').val();
          DoesFormExist.remove();
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
  return $('.ColumnTitleSumbit').on('click', function(event) {
    event.preventDefault();
    return $.ajax({
      url: '/Board/ChangeColumnName',
      type: 'POST',
      dataType: 'text',
      success: function() {
        if (data.status === "Success") {
          alert("Done");
          return $('.ColumnTitleForm'.submit());
        } else {
          return alert("Error occurs on the Database level!");
        }
      },
      error: function() {
        return alert("An error has occured when changing column name");
      }
    });
  });
};


/*
ChangeHTML = () -> 
    $('.AddTask').text 'Hello World'
 */

$(document).ready(PanelTitleClick());
