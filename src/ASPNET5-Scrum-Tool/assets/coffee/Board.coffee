BoardName = $('.BoardNameHeading').text()
ColumnNameForm = "
                     <input class='PreviousColumnName' type='hidden'  style='display: none;' />
                     <input name='ColumnName' class='NewColumnName'>
                     <input type='submit' value='Continue' class='ColumnTitleSumbit'>
                "

PanelTitleClick = () ->
    $('.panel-heading').on 'click', () ->
          PreventFormReload = $(this).find '.NewColumnName' #Clicking on form field means this event handler is called
          if  PreventFormReload.length != 0
            return 

          selectedColumn = $(this).parent()    
          selectedColumnID = $(selectedColumn).attr 'id'
          initalColumnName =  $(this).find('.panel-title').text()

          $.ajax
            url: '/Board/Show',
            type: 'GET',
            dataType: 'HTML'
            success: () ->
                        DoesFormExist = $('#MainColumn').find '.NewColumnName'
                        if  DoesFormExist.length != 0 #If it does not equal 0, that means the form has been found
                             panelHeading = DoesFormExist.parent() #Get form parent div panel-heading
                             oldBoardName = $('.PreviousColumnName').val()
                             DoesFormExist.remove()
                             $('.ColumnTitleSumbit').remove()
                             panelHeading.append "<h3 class='panel-title'></h3>"
                             panelHeading.find('.panel-title').text(oldBoardName)

                        selectedColumn.find('.panel-title').remove()     
                        selectedColumn.find('.panel-heading').append ColumnNameForm 
                        $('.PreviousColumnName').val(initalColumnName)
                        $('.NewColumnName').val(initalColumnName)
                   

SumbitColumnForm = () ->
    $('.panel-heading').on 'click', 'input.ColumnTitleSumbit', (event) ->
        event.preventDefault();

        columnName = $('.NewColumnName').val().trim()
        columnNumber = $(this).parent().parent().attr 'id'
 
        newColumnData = { 
            'ColumnName': columnName,
            'ColumnNumber': columnNumber
        }   
        
        object = JSON.stringify(newColumnData)   

        $.ajax
            url: '/Board/ChangeColumnName',
            type: 'POST'
            dataType: 'html',
            contentType: 'application/json; charset=UTF-8'
            data: object,
            success: (data) -> 
                   
                     alert "Hit the Success part";
                     alert data

            error: (xhr, err) ->
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
            
         




###
ChangeHTML = () -> 
    $('.AddTask').text 'Hello World' 
###


$(document).ready(
    PanelTitleClick()
    SumbitColumnForm()
)