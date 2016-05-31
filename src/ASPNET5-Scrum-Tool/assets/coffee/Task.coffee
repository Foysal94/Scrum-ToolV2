
ActiveComment = () ->
    $('body').on 'mouseenter', '.Comment', (event) ->
        $(this).addClass 'ActiveComment'
        $(this).find('.dropdown').removeClass 'Hidden'     
    $('body').on 'mouseleave', '.Comment', (event) ->
        $(this).removeClass 'ActiveComment'
        $(this).find('.dropdown').addClass 'Hidden'


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
                    column =  $('.ActiveTask').parent().parent()
                    columnName = $(column).find('.panel-title').html()
                    $('.EditTaskForm').replaceWith  '<a class="TaskContent"></a>'
                    $('.TaskContent').text taskContent
                    $('.TaskContent').append "<p class='ListName'> in list " + columnName + "</p>"
                    $('.ActiveTask').find('.Task').html taskContent
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
     $('body').on 'click', '.CommentFormSubmit', (event) ->       
            event.preventDefault();
            taskID = $('.TaskWindow').attr 'id'
            name = $('.CommentNameInput').val().trim()
            content = $('.CommentTextArea').val().trim()
            return alert 'Missing required Fields' if name == "" or content == ""
            $.ajax 
                url: '/Comment/Create'
                type: 'POST'
                data: { p_TaskID: taskID, p_Name: name, p_Content: content }
                success: (data) -> 
                    $('.CommentsDiv').find('.CommentForm').prev data
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
            
DeleteComment = () ->
     $('body').on 'click', '.DeleteCommentLink', (event) ->
        event.preventDefault();
        comment = $(this).parents '.Comment'
        commentID = $(comment).attr 'id'
        
        $.ajax
            url: '/Comment/Delete',
            type: 'POST',
            data: {p_CommentID: commentID},
            success: (data) ->              
                     $(comment ).remove()
            error : (error) ->
                 alert "DeleteColumn method, error"
                 alert JSON.stringify(error);
           
                  
$(document).ready(
    DeleteLabelLink()
    AddLabelClick()
    EditTaskClick()
    EditTaskContent()
    EditTaskCancelClick()
    ChangeDateButtonClick()
    AddCommentSubmitButton()
    ActiveComment()
    DeleteComment()
)
