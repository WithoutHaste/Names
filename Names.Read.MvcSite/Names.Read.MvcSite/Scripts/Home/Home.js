$(function () {
	const searchResults = $(".search-results")[0];
	
	const search = function() {
		searchResults.innerHTML = "<div class='loader'></div>";

		let options = {
			url: "Home/Search"
            ,type: "post"
            ,data: $("form").serialize() //includes values from form on current page
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
