BoardName = $('.BoardNameHeading').text()
m_BoardID = $('.BoardNameHeading').attr 'id'
ColumnNameForm = "<div class='ColumnNameForm'>
                     <input class='PreviousColumnName' type='hidden'  style='display: none;' />
                     <input name='ColumnName' class='NewColumnName'>
                     <input type='submit' value='Continue' class='ColumnTitleSubmit'>
                 </div>"

AddColumnForm = "<div class='AddColumnForm'>
                     <input name='ColumnName' class='NewColumnName'>
                     <input type='submit' value='Continue' class='AddColumnSubmit'>
                 </div>"
                                  
TaskForm = "
             <input name='TaskContent' class='TaskContent'>
             <input type='submit' value='Continue' class='TaskFormSubmit'>
           "

TaskDragOptions = {
                    appendto: "BoardColumn"
                    delay: 300
                    snap: "BoardColumn"                                                                                                        
                    revert:true
                  }     

BoardDropOptions = {
                    accept: (element) ->
                              if element.hasClass('Task')
                                 columnID = $(this).attr 'id'
                                 cardParentColumnID = $(element).parent().parent().attr 'id'
                                 return false if columnID == cardParentColumnID
                                 return true
                              return false
                    drop: (event, ui) ->
                            column = $(this)
                            selectedTask = $(ui.draggable)
                            newColumnName = $(column).find('.panel-title').text()
                            taskID = $(selectedTask).attr 'id'
                            currentColumnID = $(selectedTask).parent().parent().attr 'id'
                            $.ajax 
                                url: '/Task/MovedTask'
                                type: 'POST'
                                data: {p_ColumnName: newColumnName,  p_TaskID: taskID }
                                success: (data) ->
                                      $(selectedTask).remove()
                                      $(column).find('.AddTask').before () ->
                                            $(data).draggable TaskDragOptions
                                            
                                error : (error) ->
                                     alert "no good "+JSON.stringify(error);
                        
                     }
                        
$('.Task').draggable TaskDragOptions      
$('.BoardColumn').droppable BoardDropOptions

ActiveTask = () ->
    $('#MainColumn').on 'mouseenter', '.Task', (event) ->
        $(this).addClass 'ActiveCard'
        #$(this).append "<span class='EditPen' > <img src='~/images/EditTaskPen.png'></img> </span> "                
    $('#MainColumn').on 'mouseleave', '.Task', (event) ->
        $(this).removeClass 'ActiveCard'
        #$(this).draggable "disable"
        $('.EditPen').remove()
        
        
PanelTitleClick = () ->
    $('#MainColumn').on 'click', 'div.panel-heading',() ->
          PreventFormReload = $(this).find '.NewColumnName' #Clicking on form field means this event handler is called and keeps reloading the form. This code stops it.
          if  PreventFormReload.length != 0
            return 

          selectedColumn = $(this).parent()    
          initalColumnName =  $(this).find('.panel-title').text()
          
          DoesFormExist = $('#MainColumn').find '.AddColumnForm'
          $('.AddColumnForm').replaceWith '<a id="AddColumnButton">Add a List</a>'
          
          DoesFormExist = $('#MainColumn').find '.ColumnNameForm'
          if  DoesFormExist.length != 0 #If it does not equal 0, that means the form has been found
                   panelHeading = DoesFormExist.parent() #Get form parent div panel-heading
                   oldBoardName = $('.PreviousColumnName').val()
                   panelHeading.html("<h3 class='panel-title'></h3>").text(oldBoardName)

          selectedColumn.find('.panel-title').html(ColumnNameForm)   
          $('.PreviousColumnName').val(initalColumnName)
          $('.NewColumnName').val(initalColumnName)
                   

SubmitColumNameChange = () ->
    $('#MainColumn').on 'click', 'input.ColumnTitleSubmit', (event) ->
        event.preventDefault();

        newColumnName = $('.NewColumnName').val().trim()
        oldColumnName =  $('.PreviousColumnName').val().trim()
        found = false;
        

        $.ajax
            url: '/Column/ChangeColumnName',
            type: 'POST',
            data: {p_OldColumnName : oldColumnName, p_NewColumnName : newColumnName,  p_BoardID: m_BoardID },
            dataType: 'json',
            success: (data) ->              
                     alert 'data is' + data
                     panelHeading = $('.panel-heading').find('.NewColumnName').parent()
                     $(panelHeading).html("<h3 class='panel-title'> <h3>").text(newColumnName)
                     
            error : (error) ->
                 alert "SubmitColumnForm Method, error"
                 alert JSON.stringify(error);
       

AddColumnButtonClick = () ->
    $('#MainColumn').on 'click', '#AddColumnButton', (event) ->
        event.preventDefault()
        $('#AddColumnButton').replaceWith AddColumnForm
        DoesFormExist = $('#MainColumn').find '.ColumnNameForm'
        if  DoesFormExist.length != 0 #If it does not equal 0, that means the form has been found
               panelHeading = DoesFormExist.parent() #Get form parent div panel-heading
               oldBoardName = $('.PreviousColumnName').val()
               panelHeading.html("<h3 class='panel-title'></h3>").text(oldBoardName)

SubmitAddColumn = () ->
    $('#MainColumn').on 'click', '.AddColumnSubmit', (event) ->
         event.preventDefault()
         newColumnName = $('.NewColumnName').val().trim()
         found = false;
        
        
         $.ajax
            url:'/Column/AddColumn',
            type: 'POST',
            data: {Name: newColumnName, BoardID: m_BoardID },
            
            success: (data) ->
                $('.AddColumnForm').after '<a id="AddColumnButton">Add a List</a>'
                $('.AddColumnForm').replaceWith () ->
                    $(data).droppable BoardDropOptions
            error: (error) ->
                 alert "SubmitAddColumn method error"
                 alert  JSON.stringify(error);
                
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
          selectedColumnName = $('.TaskContent').parent().parent().find('.panel-title').text();
          taskContent = $('.TaskContent').val().trim()
          
          $.ajax
            url: '/Task/AddNewTask'
            type: 'POST'
            data: { BoardID: m_BoardID, ColumnName: selectedColumnName, TaskContent: taskContent}
            success: (data) ->
                $('.TaskContent').replaceWith () ->
                    $(data).draggable TaskDragOptions
                $('.TaskFormSubmit').replaceWith('<a class="AddTask"> Add a task.... </a>')
            error : (error) ->
                 alert "no good "+JSON.stringify(error);
    
                  

$(document).ready(
    PanelTitleClick()
    SubmitColumNameChange()
    AddTaskForm()
    SubmitTaskForm()
    AddColumnButtonClick()
    ActiveTask()
    SubmitAddColumn() 
)