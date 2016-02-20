var AjaxTest, ChangeHTML, GetBoardName;

GetBoardName = function() {
  var BoardName;
  return BoardName = $('.BoardNameHeading').text();
};

AjaxTest = function() {
  return $('.AddTask').on('click', function(event) {
    var BoardName;
    event.preventDefault();
    BoardName = GetBoardName();
    return $.ajax({
      url: '/Board/' + BoardName,
      type: 'GET',
      dataType: 'HTML',
      success: ChangeHTML
    });
  });
};

ChangeHTML = function() {
  return $('.AddTask').text('Hello World');
};

$(document).ready(AjaxTest());
