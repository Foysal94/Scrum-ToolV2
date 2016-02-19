var AjaxTest, ChangeHTML;

AjaxTest = function() {
  return $('.AddTask').on('click', function(event) {
    event.preventDefault();
    return $.ajax({
      url: '@Url.Action("Show","Board")',
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
