(function($, window) {
	'use strict';
	var self;

	window.TestWebsite = window.TestWebsite || {};
	window.TestWebsite.TestRun = window.TestWebsite.TestRun || {};

	self = window.TestWebsite.TestRun.CompleteTest = {
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
			$('#btn_pass').click(function () {
				self.submit_form(true);
			});
			$('#btn_fail').click(function () {
				self.submit_form(false);
			});
		},
		submit_form: function (passed) {
			var form = $('form');
			$("<input>", {
				type: "hidden",
				name: "Result",
				value: passed
			}).appendTo(form);
			form.submit();
		}
	};

	self.add_handlers();

})(jQuery, window);