<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FollowBoxUC.ascx.cs" Inherits="StrimmTube.UC.FollowBoxUC" %>
<div class="divFollowContent" id="<%=userName%>">
    <a class="lnkToboard" href="board/<%=userBoardUrl%>">
                <img class="imgUserAvatar" src="<%=avatarUrl%>" />
       

    </a>
     <a class="lnkToboard" href="board/<%=userBoardUrl%>">
               
        <span class="spnUserName"><%=userName%></span>

    </a>
    <div class="actionsHolder" runat="server" id="divActions" visible="false">
        <div class="divUnsubscribe" id="divUnsubscribe" runat="server">
            <a onclick="Unfollow(<%=userName%>)" class="lnkUnfollow">unfollow</a>
        </div>

    </div>
</div>
