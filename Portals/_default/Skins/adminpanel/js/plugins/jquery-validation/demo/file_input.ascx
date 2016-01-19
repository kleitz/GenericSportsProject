<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>


<form class="cmxform" id="fileForm" method="post" action="">
	<fieldset>
		<legend>Select the indicated type of files?</legend>
		<p>
			<label for="file1">Select a plain text file (e.g. *.txt)</label>
			<input type="file" id="file1" name="file1" class="required" accept="text/plain" />
		</p>
		<p>
			<label for="file2">Select any image file</label>
			<input type="file" id="file2" name="file2" class="required" accept="image/*"/>
		</p>
		<p>
			<label for="file3">Select either a PDF or a EPS file</label>
			<input type="file" id="file3" name="file3" class="required" accept="image/x-eps,application/pdf"/>
		</p>
		<p>
			<label for="file4">Select any audio or image file</label>
			<input type="file" id="file4" name="file4" class="required" accept="image/*,audio/*"/>
		</p>
		<p>
			<label for="file5">Select one or more plain text files (e.g. *.txt)</label>
			<input type="file" id="file5" name="file5" class="required" multiple accept="text/plain" />
		</p>
		<p>
			<input class="submit" type="submit" value="Submit"/>
		</p>
	</fieldset>
</form>


