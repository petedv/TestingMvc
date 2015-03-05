(function($, window) {
	'use strict';
	var self;

	window.TestWebsite = window.TestWebsite || {};
	window.TestWebsite.TestRun = window.TestWebsite.TestRun || {};

	self = window.TestWebsite.TestRun.ManualRun = {
		add_handlers: function () {
			$("body").on("change keyup", "#area_filter", function(e) {
				var form = $('form'),
					action = form.attr('action'),
					get = $.get(action, form.serialize());
				get.done(function (data) {
					var newTable = $(data).children('#testcasetable'),
						currentTable = $("#testcasetable");
					currentTable.replaceWith(newTable);
				});
			});

		}
	};

	self.add_handlers();

})(jQuery, window);
