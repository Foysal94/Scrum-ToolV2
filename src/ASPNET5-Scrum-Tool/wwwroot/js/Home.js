var FormBoardSumbit, OnWelcomeButtonClick;

OnWelcomeButtonClick = function() {
  return $('#curved-green-button').on('click', function() {
    return $('#InitalButton').animate({
      'left': '-1000px'
    }, 'slow');
  });
};

FormBoardSumbit = function() {
  return $('.boardFormSubmit').on('click', function(event) {
    var boardName;
    boardName = $.trim($('#boardName').val());
    if (boardName.length < 1) {
      alert('Error, a boardName must be supplied');
      return event.preventDefault();
    }
  });
};

$(document).ready(FormBoardSumbit());
