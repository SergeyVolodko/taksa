$(function () {
	var tokenUi =
		"<h1 class='input'><input placeholder='Enter authorization token here...' id='input_apiKey' name='apiKey' type='text' /></h1>";
	$(tokenUi).insertBefore("#resources_container");

	$("#input_apiKey").change(addApiKeyAuthorization);
	function addApiKeyAuthorization() {
		var key = encodeURIComponent($('#input_apiKey')[0].value);
		if (key && key.trim() !== "") {
			var apiKeyAuth = new window.SwaggerClient.ApiKeyAuthorization("Authorization", "Bearer " + key, "header");
			window.swaggerUi.api.clientAuthorizations.add("bearer", apiKeyAuth);
			log("Set bearer token: " + key);
		}
	};
})();