<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="FavoriteChannels.aspx.cs" Inherits="StrimmTube.FavoriteChannels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
Favorite Channels | Strimm Public
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
 <meta name="description" content="Strimm TV | Favorite channels"/>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <%: System.Web.Optimization.Scripts.Render("~/bundles/favoritechannel/js") %>
    <%--<script src="/JS/Controls.js"></script>
    <script src="/JS/FavoriteChannels.js"></script>
    <script src="/Plugins/popup/jquery.lightbox_me.js"></script>
     <link href="/Plugins/Scroller/scroller.css" rel="stylesheet" />
    <script src="/JS/jquery.cookie.js"></script>--%>
    <style type="text/css">
        .divChannel {
            background-color: #ECECEC;
            border: 1px solid #2A99BD;
            border-radius: 5px 5px 5px 5px;
            float: left;
            margin: 0.9%;
            min-width: 160px;
            max-width: 160px;
            padding: 0.5%;
        }

        .chanBox {
    float: left;
}

            .divChannel a {
                color: #2A99BD;
                display: block;
                
                font-size: 15px;
                font-weight: normal;
                overflow: hidden;
                text-decoration: none;
                text-overflow: ellipsis;
                text-transform: capitalize;
                white-space: nowrap;
            }


                .divChannel a.aTitle {
                    height: 20px;
                }


                .divChannel a.comingSoon {
                    color: #95ccdd;
                }

            .divChannel span {
                clear: both;
                color: #777;
                display: block;
                float: left;
                
                font-size: 12px;
                line-height: 15px;
            }

                .divChannel span.comingSoon {
                    color: #bbb;
                }

                .divChannel span em {
                    color: #2A99BD;
                    font-style: normal;
                }

            .divChannel img {
                border: 1px solid #f9f9f9;
                display: block;
                float: left;
                height: 122px;
                margin-bottom: 10px;
                margin-top: 10px;
                width: 100%;
                border-radius: 3px;
            }


        #lblMessage {
            color: #555555;
            display: block;
            line-height: 22px;
            margin: 100px auto;
            text-align: center;
            width: 80%;
            font-size: 20px;
        }

        .channelTiming {
    background-color: red;
    border-top-left-radius: 15px;
    bottom: 0;
    color: #fff;
    display: block;
    float: right;
    font-size: 14px;
    height: 20px;
    padding-left: 10px;
    padding-right: 5px;
    position: absolute;
    right: 0;
    text-align: left;
    line-height: 20px;
}

        .chanBoxtitle {
    color: #eee;
    float: left;
    font-size: 14px;
    font-weight: normal;
    padding-left: 5%;
    padding-top: 45%;
    width: 100%;
    text-align: left;
}

        .chanBoxsub, .chanBoxviews {
            color: #000;
            float: left;
            font-size: 12px;
            line-height: 15px;
            padding-left: 3px;
            width: 50%;
            text-align: left;
        }

.VideoBoxRemoveFav {
    background-image: url("/images/trash.png");
    background-position: 5px 3px;
    background-repeat: no-repeat;
    background-size: 80%;
    cursor: pointer;
    display: block;
    height: 27px;
    position: absolute;
    right: 1px;
    top: 5px;
    width: 27px;
    opacity: 1;
}
.chanBox .chanBoxPlay, .chanBox01 .chanBoxPlay, .chanBox02 .chanBoxPlay, .chanBox03 .chanBoxPlay, .chanBox04 .chanBoxPlay, .chanBox05 .chanBoxPlay, .chanBox06 .chanBoxPlay, .chanBox07 .chanBoxPlay {
    background-size: 27px;
    width: 27px;
}

        .ancRemove {
            display: block;
            width: 100%;
            height: 100%;
        }

        .VideoBoxRemoveFav:hover {
            opacity: 1;
            background-image: url("/images/trashHover.png");
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <script src="/Plugins/Scroller/nanoscroller.min.js"></script>--%>
    <script type="text/javascript">
        var userId = "<%=userId%>";
        $(document).ready(function () {
            GetFavoriteChannels();
            $("#sortFavorite option:first").attr('selected', 'selected');


        });



    </script>
    <%--   <div id="divTitleUrlHolder">
         <h1>favorite channels</h1>
       </div>--%>


    <div class="mainContentWrapper">
        <div id="content">
            <div class="pageInfoHolder">
                <h1>my favorite channels</h1>
                <span id="lblChannelCount"></span>
                <select id="sortFavorite" onchange="SortFavoriteChannels(this)">
                    <option value="1">recently added</option>
                    <option value="2">currently playing</option>
                    <option value="3">a-z</option>
                    <option value="4">z-a</option>
                </select>
            </div>
            <asp:Label ID="lblMessage" ClientIDMode="Static" runat="server" Text="You have no favorite channels yet" Visible="false"></asp:Label>

            <div class="left">
                <%--  <div class="nano" style="height: 500px;">--%>
                <%-- <div class="content">--%>
                <div id="channelsHolder" class="channelsHolder">
                    <%-- </div>--%>
                </div>
                <%--  </div>--%>
            </div>

            <%--    <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSortChannels" EventName="SelectedIndexChanged" />
               
            </Triggers>--%>


            <uc:FeedBack runat="server" ID="feedBack" pageName="favorite channel" />
        </div>
    </div>
    <div id="modalRemove">
        <span>Would you like to remove this channel from your Favorites? </span>
        <div class="confurmButtons">
            <a id="cancelRemove" onclick="CloseModalRemove()">cancel</a>
            <a id="removeOk">ok</a>
        </div>
        <div class="doNotShow">
            <input type="checkbox" id="msgRemove" />
            <label for="checkBox">Do not show this message again</label>
        </div>
    </div>
</asp:Content>
