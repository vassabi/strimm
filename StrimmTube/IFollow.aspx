<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="IFollow.aspx.cs" Inherits="StrimmTube.IFollow" %>


<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content=" Strimm is a Social Internet TV Network. It allows watching free video shows online continuously, create public TV streaming and socialize with friends" />
    <meta name="keywords" content=" internet tv, social tv, social network, watch videos, tv network, videos online, online videos, schedule videos, schedule broadcast, create tv, playlist builder, free videos, watch videos online, tv streaming" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <link href="Plugins/Scroller/scroller.css" rel="stylesheet" />

    <style type="text/css">
        .pageTitle {
            color: #F9F9F9;
            display: block;
            float: left;
            
            font-size: 17px;
            font-weight: normal;
            height: 36px;
            letter-spacing: 2px;
            line-height: 36px;
            margin-left: 18%;
        }

        #content {
            width: 80%;
            margin: 50px auto;
        }

        .divFollowContent {
            border: 1px solid #2A99BD;
            border-radius: 4px;
            float: left;
            height: 100px;
            margin-bottom: 10px;
            margin-right: 10px;
            padding: 5px;
            width: 300px;
        }

        .imgUserAvatar {
            display: block;
            float: left;
            height: 100px;
            width: 100px;
        }

        .lnkUnfollow {
            background-color: #2A99BD;
            background-image: url("/images/ButtonBG.jpg");
            background-position: -10px center;
            background-repeat: repeat-x;
            border: medium none;
            border-radius: 3px;
            color: #FFFFFF;
            cursor: pointer;
            display: block;
            float: left;
            
            font-size: 14px;
            margin-left: 10px;
            padding: 5px;
            width: 80px;
            text-align: center;
        }

            .lnkUnfollow:hover {
                background-image: url("/images/ButtonBGhover.jpg");
            }

        .spnUserName {
            display: block;
            float: left;
            color: #2A99BD;
            margin-left: 10px;
            width: 110px;
        }

        .lnkToboard {
            display: block;
            float: left;
        }

        .actionsHolder {
            display: block;
            float: left;
            margin-top: 60px;
        }

        #lblMsg {
            color: #555555;
            display: block;
            font-size: 20px;
            line-height: 22px;
            margin: 100px auto;
            text-align: center;
            width: 80%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Plugins/Scroller/nanoscroller.min.js"></script>
    <script type="text/javascript">
        var webMethodUnFollowUser = "WebServices/UserService.asmx/UnFollowUser";


        $(document).ready(function () {
            $(".nano").nanoScroller({ alwaysVisible: true });
        });
        function ScrollerUp() {
            $(".nano").nanoScroller({ alwaysVisible: true });
        }
        function Unfollow(userUrl) {
            //console.log(userUrl);
            $.ajax({
                type: "POST",
                url: webMethodUnFollowUser,
                data: '{"userUrl":' + "'" + userUrl.id + "'" + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    $("#" + userUrl.id).css("display", "none");
                    ScrollerUp();
                    alertify.success("You have successfully unfollowed user.");
                }
            });
        }

    </script>
    <div id="divTitleUrlHolder">
        <h1 class="pageTitle">following</h1>
    </div>
    <div class="pageComent">
        <p>This page displays everyone you follow. </br>Click “unfollow” if you choose to stop following that person.  </p>
    </div>
    <asp:ScriptManager runat="server" ID="scriptManager"></asp:ScriptManager>
    <div id="content">
        <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Always" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div class="inputHolder" id="inputHolder" runat="server">



                    <asp:DropDownList ID="ddlSortFollowers" runat="server" CssClass="ddlSortChannels" AutoPostBack="true" OnSelectedIndexChanged="ddlSortFollowers_SelectedIndexChanged" onChange="ScrollerUp()">
                        <asp:ListItem Value="0">Sort By</asp:ListItem>
                        <asp:ListItem Value="1">username A-Z</asp:ListItem>
                        <asp:ListItem Value="2">date of follow</asp:ListItem>


                    </asp:DropDownList>
                </div>
                <div class="left">
                    <div class="nano" style="height: 500px;">
                        <div class="content">
                            <div runat="server" id="followerHolder" class="followerHolder">
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSortFollowers" EventName="SelectedIndexChanged" />

            </Triggers>
        </asp:UpdatePanel>

        <uc:FeedBack runat="server" ID="feedBack" pageName="followings" />

    </div>

</asp:Content>
