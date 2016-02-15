OnWelcomeButtonClick = () ->
    $('#curved-green-button').on 'click', ->
        $('#InitalButton').animate {'left': '-1000px'}, 'slow'
        
FormBoardSumbit = ->
    $('.boardFormSubmit').on 'click', (event) ->
      boardName = $.trim $('#boardName').val()
      if boardName.length < 1
        alert 'Error, a boardName must be supplied'
        event.preventDefault()
      
        

 
$(document).ready(
    #OnWelcomeButtonClick()
    FormBoardSumbit()
)