const loadLabels = function() {
  return $('.LabelListDiv').children('.TaskLabels').each(function() {
    const button = $(this).find('.btn');
    const color = $(button).html();
    return $(button).css('background', color);
  });
};

const labelDragOptions = {
  revert: true
}

const labelJS = function() {
  $(document).ready(
     loadLabels(),
     $('.ColourLabel').draggable(labelDragOptions)
  )
}

export default labelJS;