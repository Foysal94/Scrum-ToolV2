const m_BoardName = $('.BoardNameHeading').text();
const m_BoardID = $('.BoardNameHeading').attr('id');

const boardDropOptions = {
   accept: function(element) {
      if (element.hasClass('ActiveTask')) {
         const task = $(element);
         const newColumnID = $(this).attr('id');
         const currentColumnID = $(element).parent().parent().attr('id');
         if (newColumnID === currentColumnID) {
            return false;
         }
         return true;
      }
      return false;
   },

   drop: function(event, ui) {
      let selectedTaskDiv = $(ui.draggable);
      $(selectedTaskDiv).parent().css('list-style-type', 'none');
      const column = $(this);
      const newColumnName = $(column).find('.panel-title').text();
      const taskID = $(selectedTaskDiv).find('.Task').attr('id');
      const currentColumnID = $(selectedTaskDiv).parent().parent().attr('id');
      return $.ajax({
         url: '/Task/MoveTaskToNewColumn',
         type: 'POST',
         data: {
            p_ColumnName: newColumnName,
            p_TaskID: taskID
         },
         success: function(data) {
            $(selectedTaskDiv).remove();
            return $(column).find('.AddTask').before(function() {
               return $(data).draggable(TaskDragOptions);
            });
         },
         error: function(error) {
            return alert("no good " + JSON.stringify(error));
         }
      });
   }
};


const boardJS = function() {
	$(document).ready(function() {
		$('.BoardColumn').droppable(boardDropOptions);
	});
}

export default boardJS;

