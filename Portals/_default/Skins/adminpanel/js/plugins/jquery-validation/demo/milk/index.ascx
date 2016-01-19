<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>


<h1 id="banner"><a href="http://bassistance.de/jquery-plugins/jquery-plugin-validation/">jQuery Validation Plugin</a> Demo</h1>
<div id="main">

<div id="content">

<div id="header">
  <div id="headerlogo"><img src="<%= SkinPath %>milk.png" alt="Remember The Milk" /></div>
</div>
<div style="clear: both;"><div></div></div>


<div class="content">
    <div id="signupbox">
       <div id="signuptab">
        <ul>
          <li id="signupcurrent"><a href="<%= SkinPath %> ">Signup</a></li>
        </ul>
      </div>
      <div id="signupwrap">
      		<form id="signupform" autocomplete="off" method="get" action="">
	  		  <table>
	  		  <tr>
	  		  	<td class="label"><label id="lfirstname" for="firstname">First Name</label></td>
	  		  	<td class="field"><input id="firstname" name="firstname" type="text" value="" maxlength="100" /></td>
	  		  	<td class="status"></td>
	  		  </tr>
	  		  <tr>
	  			<td class="label"><label id="llastname" for="lastname">Last Name</label></td>
	  			<td class="field"><input id="lastname" name="lastname" type="text" value="" maxlength="100" /></td>
	  			<td class="status"></td>
	  		  </tr>
	  		  <tr>
	  			<td class="label"><label id="lusername" for="username">Username</label></td>
	  			<td class="field"><input id="username" name="username" type="text" value="" maxlength="50" /></td>
	  			<td class="status"></td>
	  		  </tr>
	  		  <tr>
	  			<td class="label"><label id="lpassword" for="password">Password</label></td>
	  			<td class="field"><input id="password" name="password" type="password" maxlength="50" value="" /></td>
	  			<td class="status"></td>
	  		  </tr>
	  		  <tr>
	  			<td class="label"><label id="lpassword_confirm" for="password_confirm">Confirm Password</label></td>
	  			<td class="field"><input id="password_confirm" name="password_confirm" type="password" maxlength="50" value="" /></td>
	  			<td class="status"></td>
	  		  </tr>
	  		  <tr>
	  			<td class="label"><label id="lemail" for="email">Email Address</label></td>
	  			<td class="field"><input id="email" name="email" type="text" value="" maxlength="150" /></td>
	  			<td class="status"></td>
	  		  </tr>
              	  		  <tr>
	  			<td class="label"><label>Which Looks Right</label></td>
	  			<td class="field" colspan="2" style="vertical-align: top; padding-top: 2px;">
	  			<table>
	  			<tbody>

	  			<tr>
	  				<td style="padding-right: 5px;">
		  				<input id="dateformat_eu" name="dateformat" type="radio" value="0" />
			            <label id="ldateformat_eu" for="dateformat_eu">14/02/07</label>
	  				</td>
	  				<td style="padding-left: 5px;">
		  				<input id="dateformat_am" name="dateformat" type="radio" value="1"  />
			            <label id="ldateformat_am" for="dateformat_am">02/14/07</label>
	  				</td>
	  				<td>
	  				</td>
	  			</tr>
	  			</tbody>
	  			</table>
	  			</td>
	  		  </tr>

	  		  <tr>
	  			<td class="label">&nbsp;</td>
	  			<td class="field" colspan="2">
		  			<div id="termswrap">
			  			<input id="terms" type="checkbox" name="terms" />
			            <label id="lterms" for="terms">I have read and accept the Terms of Use.</label>
		            </div> <!-- /termswrap -->
	  			</td>
	  		  </tr>
	  		  <tr>
	  			<td class="label"><label id="lsignupsubmit" for="signupsubmit">Signup</label></td>
	  			<td class="field" colspan="2">
	            <input id="signupsubmit" name="signup" type="submit" value="Signup" />
	  			</td>
	  		  </tr>

	  		  </table>
          </form>
      </div>
    </div>
</div>

</div>

</div>


