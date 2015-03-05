(function($, window) {
	'use strict';
	window.TestWebsite = window.TestWebsite || {};

	var self = window.TestWebsite.Shared = {
		display_alert_cookie: function () {
			var message = $.cookie("FlashMessage"),
				cls = 'success',
				parts;
			if (message !== undefined) {
				parts = message.split("|");
				if (parts.length > 1) {
					cls = parts[0];
					message = parts[1];
				}
				self.display_alert(message, cls);
				$.removeCookie("FlashMessage", {path: '/'});
			}
			
		},
		display_alert: function (message, cls) {
			var alert = $("<div>").addClass("alert-box radius"),
				cls = cls === undefined ? 'success' : cls,
				a = $('<a>');
			if (message !== undefined) {
				//Create a new alert div
				alert.addClass(cls)
					.attr("data-alert", true)
					.text(message);
				a.attr('href', '#').addClass('close').html('&times;');
				alert.append(a);
				//Add the new alert to its container
				$("#alerts").empty();
				$("#alerts").append(alert);
				$("#alerts").foundation();
			}
		}
	};
})(jQuery, window);