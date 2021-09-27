<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppSettingsForm.ascx.cs" Inherits="StrimmTube.UC.AppSettingsForm" %>
<h1 class="pageTitle">App Settings</h1>
<a class="close_x" href="#" onclick="CloseAppSettingsPopup()">close</a>
<div class="modalBody">
    <div class="createModalLeft">
        <div id="toggleAddChannelToApp" class="inputEmbeddedOFF" onclick="CreateChannel.ToggleAddChannelToApp()">
        </div>
        <span class="spnAdvancedMode">Add this channel to mobile app</span>
        <div class="subscribe_error" id="addToAppError"></div>
    </div>
</div>
