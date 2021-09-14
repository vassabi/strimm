<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="VideoRoom.aspx.cs" Inherits="StrimmTube.VideoRoom" enableEventValidation="false" %>
<%@ Register Src="~/UC/CreateChannelUC.ascx" TagPrefix="uc" TagName="CreateChannelUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Video Room | Strimm
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Strimm TV - Video Room"/>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <%--<link href="/flowplayer/html5/5.5.2/skin/functional.css" rel="stylesheet" />
    <script src="/flowplayer/html5/5.5.2/flowplayer.min.js"></script>--%>
    <link href="/css/videoRoom.css" rel="stylesheet" />
    <link href="/css/CSS.css" rel="stylesheet" />
    <script src="https://www.youtube.com/iframe_api"></script>
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="/JS/Main.js"></script>
    <script src="/JS/VideoRoom.js"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%= new StrimmTube.CorsUpload { }.ToString()%>
       <script type="text/javascript">
           google.load("swfobject", "2.1");

           var userId = '<%=UserId%>';
        var videoRoom = window.getVideoRoom();
        var videoUploader = window.getVideoUploader();

        $(document).ready(function () {
            videoRoom.init(userId, cfWebOrigin, cfRtmpOrigin, cfStagingOrigin);
            videoUploader.init(access_key, policy, signature, s3UploadUrl);
        });
    </script>

    <div id="main">
      

        <div id="divHeader">

            <h1 class="vrPageTitle"> Video Room</h1>
            <div class="gradient"></div>

            <div id="DashboardBackgroungImg" >
                <a href="#"> <asp:Image runat="server" id="imgDashboard" ClientIDMode="Static"/></a>
            </div>
   
            <div id="headMenu">
                <asp:Label runat="server" ID="lblUserInfo" ClientIDMode="Static"></asp:Label>
                <asp:Label runat="server" ID="lblCountry" ClientIDMode="Static"></asp:Label>
       
                <div id="btnWatchVideoHolderDashboard"><a id="ancHowTo" onclick="ShowTutorialPlayer('Bt-EHImRpJU',false)" >watch tutorial</a></div>
            </div>

            <div id="dashboardAvatar">   
                <asp:Image runat="server" id="imgDashboardAvatar" ClientIDMode="Static" ImageUrl="/images/imgUserAvatar.jpg" />
            </div>
        </div>

        <div id="divActions">
                    
            <div id="divVideoProviderSelection">
                <div id="divUploadVideoHolder">
                <span onclick="videoRoom.uploadNewVideo()">+ Upload</span>
            </div>
                <asp:CheckBox ID="chkShowMyVideos"  class="chkShowMyVideos" runat="server" ClientIDMode="Static" Text="My Videos" onclick="videoRoom.videoSearchCriteriaChanged()"/>
                <asp:CheckBox ID="chkShowLicensedVideos" class="chkShowLicensedVideos"  runat="server" ClientIDMode="Static" Text="Licensed Videos" onclick="videoRoom.videoSearchCriteriaChanged()"/>
                <asp:CheckBox ID="chkShowExternalProvidersVideos" class="chkShowExternalProvidersVideos" runat="server" ClientIDMode="Static" Text="YouTube & Vimeo" onclick="videoRoom.videoSearchCriteriaChanged()"/>
            </div>


            <div id="divVideoSortingHolder">
<div class="styled-selectSortSearch categoryKeyword">
                <div id="divVideoCategoryFilter">
                    <asp:DropDownList runat="server" ID="ddlVrCategory" CssClass="ddlVrCategory" AutoPostBack="false" onclick="videoRoom.selectedCategoryChanged()" ClientIDMode="Static">
                    </asp:DropDownList>
                </div>
    </div>
            <div class="styled-selectSort">
            <div id="divVideoSort" class="sortBy styled-selectSort sortBySchedule">
                <select id="ddlChannelVideos" onchange="videoRoom.sortVideos(this)">
                    <option value="0">sort results</option>
                    <option value="1">longer to shorter</option>
                    <option value="2">shorter to longer</option>
                    <option value="3">most viewed</option>
                    <option value="4">recently added</option>
                </select>
            </div>
</div>

        </div>
            </div>



        <div class="vrSearchHolder">
            <div class="vrSearchMidHolder">
            <input id="txtVideoSearchKeywords" class="vrSearch" type="text" placeholder="Search Video Room" />
            <a id="search" class="vrAncSearch" onclick="videoRoom.getMoreVideos(true)">search</a>
                </div>
        </div>


        <div id="divVideosHolder">
        </div>

        <div id="divLoadMore" onclick="videoRoom.getMoreVideos()">Load More</div>

    </div>
    
    <uc:FeedBack ID="FeedBack1" runat="server" pageName="My Video Room"/>

    <div id="uploadVideoPopup" style="display:none;">
        <div class="close_xVR" onclick="videoRoom.cancelVideoChanges()"></div>
        <div class="popUpUploadVideoTitle">
           <h1>Upload Video</h1>
        </div>
        <div id="divFileUpload">
            <div id="divErrorMessage" style="display:none;height:30px;width:100%;background-color:red">
                <span id="spnErrorMessage" style="font-weight:bold;color:white;">No Error Message</span>
            </div>
            <div id="divFileUploadHolder" class="divFileUploadHolder">
                <input class="videoRoomUpload" type="file" id="btnBrowseFile" name="browseFile" value="Browse" style="display:block" onchange="videoUploader.onFileSelected()"/>
	            <div>
		            <div class="vrBrowseButton" onclick="btnBrowseFile.click()">Browse</div>
		            <div id="lblFilename" style="width:250px;line-height:21px;height:21px;">select file</div>
	            </div>
                <input type="button" id="btnUpload" class="btnUploadVR" name="uploadFile" value="Upload" style="float:right;" onclick="videoUploader.uploadVideo()"/>
            </div>  
            <div id="progressBar" class="ui-progressbar" style="display:none;">
                <div id="progressLabel" class="progress-label"></div>
            </div>  
            <div id="divExistingFileHolder" style="display:none;width:100%">
                <div id="divCustomVideoLink" onclick="showStrimmVideo(this);">Click to view video</div>
            </div>        
        </div>
        <div id="divLeftSideHolder">
            <div id="divVideoTitle">
                <input id="txtVideoTitle" placeholder="Video Title" />
            </div>
            <div id="divVideoCategory">
                <select id="ddlVideoCategory" onchange="videoUploader.videoCategoryChanged()">
                </select>
            </div>
            <div id="divVideoDescription">
                <textarea id="txtVideDescription" rows="4" cols="50" placeholder="Description(150 characters max)">
                </textarea>        
            </div>
            <div id="divVideoKeywords">
                <textarea id="txtVideoKeywords" rows="4" cols="50" placeholder="Keywords\n\r(up to 10 keywords, separated by coma">
                </textarea>
            </div>
            <div id="divPrivacy" style="width:130px">
               <span id="divPrivacyLabel" class="selectThumbTitle">Privacy Settings</span>
               <div class="chkHolder">
                    <input type="radio" id="chkPublic" value="public" name="rdPrivacy"  onclick="videoUploader.privacyFlagChanged()"/> <span class="chkPublic">Public</span>
                </div>
                <div class="chkHolder">
                    <input type="radio" id="chkPrivate" value="private" name="rdPrivacy" onclick="videoUploader.privacyFlagChanged()"/><span class="chkPrivate">Private</span>  
                </div>      
            </div>
            <div id="divParentalControl" style="width:130px">
                <span id="divParentalControlLabel" class="selectThumbTitle">Content Rating</span>
                <input type="checkbox" id="chkRRated" class="chkPrivate" value="" /><span class="chkPrivate">R-Rated</span>
            </div>        
        </div>
        <div id="divRightSideHolder">
            <div id="divThumbnailSelection">
                <div id="divThumbnailsHolder">
                    <div class="selectThumbTitle">Select Thumbnail</div>
                    <div id="divFirstThumbnail" onclick="videoUploader.selectFirstThumbnailAsActive()">
                        <img src="/images/comingSoonBG.jpg" id="imgFirstThumbnail"  style="height:100%;width:100%;"/>
                    </div>
                    <div id="divSecondThumbnail" onclick="videoUploader.selectSecondThumbnailAsActive()">
                        <img src="/images/comingSoonBG.jpg" id="imgSecondThumbnail"  style="height:100%;width:100%;"/>
                    </div>
                    <div id="divThirdThumbnail" onclick="videoUploader.selectThirdThumbnailAsActive()">
                        <img src="/images/comingSoonBG.jpg" id="imgThirdThumbnail"  style="height:100%;width:100%;"/>
                    </div>
                </div>
                <div id="divCustomThumbnailHolder">
                    <div class="selectCustomThumbTitle"> Custom thumbnail to certain video frame</div>
                    <div id="divCustomThumbnailParamHolder">
                        <div id="divCustomThumbnail" style="float:left;vertical-align:bottom;">
                            <img src="/images/comingSoonBG.jpg" id="imgCustomThumbnail" style="height:100%;width:100%;"/>
                        </div>
                        <div id="divCustomThumbnailHrs" style="float:left;vertical-align:bottom;width:60px;margin-top:40px;">
                            <input type="text" id="customThumbnailHrs" value="" style="width:20px;height:20px;vertical-align:bottom;" /> 
                            <span id="spnCustomThumbnailHrsLabel">Hrs</span>
                        </div>
                        <div id="divCustomThumbnailMin" style="float:left;vertical-align:bottom;width:60px;margin-top:40px;">
                            <input type="text" id="customThumbnailMin" value="" style="width:20px;height:20px;vertical-align:bottom;" /> 
                            <span id="spnCustomThumbnailMinLabel">Min</span>
                        </div>
                        <div id="divCustomThumbnailSec" style="float:left;vertical-align:bottom;width:60px;margin-top:40px;">
                            <input type="text" id="customThumbnailSec" value="" style="width:20px;height:20px;vertical-align:bottom;" /> 
                            <span id="spnCustomThumbnailSecLabel">Sec</span>
                        </div>
                    </div>
                    <div id="divCustomThumbnailActionsHolder" style="float:right;width:50px;">
                        <input type="button" id="btnUseCustomImage" value="Use" onclick="videoUploader.setCustomThumbnailAsActive()" style="width:50px;height:30px;"/>
                        <input type="button" id="btnGetCustomImage" value="Get" onclick="videoUploader.getCustomThumbnail()" style="width:50px;height:30px;margin-top:5px;"/>
                    </div>
                </div>
                <div id="divPreviewClipHolder">
                    <span class="previewVideo">Preview video that will be displayed to other users</span>
                    <div id="divVideoPreviewHolder" onclick="videoUploader.playVideoPreviewClip()">
                        <span class="VideoPreview">Preview</span>
                    </div>
                </div>

                <div id="divRentalFeeHolder">
                    <div id="divRentalFeeDescription">
                        <div class="rentalFeeDescription">
                        <span class="rentalFee">Rental Fee per view/hour (credits)</span></br>
                        <span class="rentalFeeBlank">(leave blank if no fee is required)</span>
                            </div>
                          <input type="text" id="txtRentalFeeAmount" placeholder="Amount" />

                    </div>
                  
                </div>
                <div id="divDisclaimer">
                    <span id="spnTermsOfUse">By uploading this video to Strimm.com I confirm that I own all copyrights to this video and I agreee to <a href="/terms-of-use">Terms of Use</a></span>
                </div>
            </div>        
        </div>
        <div id="divUploadVideoActions">
            <div id="divUpdateVideo">
                <span onclick="videoUploader.updateVideo()">Save</span>
            </div>  
            
             <div id="divCancelChanges">
                <span id="btnClose" class="close" onclick="videoRoom.cancelVideoChanges()">Cancel</span>
            </div>       
        </div>
    </div>

      <a onclick="ShowFeedback()" class="feedeback rotate"></a>

</asp:Content>
