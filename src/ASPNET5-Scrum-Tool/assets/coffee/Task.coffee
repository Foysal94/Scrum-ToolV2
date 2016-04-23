


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
    $('body').on 'click', '.TaskContent', (event) ->
        event.preventDefault();
        taskContent = $('.TaskContent').html()
        $.ajax 
            url: '/Task/EditTaskForm'
            type: 'GET'
            success: (data) ->
                $('.TaskContent').replaceWith data
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
              error: (error) ->
                     alert 'EditTaskCancelClick Method'
                     alert "no good " + JSON.stringify(error);
                     
AddCommentSubmitButton = () ->
     $('body').on 'click', '.CommentFormSubmit', () ->       
            event.preventDefault();
            taskID = $('.TaskWindow').attr 'id'
            name = $('.CommentNameInput').html()
            content = $('.CommentTextArea').html()
            
            $.ajax 
                url: '/Comment/Create'
                type: 'POST'
                data { p_TaskID: taskID, p_Name: name, p_Content: content }
                success: (data) ->
                    
                error: (error) ->
                     alert 'AddCommentSubmitButton Method'
                     alert "no good " + JSON.stringify(error)
                     
ChangeDateButtonClick = () ->
        $('body').on 'click', '.ChangeDateButton', () -> 
            $.datepicker.formatDate( "dd-M-yy", new Date( 2016, 1 - 1, 26 ) );
            $('.ChangeDateButton').datepicker(
                minDate: new Date()
                onSelect: (dateText, inst) ->      
                        dateAsString = dateText
                        dateAsObject = $(this).datepicker( 'getDate' )
                        taskID = $('.TaskWindow').attr 'id'
                        $.ajax 
                          url: '/Task/UpdateDate'
                          type: 'POST'
                          data: { p_TaskID : taskID, p_Date:dateAsString}
                          success: (data) ->
                                $('.Date').html("Date: "+ dateAsString)
                          error: (error) ->
                                 alert 'EditTaskCancelClick Method'
                                 alert "no good " + JSON.stringify(error);
            )   
           
           
                  
$(document).ready(
    DeleteLabelLink()
    AddLabelClick()
    EditTaskClick()
    EditTaskContent()
    EditTaskCancelClick()
    ChangeDateButtonClick()
)
