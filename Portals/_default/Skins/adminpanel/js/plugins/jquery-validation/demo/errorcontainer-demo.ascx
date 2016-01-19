<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>


<h1 id="banner"><a href="http://bassistance.de/jquery-plugins/jquery-plugin-validation/">jQuery Validation Plugin</a> Demo</h1>
<div id="main">

<form method="get" class="cmxform" id="form1" action="">
	<fieldset>
		<legend>Login Form</legend>
		<p>
			<label>Username</label>
			<input name="user" title="Please enter your username (at least 3 characters)" class="{required:true,minlength:3}" />
		</p>
		<p>
			<label>Password</label>
			<input type="password" maxlength="12" name="password" title="Please enter your password, between 5 and 12 characters" class="{required:true,minlength:5}" />
		</p>
		<div class="error">
		</div>
		<p>
			<input class="submit" type="submit" value="Login"/>
		</p>
	</fieldset>
</form>

<!-- our error container -->
<div class="container">
	<h4>There are serious errors in your form submission, please see below for details.</h4>
	<ol>
		<li><label for="email" class="error">Please enter your email address</label></li>
		<li><label for="phone" class="error">Please enter your phone <b>number</b> (between 2 and 8 characters)</label></li>
		<li><label for="address" class="error">Please enter your address (at least 3 characters)</label></li>
		<li><label for="avatar" class="error">Please select an image (png, jpg, jpeg, gif)</label></li>
		<li><label for="cv" class="error">Please select a document (doc, docx, txt, pdf)</label></li>
	</ol>
</div>

<form class="cmxform" id="form2" method="get" action="">
	<fieldset>
		<legend>Validating a complete form</legend>
		<p>
			<label for="email">Email</label>
			<input id="email" name="email" class="{validate:{required:true,email:true}}" />
		</p>
		<p>
			<label for="agree">Favorite Color</label>
			<select id="color" name="color" title="Please select your favorite color!" class="{validate:{required:true}}">
				<option></option>
				<option>Red</option>
				<option>Blue</option>
				<option>Yellow</option>
			</select>
		</p>
		<p>
			<label for="phone">Phone</label>
			<input id="phone" name="phone" class="some styles {validate:{required:true,number:true, rangelength:[2,8]}}" />
		</p>
		<p>
			<label for="address">Address</label>
			<input id="address" name="address" class="some other styles {validate:{required:true,minlength:3}}" />
		</p>
		<p>
			<label for="avatar">Avatar</label>
			<input type="file" id="avatar" name="avatar" class="{validate:{required:true,accept:true}}" />
		</p>
		<p>
			<label for="agree">Please agree to our policy</label>
			<input type="checkbox" class="checkbox" id="agree" title="Please agree to our policy!" name="agree" class="{validate:{required:true}}" />
		</p>
		<p>
			<label for="cv">CV</label>
			<input type="file" id="cv" name="cv" class="{validate:{required:true,accept:'docx?|txt|pdf'}}" />
		</p>
		<p>
			<input class="submit" type="submit" value="Submit"/>
			<input class="cancel" type="submit" value="Cancel"/>
		</p>
	</fieldset>
</form>

<div class="container">
	<h4>There are serious errors in your form submission, please see details above the form!</h4>
</div>

<a href="<%= SkinPath %>index.html">Back to main page</a>

</div>



