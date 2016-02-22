BoardName = $ '.BoardNameHeading'.text()
###
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
###
 
PanelTitleClick = () ->
    $('.panel-title').on 'click', () ->
          $.ajax
            url: '/Board/' + BoardName,
            type: 'POST',
            dataType: 'HTML'
            success: ColumnNameForm
            

ColumnNameForm = () -> 
        $ '.panel-title' .remove();
        Form = "<form class='ColumnTitleForm' asp-controller='Board' asp-action='ChangeColumnName' method='POST'> 
                    <input asp-for='ColumnName' class='NewColumnName'>
                    <input type='submit' value='Continue' class='ColumnTitleSumbit'>
                </form>"
        $ '.panel-heading'.append Form

SumbitColumnForm = () ->
    $('.ColumnTitleSumbit').on 'click', (event) ->
        event.preventDefault();

        $.ajax
            url: '/Board/ChangeColumnName',
            type: 'POST'
            dataType: 'text',
            success: () -> 
                if data.status == "Success" 
                    alert "Done";
                    $ '.ColumnTitleForm'.submit(); 

                else 
                    alert "Error occurs on the Database level!" ;
                
            
            error: () -> 
                alert("An error has occured when changing column name");
            
         




###
ChangeHTML = () -> 
    $('.AddTask').text 'Hello World' 
###


$(document).ready(
    PanelTitleClick()
)