

BoardName = $('.BoardNameHeading').text()
m_BoardID = $('.BoardNameHeading').attr 'id'

LoadLabels = () ->
    $('.LabelListDiv').children('.TaskLabels').each () ->
           button = $(this).find('.btn')
           color = $(button).html()
           $(button).css('background',color)
  
TaskDragOptions = {
                    delay: 300                                                                                                      
                    revert:true 
                  }

TaskDropOptions = { 
                    accept: (element) ->
                         if element.hasClass('ColourLabel')
                            return true
                         return false
                         
                    drop: (event, ui) ->
                             task = $(this);
                             taskID = $(task).find('.Task').attr 'id'
                             label = $(ui.draggable)
                             labelColour = $(label).attr 'id'
                             colour = labelColour.slice(0,-5)
                             
                             $.ajax 
                                url: '/Label/Add'
                                type: 'POST'
                                data: {p_TaskID: taskID, p_LabelColour: colour }
                                success: (data) ->
                                        alert 'Label was added successfully'
                                error: (error) ->
                                    alert 'Error dropping label, TaskDropOptions'
                                    alert "no good "+JSON.stringify(error);
                  }

                                                                                  

BoardDropOptions = {
                    accept: (element) ->
                              #'this' is the column
                              if element.hasClass('ActiveTask')
                                 task = $(element)
                                 newColumnID = $(this).attr 'id'
                                 currentColumnID = $(element).parent().parent().attr 'id'
                                 return false if newColumnID == currentColumnID
                                 return true
                              return false
                    drop: (event, ui) ->
                            column = $(this)
                            selectedTaskDiv = $(ui.draggable)
                            newColumnName = $(column).find('.panel-title').text()
                            taskID = $(selectedTaskDiv).find('.Task').attr 'id'
                            currentColumnID = $(selectedTaskDiv).parent().parent().attr 'id'
                            $.ajax 
                                url: '/Task/MovedTask'
                                type: 'POST'
                                data: {p_ColumnName: newColumnName,  p_TaskID: taskID }
                                success: (data) ->
                                      $(selectedTaskDiv).remove()
                                      $(column).find('.AddTask').before () ->
                                            $(data).draggable TaskDragOptions
                                            
                                error : (error) ->
                                     alert "no good "+JSON.stringify(error);
                        
                     }
                        
$('.TaskParentDiv').draggable TaskDragOptions 
$('.TaskParentDiv').droppable TaskDropOptions           

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
                     #alert 'data is' + data
                     dialog = data
                     $(data).dialog 
                        height:700,
                        width:900,
                        modal:true,
                        resizable:false,
                        open: (event,ui) ->
                            $('.ui-widget-overlay').bind 'click', (event,ui) ->         
                                $(data).dialog('close');
                      .siblings('.ui-dialog-titlebar').removeClass 'ui-widget-header'     
                      LoadLabels()
            error : (error) ->
                 alert "ActiveTask Click method, error"
                 alert JSON.stringify(error);
        
PanelTitleClick = () ->
    $('#MainColumn').on 'click', '.ActivePanel',() ->
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
               selectedColumn.find('.panel-title').html(data)
               $('.PreviousColumnName').val(initalColumnName)
               $('.NewColumnName').val(initalColumnName)
                   

SubmitColumNameChange = () ->
    $('#MainColumn').on 'click', 'input.ColumnTitleSubmit', (event) ->
        event.preventDefault();

        newColumnName = $('.NewColumnName').val().trim()
        oldColumnName =  $('.PreviousColumnName').val().trim()
        return alert 'Please enter a name' if newColumnName == null or newColumnName == ""
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
                 
CancelColumnNameChange = () ->
    $('#MainColumn').on 'click', '.ColumnTitleCancel', (event) ->
        event.preventDefault();
        oldColumnName =  $('.PreviousColumnName').val().trim()
        column = $(this).parentsUntil '.panel-title'
        $(column).html oldColumnName
                 
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
         return alert 'Please enter a name' if newColumnName == null or newColumnName == ""
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
 
 CancelColumnForm = () -> 
    $('#MainColumn').on 'click', '.AddColumnCancel', (event) ->
        event.preventDefault()
        $('AddColumnForm').replaceWith "<a id='AddColumnButton'>Add a List</a>"
                
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
                    $(data).droppable TaskDropOptions
                $('.TaskFormSubmit').replaceWith('<a class="AddTask"> Add a task.... </a>')
            error : (error) ->
                 alert "no good "+JSON.stringify(error);
 
 CancelTaskForm = () ->
    $('#MainColumn').on 'click', '.TaskFormCancel', (event) ->
        event.preventDefault()
        $('.AddTaskForm').replaceWith "<a class='AddTask'> Add a task.... </a>"        

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
    CancelTaskForm() 
    CancelColumnNameChange() 
    CancelColumnForm()
)

