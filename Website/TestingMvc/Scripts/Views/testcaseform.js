(function($, window) {
	'use strict';
	window.TestWebsite = window.TestWebsite || {};
	window.TestWebsite.TestCase = window.TestWebsite.TestCase || {};

	window.TestWebsite.TestCase.AddTest = {
		add_handlers: function () {
			//Product Select refreshes content on change.
			$("#body").on("change keyup", "#ProductID", function (e) {
				var form = $("#addTestForm"),
					url = form.attr("action"),
					prodSelect = $(this),
					data = {};
				data[prodSelect.attr('name')] = prodSelect.children('option:selected').val();
				$.get(url, data).done(function(content) {
					form.replaceWith(content);
				});
			});
			//When we have a validation message, add the error class to the label and field.
			var labelsWithErrors = $("label span.error");
			labelsWithErrors.parent().addClass("error");
			labelsWithErrors.siblings().addClass("error");
		}
	};

	window.TestWebsite.TestCase.EditTest = {
		add_handlers: function () {
			
			//When we have a validation message, add the error class to the label and field.
			var labelsWithErrors = $("label span.error");
			labelsWithErrors.parent().addClass("error");
			labelsWithErrors.siblings().addClass("error");
		}
	};



})(jQuery, window);