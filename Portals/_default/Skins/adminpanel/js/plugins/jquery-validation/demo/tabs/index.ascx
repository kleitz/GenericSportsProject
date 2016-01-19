<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>

	
<form id="signupform">
	
	<div id="tabs">
		<ul>
			<li><a href="<%= SkinPath %>#logindata">Login data</a></li>
			<li><a href="<%= SkinPath %>#personaldata">Personal data</a></li>
			<li><a href="<%= SkinPath %>#subscriptions">Subscriptions</a></li>
		</ul>
		<div id="logindata">
			<p>
				<label for="username">Username</label>
				<input id="username" name="username" class="required" minlength="3" maxlength="20" type="text" />
			</p>
			<p>
				<label for="email">Email address</label>
				<input id="email" name="email" class="required email" type="text" />
			</p>
			<p>
				<label for="password">Password</label>
				<input name="password" type="password" class="required" id="password" minlength="4" maxlength="50" />
			</p>
			<p>
				<label for="confirmpassword">Confirm Password</label>
				<input name="confirmpassword" type="password" class="required" equalTo="#password" id="confirmpassword" />
			</p>
		</div>
		<div id="personaldata">
			<p>
				<label for="street">Street</label>
				<input id="street" name="street" class="required" minlength="3" maxlength="50" type="text" />
			</p>
			<p>
				<label for="city">City</label>
				<input id="city" name="city" class="required" minlength="3" maxlength="50" type="text" />
			</p>
			<p id="birthdateGroup">
				<label for="birthdateDay">Birthdate</label>
				<select id="birthdateDay" name="birthdateDay" class="required">
					<option value="">Day</option>
					<option>1</option>
					<option>2</option>
					<option>3</option>
					<option>...</option>
				</select>
				<select id="birthdateMonth" name="birthdateMonth" class="required">
					<option value="">Month</option>
					<option>1</option>
					<option>2</option>
					<option>3</option>
					<option>4</option>
					<option>5</option>
					<option>6</option>
					<option>7</option>
					<option>8</option>
					<option>9</option>
					<option>10</option>
					<option>11</option>
					<option>12</option>
				</select>
				<select id="birthdateYear" name="birthdateYear" class="required">
					<option value="">Year</option>
					<option>1950</option>
					<option>1951</option>
					<option>1952</option>
					<option>1953</option>
					<option>1954</option>
					<option>1955</option>
					<option>...</option>
				</select>
			</p>
		</div>
		<div id="subscriptions">
			<p>
				<label for="weekly">Weekly Newsletter</label>
				<input id="weekly" name="weekly" type="checkbox" />
			</p>
			<p>
				<label for="updates">Product Updates</label>
				<input id="updates" name="updates" type="checkbox" />
			</p>
			<p>
				<label for="terms">Terms and conditions</label>
				<input id="terms" name="terms" class="required" type="checkbox" />
			</p>
		</div>
	</div>

	<input type="submit" />
</form>





