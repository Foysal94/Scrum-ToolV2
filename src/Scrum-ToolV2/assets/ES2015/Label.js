const loadLabels = function() {
  return $('.LabelListDiv').children('.TaskLabels').each(function() {
    const button = $(this).find('.btn');
    const color = $(button).html();
    return $(button).css('background', color);
  });
};

$('.ColourLabel').draggable({
  revert: true
});

$(document).ready(
   loadLabels()
)