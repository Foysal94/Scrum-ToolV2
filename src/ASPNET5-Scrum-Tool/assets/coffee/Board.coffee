BoardName = $('.BoardNameHeading').text()
ColumnNameForm = "
                     <input class='PreviousColumnName' type='hidden'  style='display: none;' />
                     <input name='ColumnName' class='NewColumnName'>
                     <input type='submit' value='Continue' class='ColumnTitleSubmit'>
                "
TaskForm = "
             <input name='TaskContent' class='TaskContent'>
             <input type='submit' value='Continue' class='TaskFormSubmit'>
           "

ActiveTask = () ->
    $('#MainColumn').on 'mouseenter', '.Task', (event) ->
        $(this).addClass 'ActiveCard'
        $(this).append "<span class='EditPen' > <img src='~/images/EditTaskPen.png'></img> </span> "
        $(this).draggable(
                appendto: "BoardColumn"
                cursor: "pointer"
              )
    $('#MainColumn').on 'mouseleave', '.Task', (event) ->
        $(this).removeClass 'ActiveCard'
        $(this).draggable "disable"
        $('.EditPen').remove()
        


PanelTitleClick = () ->
    $('#MainColumn').on 'click', 'div.panel-heading',() ->
          PreventFormReload = $(this).find '.NewColumnName' #Clicking on form field means this event handler is called and keeps reloading the form. This code stops it.
          if  PreventFormReload.length != 0
            return 

          selectedColumn = $(this).parent()    
          initalColumnName =  $(this).find('.panel-title').text()
          
          DoesFormExist = $('#MainColumn').find '.NewColumnName'
          if  DoesFormExist.length != 0 #If it does not equal 0, that means the form has been found
                   panelHeading = DoesFormExist.parent() #Get form parent div panel-heading
                   oldBoardName = $('.PreviousColumnName').val()
                   panelHeading.html("<h3 class='panel-title'></h3>").text(oldBoardName)

          selectedColumn.find('.panel-title').html(ColumnNameForm)   
          $('.PreviousColumnName').val(initalColumnName)
          $('.NewColumnName').val(initalColumnName)
                   

SubmitColumnForm = () ->
    $('.panel-heading').on 'click', 'input.ColumnTitleSubmit', (event) ->
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
                 alert "no good "+JSON.stringify(error);
       

AddColumn = () ->
    $('#AddColumnButton').on 'click', (event) ->
        event.preventDefault()
        newColumnDataID = $('#MainColumn').children().last().prev().attr('id')
        newColumnName = 'Something' 
        $.ajax
            url:'/Board/AddColumn',
            type: 'POST',
            data: {ColumnName: newColumnName, ColumnID: newColumnDataID },
            success: (data) ->
                #alert "Hit the success part"
                $('#AddColumnButton').before data
            error: () ->
                alert "Hit the error part"
                
AddTaskForm = () ->
    $('#MainColumn').on 'click', '.AddTask', (event) ->
        event.preventDefault()
        selectedColumn = $(this).parent().parent()    
        PreventFormReload = $(selectedColumn).find 'TaskContent'
        if  PreventFormReload.length != 0
            return 
            
        selectedColumnID = $(selectedColumn).attr 'id'
        prevTask = $(this).prev()
        
        DoesFormExist = $('#MainColumn').find '.TaskContent'
        if  DoesFormExist.length != 0 #If it does not equal 0, that means the form has been found
                column = DoesFormExist.parent()
                column.find('.TaskContent').replaceWith("<a class='AddTask'> Add a task.... </a>")
                column.find('.TaskFormSubmit').remove();
                  
        $(selectedColumn).find('.AddTask').replaceWith(TaskForm)

SubmitTaskForm = () ->
     $('#MainColumn').on 'click', '.TaskFormSubmit', (event) ->
          event.preventDefault()
          selectedColumnID = $('.TaskContent').parent().parent().attr('id')
          taskContent = $('.TaskContent').val().trim()
          taskID = $('.TaskContent').prev().attr('id')
          taskID == 0 if TaskID?
          
          $.ajax
            url: '/Board/AddNewTask'
            type: 'POST'
            data: {ParentColumnID: selectedColumnID, TaskID : taskID, TaskContent: taskContent }
            success: (data) ->
                $('.TaskContent').replaceWith data
                $('.TaskFormSubmit').replaceWith '<a class="AddTask"> Add a task.... </a>'
            error : (error) ->
                 alert "no good "+JSON.stringify(error);
    
                  

$(document).ready(
    PanelTitleClick()
    SubmitColumnForm()
    AddTaskForm()
    SubmitTaskForm()
    AddColumn()
    ActiveTask()

)