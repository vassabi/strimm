<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateChannelUC.ascx.cs" Inherits="StrimmTube.UC.CreateChannelUC"  %>

 <script src="/../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="/../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="/../Scripts/jquery.ddslick.js" type="text/javascript"></script>   
   
    <script src="/../jquery/Jcrop/js/jquery.Jcrop.min.js"></script>
    <script src="/../Plugins/popup/jquery.lightbox_me.js"></script>
   
    <script type="text/javascript">
        var channelId = "<%=channelTubeId%>";
        var channelCategory = "<%=channelCategory%>";
        var selectIndexCategory = "<%=selectIndexCategory%>";
        var domainName = '<%=domainName%>';

        var webMethodGetChannelCategoriesJson = "/WebServices/ChannelWebService.asmx/GetChannelCategories";
        var webMethodCheckChannelName = "/WebServices/ChannelWebService.asmx/CheckChannelName";
        var webMethodDeleteChannel = "/WebServices/ChannelWebService.asmx/DeleteChannel"
        var channelName = '<%=channelname%>';
        var userName = '<%=UserName%>';
        $(document).ready(function () {
            $('#txtDescription').keypress(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                }

            });

            if (channelId == "0") {
                $("#btnDeleteChannel").css("visibility", "hidden");
            }
            $("#divMenuHolder a.createChannel").addClass("active");
           
           
            //limits counter
            function limits(obj, limit, lbl) {
                var text = $(obj).val();
                var length = text.length;
                if (length > limit) {
                    $(obj).val(text.substr(0, limit));
                } else { // alert the user of the remaining char.
                    $(lbl).text(limit - length + " characters remaining");
                }
            }
            //get message on page if channel name exist

            //get url on page

            if ($("#txtChannelName").length > 0) {
                var channelName = $("#txtChannelName").val().replace(" ", "");
            }
            $("#txtChannelName").focusout(function () {
                $("#lblChannelUrl").text("www.strimm.com/" + userName + "/" + $("#txtChannelName").val().replace(" ", ""));
            });
            $('#txtChannelName').focus(function () { $("#spnChannelNameCount").text(""); }).keyup(function () {
                limits($(this), 50, $("#spnChannelNameCount"));
            }).focusout(function () {
                $("#spnChannelNameCount").text("");
            });
            $('#txtDescription').focus(function () { $("#spanDescCount").text(""); }).keyup(function () {
                limits($(this), 150, $("#spanDescCount"));
            }).focusout(function () { $("#spanDescCount").text(""); });
        });
        
        {

        }
        // add image to img on select
        function readURL(input) {
            if (input.files && input.files[0]) {

                var reader = new FileReader();
                reader.onload = function (e) {
                    $("#imgChannelAvatar")
                    .attr('src', e.target.result)
                    .width(50)
                    .height(50);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
        function storeCoords(c) {
            jQuery('#X').val(c.x);
            jQuery('#Y').val(c.y);
            jQuery('#W').val(c.w);
            jQuery('#H').val(c.h);
        };
        function ReloadPage() {
            $("#txtChannelName, #txtDescription, #fuChannelAvatar").val("");
            location.reload();
        }
        function DeleteChannel() {
            var r = confirm("Warning: The channel and all schedules will be deleted");
            return r;
          
        }
        var api;
        var cropWidth = 200;
        var cropHeight = 200;
        function ShowCropModal() {
            var imageSrc = $("#imgChannelAvatar").attr("src");
            $("#cropbox").removeAttr("src").attr("src", imageSrc);
            $('#cropModal').lightbox_me({
                centered: true,
                onLoad: function () {
                    var jcrop_api;
                    var i, ac;

                    initJcrop();




                }
            });
        }
        function initJcrop() {
            jcrop_api = $.Jcrop('#cropbox', {
                onSelect: storeCoords,
                boxWidth: 400,
                boxHeight: 300
            });
            jcrop_api.setOptions({ aspectRatio: 1 / 1 });
            jcrop_api.setOptions({
                minSize: [200, 200],
                maxSize: [200, 200]
            });
            jcrop_api.setSelect([200, 200, 200, 200]);
        };
        function SizeAlertUp() {
            ShowMessage("The image type must be either .jpeg or .png and should not exceed 300kb in size");
        };
        function triggerNewChannel() {
            $("#btnCreateNew").trigger("click");
        };
    </script>




<style> 
     
.blueNav .ChannelManagement {
 color: #fff;
}
    .divMainButtonHolder ul li a.createChannel {
      background-color: #2a99bd;

    }

       .divMainButtonHolder ul li .step1 .step {
              color: #fc0;  
        }
             .divMainButtonHolder ul li .step1 .stepDescription {
              color: #ddd;  
        }

#divCreateChannelContent {
  background-color: #fff;
  margin: auto;
  min-height: 436px;
  width: 500px;
}
 </style>


<%--    <ol class="breadcrumb">
<li ><a href="home">home</a></li>
        <li ><a href="create-channel">channel management</a></li>
        <li class="active">channel info</li>
    </ol>--%>



<%--    <h5 class="titleH1"> channel managment</h5>--%>
<div class="Scallop"></div>


    <div runat="server" id="divBackendmenuHolder">
       
    </div>

    <div id="divCreateChannelContent">

       
        <ul>
            <li>
                <div id="divSubmitUp">

                </div>
            </li>
            <li><span class="spnTitleUpdate">channel name</span>
                
                <div runat="server" id="divLoadLabel">
                    <asp:TextBox runat="server" ID="txtChannelName" ClientIDMode="Static" placeholder="3-25 characters, letters and/or digits only" MaxLength="25"></asp:TextBox>
                    <asp:Label ID="lblChannelName" runat="server" class="lblChannelName" ClientIDMode="Static"></asp:Label>
                    <div class="divError" id="erChannelName" style="float:right;"><span class="spnError">channel name is required</span></div>
                </div>
                
            </li>
            <li><span class="spnTitle">channel category</span>
                <input type="hidden"  id="category" runat="server" clientidmode="Static" />
<%--                <div class="divChannelCategoryDDl" style="height: 30px;"></div>--%>
                <asp:DropDownList ID="ddlChannelCategory"  ClientIDMode="Static" OnSelectedIndexChanged="SelectedCategoryChanged" runat="server"></asp:DropDownList>
            </li>
           <%-- <li><span class="spnTitle spnDesc">channel description</span>
                
               
                <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Rows="9" Columns="75" ClientIDMode="Static" MaxLength="150" placeholder="150 characters max"></asp:TextBox>
             
            </li>--%>
            <li>
                <div class="picLogoHolder">
                <span class="spnTitle picLogo">channel picture/Logo</span>
                <span class="spnPorametr">(300k max, .jpg or .png files only)</span>
                    </div>
                <div class="fuimgHolder">
                <asp:Image runat="server" ID="imgChannelAvatar" ClientIDMode="Static"  ImageUrl="/images/comingSoonBG.jpg"/>
               <div id="fuHolder"> 
                   <span>browse</span>
                <asp:FileUpload runat="server" ID="fuChannelAvatar" ClientIDMode="Static" onchange="readURL(this)" />
                   </div>
                <a onclick="ShowCropModal()" class="cropImg">crop image</a>
                    </div>
                <%--<input runat="server" onchange="readURL(this)" type="file" id="fuChannelAvatar" clientidmode="static" />--%>
            </li>
            <li><span class="spnTitle">channel URL</span>
                <asp:Label runat="server" ID="lblChannelUrl" ClientIDMode="Static"></asp:Label></li>
            <li>
                <div id="divTermOfUse" style="display:none;">
                    <asp:CheckBox runat="server" ID="chkBoxterms" Checked="true" ClientIDMode="Static" /><span id="spnTerms">I agree with Strimm.com<a href="/terms-of-use">Terms of Use</a> </span>
                </div>
            </li>
           
            <li>
                <div id="divSubmit">
                  
                 
<%--                    <a id="btnDeleteChannel" onclick="DeleteChannel()" >delete channel</a>--%>
                    <asp:Button OnClientClick="JavaScript: return DeleteChannel()" runat="server" ID="btnDeleteChannel" ClientIDMode="Static" OnClick="btnDelete_Click" Text="Delete" />

                    <a onclick="ReloadPage()" id="ancCancel">reset</a>
        <asp:Label runat="server" ID="lblMsg" ClientIDMode="Static"></asp:Label>

                    <asp:Button runat="server" ID="btnSubmit"  ClientIDMode="Static" OnClick="btnSubmit_Click" Text="submit" />
                <%--<a ID="btnSubmit" runat="server" ClientIdMode="Static" onclick="SubmitChannel()">submit</a>--%>
                </div>
            </li>
        </ul>
    
        
    </div>
<%--DO NOT REMOVE--%>
  <%--  <div id="cropModal">
        <div>        
<a>crop image</a> 
<br />        
<br />        
<asp:Image ID="cropedImage" runat="server" Visible="False" />        
<br />        
<br />        

<img id="cropbox" />

<asp:HiddenField ID="X" runat="server" />        
<asp:HiddenField ID="Y" runat="server" />        
<asp:HiddenField ID="W" runat="server" />        
<asp:HiddenField ID="H" runat="server" />        

</div>
    </div>--%>
       
   <%--DO NOT REMOVE--%>    