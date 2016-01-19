<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>

	<div id="page">

		<div id="header">
			<h1>Login</h1>
		</div>

		<div id="content">
			<p id="status"></p>
			<form action="" method="get" id="login">
				<fieldset>
					<legend>User details</legend>
					<ul>
						<li>
							<label for="email"><span class="required">Email address</span></label>
							<input id="email" name="email" class="text required email" type="text" />
							<label for="email" class="error">This must be a valid email address</label>
						</li>
						
						<li>
							<label for="password"><span class="required">Password</span></label>
							<input name="password" type="password" class="text required" id="password" minlength="4" maxlength="20" />
						</li>

						<li>
							<label class="centered info"><a id="forgotpassword" href="<%= SkinPath %>#">Email my password...</a></label>
						</li>
					</ul>
				</fieldset>
				
				<fieldset class="submit">
					<input type="submit" class="button" value="Login..." />
				</fieldset>
				
				<div class="clear"></div>
			</form>
			
			</div>
	</div>
	

