<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>


	<div id="page1" data-role="page">

		<div data-role="header">
			<h1>Welcome</h1>
		</div>

		<div data-role="content">
			<form method="GET">
				<div data-role="fieldcontain">
					<label for="email">Email:</label>
					<input type="email" name="email" id="email" />
				</div>
				<div data-role="fieldcontain">
					<label for="password">Password:</label>
					<input type="password" name="password" id="password" />
				</div>
				<input data-role="submit" type="submit" value="Login" />
			</form>
		</div>

	</div>

	<script>
		$('#page1').bind('pageinit', function(event) {
			$('form').validate({
				rules: {
					email: {
						required: true
					},
					password: {
						required: true
					}
				}
			});
		});
	</script>

