<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityBox.ascx.cs" Inherits="StrimmTube.UC.ActivityBox" %>
  <div class="activity TestimonialBox item block">
              <a href="<%=userName%>" class="spnuserName"><%=userName%></a> 
       <span class="spnTimeStamp block"><%=postTime%></span>    
      <div class="imgSpanWrapper">
                    <div class="imgActivity" runat="server" id="divImg">
                        <img src="<%=postImage%>" />
                    </div>
                    <div class="activityPost">
                        <span><%=post%></span>
                        
                     </div>   
                    </div>
      </div>
