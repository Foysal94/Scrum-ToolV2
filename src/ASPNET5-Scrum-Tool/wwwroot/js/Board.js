var BoardName, ColumnNameForm, PanelTitleClick, SumbitColumnForm;

BoardName = $('.BoardNameHeading').text();

ColumnNameForm = "<form class='ColumnTitleForm' asp-controller='Board' asp-action='ChangeColumnName' method='POST'> <input asp-for='ColumnName' class='NewColumnName'> <input type='submit' value='Continue' class='ColumnTitleSumbit'> </form>";

PanelTitleClick = function() {
  return $('.panel-heading').on('click', function() {
    var column, columnID;
    column = $(this).parent();
    columnID = $(column).attr('id');
    return $.ajax({
      url: '/Board/' + BoardName,
      type: 'GET',
      dataType: 'HTML',
      success: function() {
        column.find('.panel-title').remove();
        return column.find('.panel-heading').append(ColumnNameForm);
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
