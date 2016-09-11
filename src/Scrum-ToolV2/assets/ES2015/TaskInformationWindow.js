const activeComment = function() {
	$('body').on('mouseenter', '.Comment', function() {
		$(this).addClass('ActiveComment');
		return $(this).find('.dropdown').removeClass('Hidden');
	});
	return $('body').on('mouseleave', '.Comment', function() {
		$(this).removeClass('ActiveComment');
		return $(this).find('.dropdown').addClass('Hidden');
	});
};

const deleteLabelLink = function() {
	return $('body').on('click', '.DeleteTaskLabel', function(event) {
		event.preventDefault();
		const labelParent_li = $(this).closest('.TaskLabels');
		const labelID = $(labelParent_li).attr('id');
		return $.ajax({
			url: '/Label/Delete',
			type: 'POST',
			data: {
				p_LabelID: labelID
			},
			success: function() {
				alert('Label was added successfully');
				return labelParent_li.remove();
			},
			error: function(error) {
				alert('Error while deleting Label');
				return alert("no good " + JSON.stringify(error));
			}
		});
	});
};

const addLabelClick = function() {
	return $('body').on('click', '.TaskColourLabel', function(event) {
		event.preventDefault();
		const labelColour = $(this).attr('id');
		const colour = labelColour.slice(0, -5);
		const taskID = $('.TaskWindow').attr('id');
		return $.ajax({
			url: '/Label/TaskAddLabel',
			type: 'POST',
			data: {
				p_TaskID: taskID,
				p_LabelColour: colour
			},
			success: function(data) {
				alert('Label was added successfully');
				return $('.LabelListDiv').find('ul').append(data);
			},
			error: function(error) {
				alert('AddLabelClick Method');
				return alert("no good " + JSON.stringify(error));
			}
		});
	});
};

const editTaskClick = function() {
	return $('body').on('click', '.TaskContent', function(event) {
		event.preventDefault();
		const taskContent = $('.TaskContent').html();
		return $.ajax({
			url: '/Task/EditTaskForm',
			type: 'GET',
			success: function(data) {
				$('.TaskContent').replaceWith(data);
				return $('.EditTaskContent').text(taskContent);
			},
			error: function(error) {
				alert('EditTaskButtonClick Method');
				return alert("no good " + JSON.stringify(error));
			}
		});
	});
};

const editTaskContent = function() {
	return $('body').on('click', '.EditTaskSubmit', function() {
		const taskID = $('.TaskWindow').attr('id');
		const newTaskContent = $('.NewTaskContent').val().trim();
		return $.ajax({
			url: '/Task/UpdateContent',
			type: 'POST',
			data: {
				p_TaskID: taskID,
				p_NewTaskContent: newTaskContent
			},
			success: function() {
				const column = $('.ActiveTask').parent().parent();
				const columnName = $(column).find('.panel-title').html();
				$('.EditTaskForm').replaceWith('<a class="TaskContent"></a>');
				$('.TaskContent').text(newTaskContent);
				$('.TaskContent').append("<p class='ListName'> in list " + columnName + "</p>");
				return $('.ActiveTask').find('.Task').html(newTaskContent);
			},
			error: function(error) {
				alert('EditTaskContent Method');
				return alert("no good " + JSON.stringify(error));
			}
		});
	});
};

const editTaskCancelClick = function() {
	return $('body').on('click', '.EditTaskCancel', function() {
		const taskID = $('.TaskWindow').attr('id');
		return $.ajax({
			url: '/Task/GetTaskContent',
			type: 'POST',
			data: {
				p_TaskID: taskID
			},
			success: function(data) {
				$('.EditTaskForm').replaceWith('<a class="TaskContent"></a>');
				return $('.TaskContent').text(data);
			},
			error: function(error) {
				alert('EditTaskCancelClick Method');
				return alert("no good " + JSON.stringify(error));
			}
		});
	});
};

const addCommentSubmitButton = function() {
	return $('body').on('click', '.CommentFormSubmit', function(event) {
		event.preventDefault();
		const taskID = $('.TaskWindow').attr('id');
		const name = $('.CommentNameInput').val().trim();
		const content = $('.CommentTextArea').val().trim();
		if (name === "" || content === "") {
			return alert('Missing required Fields');
		}
		return $.ajax({
			url: '/Comment/Create',
			type: 'POST',
			data: {
				p_TaskID: taskID,
				p_Name: name,
				p_Content: content
			},
			success: function(data) {
				return $('.CommentsDiv').find('.CommentForm').prev(data);
			},
			error: function(error) {
				alert('AddCommentSubmitButton Method');
				return alert("no good " + JSON.stringify(error));
			}
		});
	});
};

const changeDateButtonClick = function() {
	return $('body').on('click', '.ChangeDateButton', function() {
		$.datepicker.formatDate("dd-M-yy", new Date(2016, 1 - 1, 26));
		return $('.ChangeDateButton').datepicker({
			minDate: new Date(),
			onSelect: function(dateText) {
				const dateAsString = dateText;
				const taskID = $('.TaskWindow').attr('id');
				return $.ajax({
					url: '/Task/UpdateDate',
					type: 'POST',
					data: {
						p_TaskID: taskID,
						p_Date: dateAsString
					},
					success: function() {
						return $('.Date').html("Date: " + dateAsString);
					},
					error: function(error) {
						alert('EditTaskCancelClick Method');
						return alert("no good " + JSON.stringify(error));
					}
				});
			}
		});
	});
};

const deleteComment = function() {
	return $('body').on('click', '.DeleteCommentLink', function(event) {
		event.preventDefault();
		const comment = $(this).parents('.Comment');
		const commentID = $(comment).attr('id');
		return $.ajax({
			url: '/Comment/Delete',
			type: 'POST',
			data: {
				p_CommentID: commentID
			},
			success: function() {
				return $(comment).remove();
			},
			error: function(error) {
				alert("DeleteColumn method, error");
				return alert(JSON.stringify(error));
			}
		});
	});
};

const taskInformationWindowJS = function() {
	$(document).ready(
		deleteLabelLink(),
		addLabelClick(),
		editTaskClick(),
		editTaskContent(),
		editTaskCancelClick(),
		changeDateButtonClick(),
		addCommentSubmitButton(),
		activeComment(),
		deleteComment()
	)
}

export default taskInformationWindowJS;
