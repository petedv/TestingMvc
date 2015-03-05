$(function() {
	/*
	var add_change_event = function($select, $button) {
		$select.change(function() {
			var data = $("#filterform").serializeArray();
			$.get(action, data, function (resp) {
				$("#filterform").replaceWith(resp);
				add_change_event($("#db_products"), $("#btn_search"));
			});
		});
	};

	add_change_event($('#db_products'));
	*/

	$("body").on("change keyup", "#db_products", function ()  {
		var action = $("#filterform").attr("data-filter");
		var data = $("#filterform").serializeArray();
		$.get(action, data, function (resp) {
			$("#filterform").replaceWith(resp);
		});
	});

	$("body").on("click", "#btn_search", function () {
		$("#filterform").submit();
	});

});