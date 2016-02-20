﻿<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE stylesheet [
  <!ENTITY space "<xsl:text> </xsl:text>">
  <!ENTITY cr "<xsl:text>
</xsl:text>">
]>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html"/>
	<xsl:param name="ControlID" />
	<xsl:param name="Options" />
    <xsl:param name="ManifestPath" />
	
	<xsl:template match="/*">
		<xsl:apply-templates select="root" />
	</xsl:template>
	
	<xsl:template match="root"> 
		<nav id="{$ControlID}" class="navigation">
			<a class="cs-click-menu"><i class="fa fa-bars"></i></a>
			<ul id="menus" class="dnnPEM-GlobalNav dnnClear">
				<xsl:apply-templates select="node">
				  <xsl:with-param name="nodeType"/>
				</xsl:apply-templates>
			</ul>
		</nav>
	</xsl:template>
	
	<xsl:template match="node">
		<xsl:param name="nodeType" />
		<li>
		  <xsl:variable name="nodeClass">
			<xsl:value-of select="$nodeType"/>
			menu-item menu-item-type-post_type menu-item-object-page			
			<xsl:if test="@first = 1">dnnPEM-NavFirst </xsl:if>        
			<xsl:if test="@last = 1">dnnPEM-NavLast </xsl:if>
			<xsl:if test="node">hasChild </xsl:if>
			<xsl:if test="@selected = 1">dnnPEM-NavOn </xsl:if>
		  </xsl:variable>
		  <xsl:attribute name="class">
			<xsl:value-of select="$nodeClass"/>
		  </xsl:attribute>
		  <xsl:choose>
			<xsl:when test="@enabled = 1">
			  <a href="{@url}">
				<span>
					<xsl:value-of select="@text" />																		
				</span>
			  </a>		  
			</xsl:when>
		  </xsl:choose>
		  <xsl:if test="node">
			<ul id="submenuitem" class="sub-menu">
				<xsl:apply-templates select="node">
				  <xsl:with-param name="nodeType"/>
				</xsl:apply-templates>
			</ul>
		  </xsl:if>
		</li>	
	</xsl:template>
</xsl:stylesheet>
