<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeedbackUC.ascx.cs" Inherits="StrimmTube.UC.FeedbackUC" %>
<script src="/../Plugins/popup/jquery.lightbox_me.js"></script>
<a onclick="ShowFeedback()" class="feedeback rotate">
 </a>
<div id="feedbackModal" style="display:none;">
    <div class="divPopupContainer">
          <a id="close_x" class="close close_x closeFeedback" href="#"></a>
        <div class="feedbackwrapper">
          
            <div class="feedbackValue">We value your opinion</div>
            <div class="feedbackPage">
                <span class="pageName">Page name  </span>
                <div class="feedbackh1"><%=pageName%></div>
            </div>                      
            
            <div class="typeFeedback">
            <h3>Select type of feedback</h3>
            <asp:DropDownList runat="server" ID="ddlFeedCat" ClientIDMode="Static">
                <asp:ListItem>please select</asp:ListItem>
                <asp:ListItem>general comments</asp:ListItem>
                <asp:ListItem>error/bug report</asp:ListItem>
                <asp:ListItem>recommendations</asp:ListItem>               
            </asp:DropDownList>
            </div>
           
            <div class="commentFeedback">
                <h3>Feedback</h3>
                <asp:TextBox ID="txtFeedback" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="10" Columns="35"></asp:TextBox>
            </div>
            <div class="submitFeedback">
                <asp:Label runat="server" ID="lblMsgFeedback" ClientIDMode="Static"></asp:Label>
                <a onclick="SendFeedback('<%=pageName%>')" class="btnSendFeedback">send</a>        
            </div>        
        </div>
    </div>
</div>