<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewSwitcher.ascx.cs" Inherits="WISETRACK.ViewSwitcher" %>



<div id="viewSwitcher">

    Vista <%: CurrentView %> | <a style="font-size:large" href="<%: SwitchUrl %>" data-ajax="false">Cambiar a <%: AlternateView  %></a>
</div>