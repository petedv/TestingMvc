(function($, window) {
	'use strict';
	window.TestWebsite = window.TestWebsite || {};
	window.TestWebsite.Product = window.TestWebsite.Product || {};

	window.TestWebsite.Product.ConfigFields = {
		add_handlers: function () {
			/*
			$("body").on("click", "#addconfigfield", function(e) {
				e.preventDefault();
				var form = $("#addConfigFieldForm");
				var data = form.serializeArray();
				var url = form.attr("action");
				var posting = $.post(url, data);
				posting.done(function(result) {
					$("#addConfigFieldTab").replaceWith(result);
				});
			});
			*/
			$("body").on("click", ".removeFieldButton", function(e) {
				var button = $(this);
				var productid = $("#ProductID").val();
				var field = button.data("for");
				var url = button.parents('table').data("remove");
				var posting = $.post(url, {
					ProductID: productid,
					Name: field
				});
				posting.done(function(result) {
					$("#addConfigFieldTab").replaceWith(result);
				});

			});
		}
	};

})(jQuery, window);
