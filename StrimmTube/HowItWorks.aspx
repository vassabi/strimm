<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="HowItWorks.aspx.cs" Inherits="StrimmTube.HowItWorks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content=" Strimm is a Social Internet TV Network. It allows watching free video shows online continuously, create public TV streaming and socialize with friends" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <style>
        .stepiki a img {
            display: block;
            margin: auto;
            width: 93%;
            border-radius: 7px;
            padding: 10px;
            border: 1px solid #ccc;
            margin-bottom: 10px;
            background: #ddd;
        }

        .stepiki img {
            display: block;
            margin: auto;
            width: 100%;
            max-width: 320px;
            border-radius: 7px;
            border: 1px solid #ccc;
            margin-bottom: 10px;
        }

        .stepiki a:hover img {
            background: #2a99bd;
        }

        .stepiki h5 {
            width: 90%;
            margin: auto;
            text-align: center;
            font-size: 16px;
            
            margin-bottom: 10px;
        }

            .stepiki h5 strong {
                color: #2a99bd;
                font-size: 40px;
                font-weight: 900;
            }

                .stepiki h5 strong:hover {
                    color: #900;
                    font-size: 40px;
                }

        .stepiki a {
            text-decoration: none;
            color: #777;
            text-align: center;
            cursor:default;
        }



        #divContent .stepiki p {
            font-size: 18px;
            height: auto;
            text-align: center;
            color: #000;
        }

        .stepiki p strong {
            font-weight: normal;
            font-size: 19px;
            line-height: 1.4em;
            display: block;
            height: 60px;
            color: #333;
        }

        #homeBox p,
        #homeBox2 p,
        #homeBox3 p {
            height: 75px;
        }

        .centered {
            text-align: center;
        }

        .titleH1 {
            text-align: center;
            color: #2a99bd;
            padding: 10px;
            font-size: 37px;
            font-family: Verdana;
            letter-spacing: 2px;
            margin-right: 50px;
            font-weight: normal;
            margin-top: 20px;
            margin-bottom: 15px;
        }

        #divContent .stepiki .detailsSteps {
            display: block;
            width: 95%;
            height: 150px;
        }

        .how {
            max-width: 1280px;
        }

        #divContent .stepiki .detailsSteps:hover p {
            color: #000;
        }

        #divContent .stepiki .detailsSteps p {
            text-align: left;
            color: #777;
            font-size: 14px;
            line-height: 1.2em;
        }

        .detailsSteps ol {
            list-style: decimal;
            text-align: left;
            color: #777;
            font-size: 14px;
            line-height: 1.3em;
            padding-left: 50px;
            padding-right: 5px;
            margin-bottom: 10px;
            height: 100px;
        }

            .detailsSteps ol li {
                margin-bottom: 7px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".spnDetails.personal").click(function () {
                if ($(".details.personal").is(":visible")) {
                    $(".details.personal").hide();
                }
                else {
                    $(".details.personal").show();
                }
            });
            $(".spnDetails.network").click(function () {
                if ($(".details.network").is(":visible")) {
                    $(".details.network").hide();
                }
                else {
                    $(".details.network").show();
                }
            });
        });
    </script>
    <div id="divBoardContent">
        <div id="divTitleUrlHolder">
            <h1 class="pageTitle">how it works</h1>
        </div>


        <div id="divContentHow">
            <div class="container block how">

                <h5 class="titleH1">It works in 3 simple steps</h5>
                <div class="one-third column first stepiki" id="homeBox">
                    <a>
                        <h5>
                            <img src="/images/step1.png" /><strong>1</strong></h5>
                    </a>
                    <p class="centered">
                        <strong>Create your own 
                                              Social and TV Network</strong>
                    </p>
                    <div class="detailsSteps">
                        <ol>
                            <li>Create your social board, upload images, posts and follow others.</li>
                            <li>Create your own channels in different TV categories (Entertainment, Sport, Music, Mixed, etc.).</li>
                        </ol>
                        <p>Tip: You can create up to 10 TV channels in different or same categories.  </p>
                    </div>

                    <%--<a class="homeBoxReadMoreBtn" href="HowItWorks.aspx">details &#8250;&#8250;</a>--%>
                </div>
                <div class="one-third column stepiki" id="homeBox2">
                    <a>
                        <h5>
                            <img src="/images/step2.png" />
                            <strong>2</strong></h5>

                        <p>
                            <strong>Link the best public videos from YouTube library to your Video Room</strong>
                        </p>
                    </a>
                    <div class="detailsSteps">
                        <ol>
                            <li>Search for videos you enjoy, either on YouTube or directly on the Strimm platform.   </li>
                            <li>Organize the imported videos into categories and link them to your Video Room.  </li>
                        </ol>
                        <p>Tip: Use longer videos to make filling the daily schedule easier.   </p>
                    </div>
                    <%--<a class="homeBoxReadMoreBtn" href="FeaturedChannels.aspx">details &#8250;&#8250;</a>--%>
                </div>
                <div class="one-third column stepiki " id="homeBox3">
                    <a>
                        <h5>
                            <img src="/images/step3.png" />
                            <strong>3</strong></h5>
                    </a>
                    <p>
                        <strong>Schedule 24/7 continuous  Broadcast of the chosen videos and then go ON AIR!</strong>
                    </p>
                    <div class="detailsSteps">
                        <ol>
                            <li>Create a schedule from the chosen videos on the day you want.  </li>
                            <li>That’s it! Soon you will be broadcasting to the world. </li>
                        </ol>
                        <p>Tip: Share your channel with as many friends as you can. </p>
                    </div>
                    <%-- <a class="homeBoxReadMoreBtn" href="About.aspx">details &#8250;&#8250;</a>--%>
                </div>
            </div>
        </div>
        <uc:FeedBack runat="server" ID="feedBack" pageName="how it works" />
    </div>
</asp:Content>
