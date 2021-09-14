<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateChannelUC.ascx.cs" Inherits="StrimmTube.UC.UpdateChannelUC"  %>


<script src="/Scripts/jquery.ui.core.js" type="text/javascript"></script>
<script src="/Scripts/jquery.ui.widget.js" type="text/javascript"></script>
<script src="/Scripts/jquery.ddslick.js" type="text/javascript"></script>   
   
<script src="/jquery/Jcrop/js/jquery.Jcrop.min.js"></script>
<script src="/Plugins/popup/jquery.lightbox_me.js"></script>


<script type="text/javascript">
    var updateChannelId = "<%=channelTubeId%>";
    var updateChannelCategory = "<%=channelCategory%>";
    var updateSelectIndexCategory = "<%=selectIndexCategory%>";
    var updateDomainName = '<%=domainName%>';
    var updateWebMethodGetChannelCategoriesJson = "/WebServices/ChannelWebService.asmx/GetChannelCategories";
    var updateWebMethodCheckChannelName = "/WebServices/ChannelWebService.asmx/CheckChannelName";
    var updateWebMethodDeleteChannel = "/WebServices/ChannelWebService.asmx/DeleteChannel"
    var updateChannelName = '<%=channelname%>';
    var updateUserName = '<%=UserName%>';

    $(document).ready(function () {
        $('#txtDescription').keypress(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
            }
        });

        if (channelId == "0") {
            $("#btnDeleteChannel_Update").css("visibility", "hidden");
        }
           
    });

    // add image to img on select
    function readURL_Update(input) {
        if (input.files && input.files[0]) {

            var reader = new FileReader();
            reader.onload = function (e) {
                $("#imgChannelAvatar_Update")
                .attr('src', e.target.result)
                .width(50)
                .height(50);
            };
            reader.readAsDataURL(input.files[0]);
        }
    }

    function ReloadPage_Update() {
        $("#fuChannelAvatar_Update").val("");
        location.reload();
    }

    function DeleteChannel_Update() {
        var r = confirm("Warning: The channel and all schedules will be deleted");
        return r;
    }

    var api;
    var cropWidth = 200;
    var cropHeight = 200;

    function ShowCropModal_Update() {
        var imageSrc = $("#imgChannelAvatar_Update").attr("src");
        $("#cropbox").removeAttr("src").attr("src", imageSrc);
        $('#cropModal').lightbox_me({
            centered: true,
            onLoad: function () {
                var jcrop_api;
                var i, ac;

                initJcrop();
            },
            onClose: function () {
                RemoveOverlay();
            }
        });
    }

    function initJcrop_Update() {
        jcrop_api = $.Jcrop('#cropbox_Update', {
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

    function SizeAlertUp_Update() {
        ShowMessage("The image type must be either .jpeg or .png and should not exceed 300kb in size");
    };
</script>

<div class="Scallop_Update"></div>

<div runat="server" id="divBackendmenuHolder_Update">
       
</div>

<div id="divBoardContent_Update">
    <ul>
        <li>
            <div id="divSubmitUp_Update">
            </div>
        </li>
        <li><span class="spnTitleUpdate">channel name</span>
                
            <div runat="server" id="divLoadLabel_Update" ClientIDMode="Static" >
                <asp:Label ID="lblChannelName_Update" runat="server" class="lblChannelName_Update" ClientIDMode="Static"></asp:Label>
            </div>
                
        </li>
        <li><span class="spnTitleUpdate">channel category</span>
            <input type="hidden"  id="category_Update" runat="server" clientidmode="Static" />
            <asp:DropDownList ID="ddlChannelCategory_Update"  ClientIDMode="Static" OnSelectedIndexChanged="SelectedCategoryChanged" runat="server"></asp:DropDownList>
        </li>
        <li>
            <div class="picLogoHolder">
            <span class="spnTitle picLogo">channel picture/Logo</span>
            <span class="spnPorametr">(300k max, .jpg or .png files only)</span>
                </div>
            <div class="fuimgHolder">
            <asp:Image runat="server" ID="imgChannelAvatar_Update" ClientIDMode="Static"  ImageUrl="/images/comingSoonBG.jpg"/>
            <div id="fuHolder_Update"> 
                <span>browse</span>
            <asp:FileUpload runat="server" ID="fuChannelAvatar_Update" ClientIDMode="Static" onchange="readURL_Update(this)" />
                </div>
            <a onclick="ShowCropModal_Update()" class="cropImg">crop image</a>
                </div>
        </li>
        <li><span class="spnTitle">channel URL</span>
            <asp:Label runat="server" ID="lblChannelUrl_Update" ClientIDMode="Static"></asp:Label></li>
        <li>
            <div id="divTermOfUse_Update" style="display:none;">
                <asp:CheckBox runat="server" ID="chkBoxterms_Update" Checked="true" ClientIDMode="Static" /><span id="spnTerms_Update">I agree with Strimm.com<a href="/terms-of-use">Terms of Use</a> </span>
            </div>
        </li>
           
        <li>
            <div id="divSubmit_Update">
                <asp:Button OnClientClick="JavaScript: return DeleteChannel_Update()" runat="server" ID="btnDeleteChannel_Update" ClientIDMode="Static" OnClick="btnDelete_Click" Text="Delete" />

                <a onclick="ReloadPage_Update()" id="ancCancel_Update">reset</a>
    <asp:Label runat="server" ID="lblMsg_Update" ClientIDMode="Static"></asp:Label>

                <asp:Button runat="server" ID="btnSubmit_Update" ClientIDMode="Static" OnClick="btnSubmit_Click" Text="submit" />
            </div>
        </li>
    </ul>
    
        
</div>