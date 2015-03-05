(function($, window) {
	'use strict';
	var self;

	window.TestWebsite = window.TestWebsite || {};
	window.TestWebsite.TestRun = window.TestWebsite.TestRun || {};

	self = window.TestWebsite.TestRun.TestRunHistory = {
		add_handlers: function () {
			$("body").on("change keyup", ".testrun_filter", function(e) {
				self.submit_form();
			});
		},
		submit_form: function () {
			var form = $('#filter_form'),
				action = form.attr('action');
			$.get(action, form.serializeArray()).done(function (content) {
				var newData = $(content),
					newTable = newData.find('#history_table');
				form.replaceWith(newData.find('#filter_form'));
				if (newTable.length !== 0) {
					$('#history_table').replaceWith(newTable);
				}
			});
		}
	};

	self.add_handlers();
})(jQuery, window);