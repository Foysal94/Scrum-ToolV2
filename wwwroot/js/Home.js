var OnWelcomeButtonClick;

OnWelcomeButtonClick = function() {
  return $('#curved-green-button').on('click', function() {
    return $('#InitalButton').animate({
      'left': '-1000px'
    }, 'slow');
  });
};

$(document).ready(function() {
  return OnWelcomeButtonClick();
});
