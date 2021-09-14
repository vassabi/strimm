<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewStartTimeUC.ascx.cs" Inherits="StrimmTube.UC.NewStartTimeUC" %>
<a class="btnNewStartTime" onclick="ShowNewTime()">Save & make new start time</a>
<div class="newTime" style="display:none;">
    <div class="divTime" id="divNewTime">
        <input class="txtTime" type="text" placeholder="hh:mm" runat="server" id="txtNewTime" />
        <input type="hidden" runat="server" id="hiddenNewHourValue" clientidmode="Static" />
    </div>
    <div class="divBtnGo" id="divBtnGo">
        <input type="button" class="btnNewGo" id="btnNewGo" value="GO" />
    </div>
    <div class="divBtnNewGoUpdate" runat="server" id="divBtnNewGoUpdate" clientidmode="Static">
        <input type="button" class="btnNewGoUpdate" value="GO" />
    </div>
</div>
