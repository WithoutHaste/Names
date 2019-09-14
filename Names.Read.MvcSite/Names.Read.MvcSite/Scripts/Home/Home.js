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
		});
		return false; //stop event propagation
	};

	$("#submitSearch").on("click", search);
});