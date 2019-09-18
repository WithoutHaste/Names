$(function () {
	const searchResults = $(".search-results")[0];
	
	const search = function() {
		searchResults.innerHTML = "<div class='loader'></div>";

		let options = {
			url: "Home/Search"
            ,type: "post"
            ,data: $("form").serialize() //includes values from form on current page
			, error: function (jqXHR, textStatus, errorThrown) {
				searchResults.innerHTML = "Error: " + errorThrown;
			}
		};
		$.ajax(options).done(function (data) {
			searchResults.innerHTML = data;
			enableExpandCollapse();
		});
		return false; //stop event propagation
	};

	$("#submitSearch").on("click", search);
});

function enableExpandCollapse() {
	const namesCount = $(".names-count")[0];
	if (namesCount != undefined) {
		if ($(namesCount).data("names-count") < 400) {
			//do not enable expand/collapse, just leave all names visible
			return;
		}
	}

	$(".expand-collapse").each(function () {
		$(this).removeClass("expand-disabled"); //enable operation
		$(this).addClass("collapsed"); //start with all segments collapsed

		$(this).find(".button-expand").on("click", () => {
			$(this).addClass("expanded");
			$(this).removeClass("collapsed");
		});

		$(this).find(".button-collapse").on("click", () => {
			$(this).addClass("collapsed");
			$(this).removeClass("expanded");
		});
	});
}

enableExpandCollapse();
