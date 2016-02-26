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
<<<<<<< HEAD

        $.ajax
            url: '/Board/ChangeColumnName',
            type: 'POST',
            data: {ColumnName: columnName, ColumnNumber: columnNumber }
            success: (data) ->              
                     #alert "Hit the Success part"
                     #alert 'data is' + data
                     panelTitleHTML = "<h3 class='panel-title'></h3>"
                     panelHeading = $('.panel-heading').find('.NewColumnName').parent()
                     $(panelHeading).empty();
                     $(panelHeading).append(panelTitleHTML)
                     $(panelHeading).find('.panel-title').text(columnName)
                     
=======
 
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

>>>>>>> c8c888e56b2a28245cec67001eaddd58941f837d
            error: (xhr, err) ->
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
            
<<<<<<< HEAD
AddColumn = () ->
    $('#AddColumnButton').on 'click', (event) ->
        event.preventDefault()
        $.ajax
            url:'/Board/AddColumn',
            type: 'POST',
            success: (data) ->
                alert "Hit the success part"
                alert data
            error: () ->
                alert "Hit the error part"
                  
=======
         




###
ChangeHTML = () -> 
    $('.AddTask').text 'Hello World' 
###

>>>>>>> c8c888e56b2a28245cec67001eaddd58941f837d

$(document).ready(
    PanelTitleClick()
    SumbitColumnForm()
<<<<<<< HEAD
    AddColumn()
=======
>>>>>>> c8c888e56b2a28245cec67001eaddd58941f837d
)