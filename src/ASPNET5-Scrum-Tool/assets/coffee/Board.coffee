BoardName = $('.BoardNameHeading').text()
m_BoardID = $('.BoardNameHeading').attr 'id'
  
TaskDragOptions = {
                    delay: 300                                                                                                      
                    revert:true
                    helper: 'clone'
                        
                  }

TaskDropOptions = { 
                    accept: (element) ->
                         if element.hasClass('ColorLabel')
                            return true
                         return false
                         
                    drop: (event, ui) ->
                             task = $(this);
                             taskID = $(task).attr 'id'
                             label = $(ui)
                             labelColour = $(label).find('a').html();
                             
                             $.ajax 
                                url: '/Task/AddLabel'
                                type: 'POST'
                                data: {p_TaskID: taskID, p_LabelColour: labelColour }
                                success: (data) ->
                                        alert 'Label was added successfully'
                                error: (error) ->
                                    alert "no good "+JSON.stringify(error);
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

$('.ColourLabel').draggable 
                    revert:true;


                    
                    

ActiveTask = () ->
    $('#MainColumn').on 'mouseenter', '.TaskParentDiv', (event) ->
        $(this).addClass 'ActiveTask'
        $(this).find('.dropdown').removeClass 'Hidden'               
    $('#MainColumn').on 'mouseleave', '.TaskParentDiv', (event) ->
        $(this).removeClass 'ActiveTask'
        $(this).find('.dropdown').addClass 'Hidden'
   
      
ActiveColumn = () ->
    $('#MainColumn').on 'mouseenter', '.panel-title', (event) ->
        $(this).addClass 'ActivePanel'           
    $('#MainColumn').on 'mouseleave', '.panel-title', (event) ->
        $(this).removeClass 'ActivePanel'
        #$(this).draggable "disable"

ActiveTaskClick = () ->
    $('#MainColumn').on 'click', '.ActiveTask', (event) ->
        event.preventDefault();
        task = $(this).find '.Task'
        taskID = $(task).attr 'id'
        $.ajax
            url: '/Task/Information/',
            type: 'GET',
            dataType: 'html'
            data: {p_TaskID: taskID},
            success: (data) ->              
                     alert 'data is' + data
                     $(data).dialog 
                        height:500,
                        width:500,
                        modal:true,
                        resizable:false,
            error : (error) ->
                 alert "ActiveTask Click method, error"
                 alert JSON.stringify(error);
        
PanelTitleClick = () ->
    $('#MainColumn').on 'click', '.panel-title',() ->
          PreventFormReload = $(this).find '.NewColumnName' #Clicking on form field means this event handler is called and keeps reloading the form. This code stops it.
          if  PreventFormReload.length != 0
            return 

          selectedColumn = $(this).parent()    
          initalColumnName =  $(this).text()
          
          DoesFormExist = $('#MainColumn').find '.AddColumnForm'
          $('.AddColumnForm').replaceWith '<a id="AddColumnButton">Add a List</a>'
          
          DoesFormExist = $('#MainColumn').find '.ColumnNameForm'
          if  DoesFormExist.length != 0 #If it does not equal 0, that means the form has been found
                   panelHeading = DoesFormExist.parent() #Get form parent div panel-heading
                   oldBoardName = $('.PreviousColumnName').val()
                   panelHeading.html("<h3 class='panel-title'></h3>").text(oldBoardName)
          
          $.get '/Board/ColumnNameChangeForm', (data) -> 
               selectedColumn.find('.panel-title').html(ColumnNameForm)
               $('.PreviousColumnName').val(initalColumnName)
               $('.NewColumnName').val(initalColumnName)
                   

SubmitColumNameChange = () ->
    $('#MainColumn').on 'click', 'input.ColumnTitleSubmit', (event) ->
        event.preventDefault();

        newColumnName = $('.NewColumnName').val().trim()
        oldColumnName =  $('.PreviousColumnName').val().trim()
       
        found = false
        $('.panel-title').each (index,element) ->
             columnName = $(element).text()
             if columnName == newColumnName
                found = true
                return false

        return alert 'That ColumnName is already in use' if found == true;
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
                 
DeleteColumnLinkClick = () ->
    $('#MainColumn').on 'click', '.DeleteColumnLink', (event) ->
        event.preventDefault()
        column = $(this).parent().parent().parent().parent().parent()
        columnID = $(column).attr 'id'
        $.ajax
            url: '/Column/Delete',
            type: 'POST',
            data: {p_ColumnID: columnID},
            success: (data) ->              
                     $(column).remove()
                     
            error : (error) ->
                 alert "DeleteColumn method, error"
                 alert JSON.stringify(error);
                 
DeleteTaskLinkClick = () ->
    $('#MainColumn').on 'click', '.DeleteTaskLink', (event) ->
     event.preventDefault()
     task = $('.ActiveTask').find '.Task'
     taskID = $(task).attr 'id'
     $.ajax
         url: '/Task/Delete',
         type: 'POST',
         data: {p_TaskID: taskID},
         success: (data) ->              
                     $(task).parent().remove() #Get hold of parentDivTask and Remove that
                     
         error: (error) ->
                   alert "DeleteTask method, error"
                   alert JSON.stringify(error);
            

AddColumnButtonClick = () ->
    $('#MainColumn').on 'click', '#AddColumnButton', (event) ->
        event.preventDefault()
        $.get '/Board/AddColumnForm', (data) -> 
              $('#AddColumnButton').replaceWith data
        DoesFormExist = $('#MainColumn').find '.ColumnNameForm'
        if  DoesFormExist.length != 0 #If it does not equal 0, that means the form has been found
               panelHeading = DoesFormExist.parent() #Get form parent div panel-heading
               oldBoardName = $('.PreviousColumnName').val()
               panelHeading.html("<h3 class='panel-title'></h3>").text(oldBoardName)

SubmitAddColumn = () ->
    $('#MainColumn').on 'click', '.AddColumnSubmit', (event) ->
         event.preventDefault()
         newColumnName = $('.NewColumnName').val().trim()
         found = false
         $('.panel-title').each (index,element) ->
             columnName = $(element).text()
             if columnName == newColumnName
                found = true
                return false

         return alert 'That ColumnName is already in use' if found == true;
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
                
        $.get '/Board/AddTaskForm', (data) ->          
            $(selectedColumn).find('.AddTask').replaceWith data

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
    ActiveColumn()
    DeleteColumnLinkClick()
    DeleteTaskLinkClick()
    ActiveTaskClick()
)