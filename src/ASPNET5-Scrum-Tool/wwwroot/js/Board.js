var BoardName, ColumnNameForm, PanelTitleClick, SumbitColumnForm;

BoardName = $('.BoardNameHeading'.text());


/*
AjaxTest = () ->
   $('.AddTask').on 'click', (event) ->
        event.preventDefault()
        BoardName = GetBoardName()
        $.ajax({
            url: '/Board/' + BoardName,
            type: 'GET',
            dataType: 'HTML'
            success: ChangeHTML
        
        })
 */

PanelTitleClick = function() {
  return $('.panel-title').on('click', function() {
    return $.ajax({
      url: '/Board/' + BoardName,
      type: 'POST',
      dataType: 'HTML',
      success: ColumnNameForm
    });
  });
};

ColumnNameForm = function() {
  var Form;
  $('.panel-title'.remove());
  Form = "<form class='ColumnTitleForm' asp-controller='Board' asp-action='ChangeColumnName' method='POST'> <input asp-for='ColumnName' class='NewColumnName'> <input type='submit' value='Continue' class='ColumnTitleSumbit'> </form>";
  return $('.panel-heading'.append(Form));
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
