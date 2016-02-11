<<<<<<< HEAD
var FormBoardSumbit, OnWelcomeButtonClick;
=======
var OnWelcomeButtonClick;
>>>>>>> 80e0797f1f6c68d1e9e129fc830cd262303c3682

OnWelcomeButtonClick = function() {
  return $('#curved-green-button').on('click', function() {
    return $('#InitalButton').animate({
      'left': '-1000px'
    }, 'slow');
  });
};

<<<<<<< HEAD
FormBoardSumbit = function() {
  return $('#boardFormSubmit').on('click', function(event) {
    var boardName;
    boardName = $.trim($('#boardName').val());
    if (boardName.length < 1) {
      alert('Error, a boardName must be supplied');
      return event.preventDefault();
    }
  });
};

$(document).ready(FormBoardSumbit());
=======
$(document).ready(function() {
  return OnWelcomeButtonClick();
});
>>>>>>> 80e0797f1f6c68d1e9e129fc830cd262303c3682
