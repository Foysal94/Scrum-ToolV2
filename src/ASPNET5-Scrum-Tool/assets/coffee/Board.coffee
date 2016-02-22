BoardName = $('.BoardNameHeading').text()
ColumnNameForm = "<form class='ColumnTitleForm' asp-controller='Board' asp-action='ChangeColumnName' method='POST'> 
                     <input class='PreviousColumnName' type='hidden'  style='display: none;' />
                     <input asp-for='ColumnName' class='NewColumnName'>
                     <input type='submit' value='Continue' class='ColumnTitleSumbit'>
                 </form>"

PanelTitleClick = () ->
    $('.panel-heading').on 'click', () ->
          selectedColumn = $(this).parent()    
          selectedColumnID = $(selectedColumn).attr 'id'
          initalColumnName =  $(this).find('.panel-title').text()
          $.ajax
            url: '/Board/Show',
            type: 'GET',
            dataType: 'HTML'
            success: () ->
                        DoesFormExist = $('#MainColumn').find '.ColumnTitleForm'
                        if  DoesFormExist.length != 0 #If it does not equal 0, that means the form has been found
                             panelHeading = DoesFormExist.parent() #Get form parent div panel-heading
                             oldBoardName = $('.PreviousColumnName').val()
                             DoesFormExist.remove()
                             panelHeading.append "<h3 class='panel-title'></h3>"
                             panelHeading.find('.panel-title').text(oldBoardName)

                        selectedColumn.find('.panel-title').remove()     
                        selectedColumn.find('.panel-heading').append ColumnNameForm 
                        $('.PreviousColumnName').val(initalColumnName)
                        $('.NewColumnName').val(initalColumnName)
                   

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