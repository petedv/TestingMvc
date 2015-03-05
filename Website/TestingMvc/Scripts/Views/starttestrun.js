(function($, window) {
	'use strict';
	var self;

	window.TestWebsite = window.TestWebsite || {};
	window.TestWebsite.TestRun = window.TestWebsite.TestRun || {};

	self = window.TestWebsite.TestRun.StartTest = {
		add_handlers: function () {
			$("body").on("change keyup", "#productSelect", function(e) {
				var form = $('form'),
					action = form.attr('action'),
					get = $.get(action, form.serialize());
				get.done(function (data) {
					var innerForm = $(data).children('form');
					form.replaceWith(innerForm);
				});
			});
		}
	};

	self.add_handlers();

})(jQuery, window);