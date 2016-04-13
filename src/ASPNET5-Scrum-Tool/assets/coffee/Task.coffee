DeleteLabelLink = () ->
     $('body').on 'click', '.DeleteTaskLabel', (event) ->
        event.preventDefault()
        labelParent_li = $(this).closest('.TaskLabels')
        labelID = $(labelParent_li).attr 'id'
        
        $.ajax
            url: '/Label/Delete'
            type: 'POST'
            data: {p_LabelID: labelID}
            success: (data) ->
                  alert 'Label was added successfully'
                  labelParent_li.remove()
            error: (error) ->
                  alert 'Error while deleting Label'
                  alert "no good "+JSON.stringify(error);
                  
AddLabelClick = () ->
    $('body').on 'click', '.TaskColourLabel', (event) ->
        event.preventDefault();
        labelColour = $(this).attr 'id'
        colour = labelColour.slice(0,-5)
        taskID = $('.TaskWindow').attr 'id'
        
        $.ajax 
              url: '/Label/TaskAddLabel'
              type: 'POST'
              data: {p_TaskID: taskID, p_LabelColour: colour }
              success: (data) ->
                         alert 'Label was added successfully'
                         $('.LabelListDiv').find('ul').append data
              error: (error) ->
                     alert 'AddLabelClick Method'
                     alert "no good " + JSON.stringify(error);
                     
EditTaskClick = () ->
    $('body').on 'click', '.EditTaskButton', (event) ->
        event.preventDefault();
        taskContent = $('.TaskContent').html()
        $.ajax 
            url: '/Task/EditTaskForm'
            type: 'GET'
            success: (data) ->
                $('.TaskContent').remove()
                $('.EditTaskButton').replaceWith data
                $('.EditTaskContent').text taskContent
            error: (error) ->
                     alert 'EditTaskButtonClick Method'
                     alert "no good " + JSON.stringify(error);
                
 EditTaskContent = () ->
      $('body').on 'click', '.EditTaskSubmit', () ->  
          taskID = $('.TaskWindow').attr 'id'
          taskContent = $('.NewTaskContent').val().trim()
          
          $.ajax 
              url: '/Task/UpdateContent'
              type: 'POST'
              data: { p_TaskID : taskID, p_NewTaskContent: taskContent }
              success: () ->
                    $('.EditTaskForm').replaceWith  '<a class="TaskContent"></a>'
                    $('.TaskContent').text taskContent
                    $('.TaskContent').after '<a class="EditTaskButton"></a>'
                    $('.EditTaskButton').text 'Edit Task Content....'
                   
              
              error: (error) ->
                     alert 'EditTaskContent Method'
                     alert "no good " + JSON.stringify(error);

 EditTaskCancelClick = () -> 
    $('body').on 'click', '.EditTaskCancel', () ->  
        taskID = $('.TaskWindow').attr 'id'
         
        $.ajax 
              url: '/Task/GetTaskContent'
              type: 'POST'
              data: { p_TaskID : taskID }
              success: (data) ->
                    $('.EditTaskForm').replaceWith  '<a class="TaskContent"></a>'
                    $('.TaskContent').text data
                    $('.TaskContent').after '<a class="EditTaskButton"></a>'
                    $('.EditTaskButton').text 'Edit Task Content....'
                   
              
              error: (error) ->
                     alert 'EditTaskCancelClick Method'
                     alert "no good " + JSON.stringify(error);
                 
                  
$(document).ready(
    DeleteLabelLink()
    AddLabelClick()
    EditTaskClick()
    EditTaskContent()
    EditTaskCancelClick()
)
