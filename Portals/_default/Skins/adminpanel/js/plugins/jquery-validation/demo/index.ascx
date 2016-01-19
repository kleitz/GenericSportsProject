<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>


<h1 id="banner"><a href="http://bassistance.de/jquery-plugins/jquery-plugin-validation/">jQuery Validation Plugin</a> Demo</h1>
<div id="main">

<p>Default submitHandler is set to display an alert into of submitting the form</p>

<form class="cmxform" id="commentForm" method="get" action="">
	<fieldset>
		<legend>Please provide your name, email address (won't be published) and a comment</legend>
		<p>
			<label for="cname">Name (required, at least 2 characters)</label>
			<input id="cname" name="name" minlength="2" type="text" required />
		<p>
			<label for="cemail">E-Mail (required)</label>
			<input id="cemail" type="email" name="email" required />
		</p>
		<p>
			<label for="curl">URL (optional)</label>
			<input id="curl" type="url" name="url" />
		</p>
		<p>
			<label for="ccomment">Your comment (required)</label>
			<textarea id="ccomment" name="comment" required></textarea>
		</p>
		<p>
			<input class="submit" type="submit" value="Submit"/>
		</p>
	</fieldset>
</form>

<form class="cmxform" id="signupForm" method="get" action="">
	<fieldset>
		<legend>Validating a complete form</legend>
		<p>
			<label for="firstname">Firstname</label>
			<input id="firstname" name="firstname" type="text" />
		</p>
		<p>
			<label for="lastname">Lastname</label>
			<input id="lastname" name="lastname" type="text" />
		</p>
		<p>
			<label for="username">Username</label>
			<input id="username" name="username" type="text" />
		</p>
		<p>
			<label for="password">Password</label>
			<input id="password" name="password" type="password" />
		</p>
		<p>
			<label for="confirm_password">Confirm password</label>
			<input id="confirm_password" name="confirm_password" type="password" />
		</p>
		<p>
			<label for="email">Email</label>
			<input id="email" name="email" type="email" />
		</p>
		<p>
			<label for="agree">Please agree to our policy</label>
			<input type="checkbox" class="checkbox" id="agree" name="agree" />
		</p>
		<p>
			<label for="newsletter">I'd like to receive the newsletter</label>
			<input type="checkbox" class="checkbox" id="newsletter" name="newsletter" />
		</p>
		<fieldset id="newsletter_topics">
			<legend>Topics (select at least two) - note: would be hidden when newsletter isn't selected, but is visible here for the demo</legend>
			<label for="topic_marketflash">
				<input type="checkbox" id="topic_marketflash" value="marketflash" name="topic" />
				Marketflash
			</label>
			<label for="topic_fuzz">
				<input type="checkbox" id="topic_fuzz" value="fuzz" name="topic" />
				Latest fuzz
			</label>
			<label for="topic_digester">
				<input type="checkbox" id="topic_digester" value="digester" name="topic" />
				Mailing list digester
			</label>
			<label for="topic" class="error">Please select at least two topics you'd like to receive.</label>
		</fieldset>
		<p>
			<input class="submit" type="submit" value="Submit"/>
		</p>
	</fieldset>
</form>

<h3>Synthetic examples</h3>
<ul>
	<li><a href="<%= SkinPath %>errorcontainer-demo.html">Error message containers in action</a></li>
	<li><a href="<%= SkinPath %>custom-messages-data-demo.html">Custom Messages as Element Data</a></li>
	<li><a href="<%= SkinPath %>radio-checkbox-select-demo.html">Radio and checkbox buttons and selects</a></li>
	<li><a href="<%= SkinPath %>ajaxSubmit-integration-demo.html">Integration with Form Plugin (AJAX submit)</a></li>
	<li><a href="<%= SkinPath %>custom-methods-demo.html">Custom methods and message display.</a></li>
	<li><a href="<%= SkinPath %>dynamic-totals.html">Dynamic forms</a></li>
	<li><a href="<%= SkinPath %>themerollered.html">Forms styled with jQuery UI Themeroller</a></li>
	<li><a href="<%= SkinPath %>tinymce/">TinyMCE Demo</a></li>
	<li><a href="<%= SkinPath %>file_input.html">File inputs</a></li>
	<li><a href="<%= SkinPath %>jquerymobile.html">jQuery Mobile Form Validation</a></li>
</ul>
<h3>Real-world examples</h3>
<ul>
	<li><a href="<%= SkinPath %>milk/">Remember The Milk signup form</a></li>
	<li><a href="<%= SkinPath %>marketo/">Marketo signup form</a></li>
	<li><a href="<%= SkinPath %>multipart/">Buy and Sell a House multipart form</a></li>
	<li><a href="<%= SkinPath %>captcha/">Remote captcha validation</a></li>
</ul>

<h3>Testsuite</h3>
<ul>
	<li><a href="<%= SkinPath %>../test/">Validation Testsuite</a></li>
</ul>

</div>



