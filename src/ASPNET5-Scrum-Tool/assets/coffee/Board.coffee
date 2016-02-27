BoardName = $('.BoardNameHeading').text()
ColumnNameForm = "
                     <input class='PreviousColumnName' type='hidden'  style='display: none;' />
                     <input name='ColumnName' class='NewColumnName'>
                     <input type='submit' value='Continue' class='ColumnTitleSumbit'>
                "

PanelTitleClick = () ->
    $('#MainColumn').on 'click', 'div.panel-heading',() ->
          PreventFormReload = $(this).find '.NewColumnName' #Clicking on form field means this event handler is called and keeps reloading the form. This code stops it.
          if  PreventFormReload.length != 0
            return 

          selectedColumn = $(this).parent()    
          selectedColumnID = $(selectedColumn).attr 'id'
          initalColumnName =  $(this).find('.panel-title').text()

          $.ajax
            url: '/Board/Show/' + BoardName,
            type: 'GET',
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


        $.ajax
            url: '/Board/ChangeColumnName',
            type: 'POST',
            data: {ColumnName: columnName, ColumnNumber: columnNumber },
            dataType: 'json',
            success: (data) ->              
                     #alert 'data is' + data
                     panelHeading = $('.panel-heading').find('.NewColumnName').parent()
                     $(panelHeading).html("<h3 class='panel-title'> <h3>").text(columnName)
                     
             error : (error) ->
                 alert("no good "+JSON.stringify(error));
       

AddColumn = () ->
    $('#AddColumnButton').on 'click', (event) ->
        event.preventDefault()
        newColumnDataID = $('#MainColumn').children().last().prev().attr('id')
        newColumnName = 'Something' 
        $.ajax
            url:'/Board/AddColumn',
            type: 'POST',
            data: {ColumnName: newColumnName, ColumnNumber: newColumnDataID },
            success: (data) ->
                #alert "Hit the success part"
                $('#AddColumnButton').before data
            error: () ->
                alert "Hit the error part"
                  

$(document).ready(
    PanelTitleClick()
    SumbitColumnForm()

    AddColumn()

)