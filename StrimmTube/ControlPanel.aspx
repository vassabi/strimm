<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="ControlPanel.aspx.cs" Inherits="StrimmTube.ControlPanel" %>

<asp:Content ID="Content3" ContentPlaceHolderID="titleHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="metaHolder" runat="server">
<meta name="description" content=" Strimm is a Social Internet TV Network. It allows watching free video shows online continuously, create public TV streaming and socialize with friends" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/panel.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        //DO NOT DELETE FUNCTIONS FROM THE PAGE !
        function ShowMessage()
        {
            alert("Please complete your personal profile first");
        }
    </script>
              <div id="divTitleUrlHolder">
            <h1 class="pageTitle">control panel</h1>
          
        </div>
    <div id="divCPContent">
     
        
      <div id="divMetroMenu">
              <a href="browse-channel?id=101">
                <div class="divMetroItem watchNow wide">
                    <div class="img"></div>
                    <span class="metroItemLink">watch now</span>
                   <span class="itemDescribtion">Grab your favorite snack<br />and enjoy the show!</span>
                  </div>
            </a>

              <div class="divMetroItem grey10"></div>

                 <a href="favorite-channels">
                <div class="divMetroItem favorite">
                    <div class="img"></div>
                    <span class="metroItemLink">favorite channels</span></div>
            </a>
             <div class="divMetroItem grey10"></div>
                                <a  runat="server" id="ancBoard">
                <div class="divMetroItem board wide">
                    <div class="img"></div>
                    <span class="metroItemLink">my board</span>
                    <span class="itemDescribtion">What is in your mind today?</span>
                </div>
            </a>

                      <a href="profile">
                <div class="divMetroItem profile ">
                    <div class="img"></div>
                    <span class="metroItemLink">my profile</span></div>
            </a>




          <div class="divMetroItem grey10"></div>

                                <a href="watch-it-later">
                <div class="divMetroItem archive">
                    <div class="img"></div>
                    <span class="metroItemLink">watch later</span></div>
            </a>


                    <a runat="server" id="ancNetwork">
                <div class="divMetroItem network wide ">
                    <div class="img"></div>
                    <div class="itemTextHolder">
                    <span class="metroItemLink">create & manage channel</span>
                    <span class="itemDescribtion">Become a Producer! </span>
                        </div>
                </div>
            </a>
          
         <div class="divMetroItem grey10"></div>


                               <a href="followers?id=<%=userId%>">
                <div class="divMetroItem follow">
                    <div class="img"></div>
                    <span class="metroItemLink">following</span></div>
            </a>

           <div class="divMetroItem grey10"></div>


                      <a href="guides">
                <div class="divMetroItem howTo wide ">
                    <div class="img"></div>
                    <span class="metroItemLink">how to</span>
                    <span class="itemDescribtion">Get knowledge, tips and advices of how to <br />become a professional Strimmer! </span>
                </div>
            </a>
 


            


       
  
           <div class="divMetroItem grey5"></div>
     
      


             <div class="divMetroItem grey10"></div>
          


      
           
          
          
         <%-- <div class="divMetroItem grey10 wide"></div>--%>

            <a href="monetize">
                <div class="divMetroItem monetize">
                    <div class="img"></div>
                    <span class="metroItemLink">monetize</span></div>
            </a>
       

          
<div class="spacer"></div>
      </div>
          <uc:FeedBack runat="server" ID="feedBack" pageName="control panel" />
    </div>
</asp:Content>