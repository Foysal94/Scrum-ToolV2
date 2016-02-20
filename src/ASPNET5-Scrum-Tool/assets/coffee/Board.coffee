
GetBoardName = () ->
    BoardName = $('.BoardNameHeading').text()

AjaxTest = () ->
   $('.AddTask').on 'click', (event) ->
        event.preventDefault()
        BoardName = GetBoardName()
        $.ajax({
            url: '/Board/' + BoardName,
            type: 'GET',
            dataType: 'HTML'
            success: ChangeHTML
        
        })
 
PanelTitleClick = () ->
    $('.panel-title').on 'click', () ->
          $.ajax({
            url: '/Board/' + BoardName,
            type: 'POST',
            dataType: 'HTML'

            })

ChangePanelTitle = () ->


ChangeHTML = () -> 
    $('.AddTask').text 'Hello World' 



$(document).ready(
    
    AjaxTest()
)