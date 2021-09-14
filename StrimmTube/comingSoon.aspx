<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="comingSoon.aspx.cs" Inherits="StrimmTube.comingSoon" %>

<!DOCTYPE html>

<html >
<head runat="server">
    <title>Strimm – Free Online Video Platform to Create Your Own TV Network</title>
</head>
<body>
    <style>
        @font-face {
    font-family: OpenSans-Light;
    src: url('/Fonts/OpenSans-Light.ttf');
}

        @font-face {
    font-family: OpenSans-Regular;
    src: url('/Fonts/OpenSans-Regular.ttf');
}
        body {
  background-image: url("/images/formsBG-web.jpg");
  background-position: 50% 50%;
  background-repeat: no-repeat;
  background-size: cover;
}

        .wrapperCS{
    display: block;
    width: 100%;
    margin: auto;
}
    .topCS{
background-color: #fff;
display: block;
height: 80px;
left: 0;
margin: auto;
opacity: 0.7;
position: absolute;
top: 0;
width: 100%;
    }

        .topCS img {
display: block;
float: left;
height: 60px;
margin-left: 75px;
margin-top: 7px;
        }
    .contentCS{
display: block;
margin-bottom: auto;
margin-right: auto;
margin-top: 300px;
min-height: 600px;

    }
        .contentCS ul {
display: block;
float: left;
height: 60px;
width: 50%;
 }

            .contentCS ul li {
background-color: #fff;
box-shadow: 1px 2px 10px #fff;
color: #777;
display: block;
font-family: OpenSans-Regular, Arial,sans-serif;
font-size: 27px;
height: 30px;
line-height: 30px;
margin: auto;
max-width: 460px;
min-width: 320px;
opacity: 0.85;
padding: 10px 10px 10px 20px;
text-align: left;
text-transform: capitalize;
width: 70%;
             
            }

        .soon {
background: #2a99bd none repeat scroll 0 0;
color: #fff;
cursor: default;
float: right;
font-size: 20px;
height: 40px;
line-height: 50px;
margin-right: -10px;
margin-top: -10px;
padding-bottom: 10px;
padding-left: 10px;
padding-right: 10px;
text-align: center;
text-transform: lowercase;
width: 80px;
        }
   

    </style>


    <div class="wrapperCS">
    <div class="topCS">
      <img src="/images/LogoPureMain.png" />
    </div>
    <div class="contentCS">
        <ul class="comingsoonHolder">
            <li class="comingsoon">coming soon... <span class="soon">soon</span></li>
            
        </ul>
    </div>

        </div>

    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
