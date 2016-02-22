BoardName = $('.BoardNameHeading').text()
ColumnNameForm = "<form class='ColumnTitleForm' asp-controller='Board' asp-action='ChangeColumnName' method='POST'> 
                     <input asp-for='ColumnName' class='NewColumnName'>
                     <input type='submit' value='Continue' class='ColumnTitleSumbit'>
                 </form>"

PanelTitleClick = () ->
    $('.panel-heading').on 'click', () ->
          column = $(this).parent()    
          columnID = $(column).attr 'id'

          $.ajax
            url: '/Board/' + BoardName,
            type: 'GET',
            dataType: 'HTML'
            success: () ->
                   column.find('.panel-title').remove()     
                   column.find('.panel-heading').append ColumnNameForm  

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