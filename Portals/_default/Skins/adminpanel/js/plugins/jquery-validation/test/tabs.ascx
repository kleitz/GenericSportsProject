<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>


	<form class="cmxform" id="commentForm" method="get" action="">

		<div id="example" class="flora">
	        <ul>

	            <li><a href="<%= SkinPath %>#fragment-1"><span>One</span></a></li>
	            <li><a href="<%= SkinPath %>#fragment-2"><span>Two</span></a></li>
	            <li><a href="<%= SkinPath %>#fragment-3"><span>Three</span></a></li>
	        </ul>
	        <div id="fragment-1">
					<fieldset>
						<legend>A simple comment form with submit validation and default messages</legend>
						<p>
							<label for="cname">Name (required, at least 2 characters)</label>
							<input id="cname" name="name" class="some other styles {required:true,minLength:2}" />
						<p>
							<label for="cemail">E-Mail (required)</label>
							<input id="cemail" name="email" class="{required:true,email:true}" />
						</p>
						<p>
							<label for="curl">URL (optional)</label>
							<input id="curl" name="url" class="{url:true}" value="" />
						</p>
						<p>
							<label for="ccomment">Your comment (required)</label>
							<textarea id="ccomment" name="comment" class="{required:true}"></textarea>
						</p>
					</fieldset>

	        </div>
	        <div id="fragment-2">
	            Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
	            Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
	        </div>
	        <div id="fragment-3">
	            Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
	            Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
	            Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
	        </div>
	    </div>
		<p>
			<input class="submit" type="submit" value="Submit"/>
		</p>

	</form>


