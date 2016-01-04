 OnWelcomeButtonClick = () ->
    $('#curved-green-button').on 'click', ->
        $('#InitalButton').animate {'left': '-1000px'}, 'slow'
 
 $(document).ready ->
    OnWelcomeButtonClick()