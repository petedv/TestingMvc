(function($, window) {
	'use strict';
	var self;

	window.TestWebsite = window.TestWebsite || {};
	window.TestWebsite.TestRun = window.TestWebsite.TestRun || {};

	self = window.TestWebsite.TestRun.IncompleteRun = {
		add_handlers: function () {
			$("body").on("change keyup", ".testrunfilter", function(e) {
				self.submit_form();
			});
		},
		submit_form: function () {
			var form = $('form'),
				action = form.attr('action');
			$.get(action, form.serializeArray()).done(function (content) {
				var newData = $(content),
					newTable = newData.find('table');
				form.replaceWith(newData.find('form'));
				if (newTable.length !== 0) {
					$('table').replaceWith(newTable);
				}
			});
		}
	};

	self.add_handlers();
})(jQuery, window);