<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>

    <div id="toolbar" class="toolbar">
        <a href="<%= SkinPath %>#" onclick="parent.console.clear()">Clear</a>
        <span class="toolbarRight">
            <a href="<%= SkinPath %>#" onclick="parent.console.close()">Close</a>
        </span>
    </div>
    <div id="log"></div>
    <input type="text" id="commandLine">
    
    <script>parent.onFirebugReady(document);</script>

