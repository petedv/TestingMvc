(function($, window) {
	'use strict';
	var self;

	window.TestWebsite = window.TestWebsite || {};
	window.TestWebsite.TestRun = window.TestWebsite.TestRun || {};

	self = window.TestWebsite.TestRun.ViewRun = {
		add_handlers: function () {
			$("body").on("change keyup", ".testrun_filter", function(e) {
				self.submit_form();
			});
		},
		submit_form: function () {
			var form = $('#testrunform'),
				action = form.attr('action');
			$.get(action, form.serializeArray()).done(function (content) {
				var newData = $(content),
					newDetails = newData.find('#testrundetails');
				form.replaceWith(newData.find('#testrunform'));
				if (newDetails.length !== 0) {
					$('#testrundetails').replaceWith(newDetails);
				}
				$(window.document).foundation();
			});
		}
	};
	self.add_handlers();
})(jQuery, window);