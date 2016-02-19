AjaxTest = () ->
   $('.AddTask').on 'click', (event) ->
        event.preventDefault()
        
        $.ajax({
            url: '@Url.Action("Show","Board")',
            type: 'GET',
            dataType: 'HTML'
            success: ChangeHTML
        
        })
    
    

ChangeHTML = () -> 
    $('.AddTask').text 'Hello World' 
   


$(document).ready(
    
    AjaxTest()
)