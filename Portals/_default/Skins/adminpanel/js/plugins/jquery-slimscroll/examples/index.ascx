<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html>
    <head>
    </head>
    <frameset framespacing="0" cols="250,*" frameborder="0" noresize>
        <frame name="nav" src="<%= SkinPath %>navigation.html" target="top">
        <frame name="main" src="<%= SkinPath %>height-width.html" target="main">
    </frameset>
</html>
