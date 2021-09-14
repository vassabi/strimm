<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OttSettingsForm.ascx.cs" Inherits="StrimmTube.UC.OttSettingsForm" %>
<h1 class="pageTitle">OTT Settings</h1>
<a class="close_x" href="#" onclick="CloseOTTSettingsPopup()">close</a>

<div class="modalBody">
    <div class="createModalLeft">
        <div id="toggleAddChannelToRoku" class="inputEmbeddedOFF" onclick="CreateChannel.ToggleAddChannelToRoku()">
        </div>
        <span class="spnAdvancedMode">Add This Channel to Roku App</span>
        <div class="subscribe_error" id="addToRokuError"></div>
    </div>
    <div class="createModalRight">
        <a href="https://www.strimm.us/roku-app-instructions" target="_blank">Click Here To See Instructions</a>
    </div>
    
    <div class="ottDemo">
        <div class="createModalLeft"><img src="/images/ott_demo_left.jpg" /></div>
        <div class="createModalRight"><img src="/images/roku_app-7.20.21-1.jpg" /></div>
    </div>
    <div class="ottData">
        <div style="margin-bottom:15px;">
            <small><u>Attention:</u> Generate Roku App zip file for the first channel only. If Roku App name or images are changed, then a new zip file should be generated and uploaded to Roku. If "About" or links are changed, then click UPDATE button to save the changes. Don't re-generate a zip file.</small>
        </div>
        <table>
            <tr>
                <td>
                    <input id="txtRokuAppName" onchange="CreateChannel.RokuAppRegenerateRequired();" maxlength="30" placeholder="Enter Your Roku App Name (Your Roku TV Network)" class="cap">
                    <small>(Up to 30 characters maximum, letters or digits only, including spaces. No special characters. It must be unique for Roku.)</small>
                </td>
                <td>
                    <input id="txtPrivacyPolicyLink" placeholder="Enter Privacy Policy Link (optional)" class="cap" />
                </td>
            </tr>
            <tr>
                <td>
                    <textarea id="txtAboutApp" maxlength="500" rows="5" class="cap" placeholder="About app (optional)"></textarea>
                </td>
                <td>
                    <input id="txtRokuAdLink" placeholder="Enter Advertisment Link (optional)" class="cap">
                    <small>(Advertisement link is optional. It is used for advertisement on Roku and has a specific format. Please check instructions above to learn about it)</small>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="imageCropperTitle">IMAGE #1 (Your App Image/Avatar - HD quality)</div>
                </td>
                <td>
                    <div class="imageCropperTitle">IMAGE #2 (Your App Image/Avatar - SD quality)</div>
                </td>
            </tr>
            <tr>
                <td>
                    <ul>
                        <li class="liImageEditor">
                            <div class="image-editor-hd">

                                <div class="image-preview-container">
                                    <div id="hdImagePreview" class="image-preview hd"></div>
                                </div>
                                <div class="minImgSize">Size: 290 X 218 px, PNG only</div>
                                <div class="img-validation"></div>

                                <div class="select-image-btn">Upload Channel Image</div>
                                <input type="hidden" id="hdnHdImageValid" value="0" />
                                <input type="hidden" id="hdnHdImageData" />
                                <input type="file" name="hd-image" class="cropit-image-input" />
                            </div>
                        </li>
                    </ul>
                </td>
                <td>
                    <ul>
                        <li class="liImageEditor">
                            <div class="image-editor-sd">

                                <div class="image-preview-container">
                                    <div id="sdImagePreview" class="image-preview sd"></div>
                                </div>
                                <div class="minImgSize">Size: 246 X 140 px, PNG only</div>
                                <div class="img-validation"></div>

                                <div class="select-image-btn">Upload Channel Image</div>
                                <input type="hidden" id="hdnSdImageValid" value="0" />
                                <input type="hidden" id="hdnSdImageData" />
                                <input type="file" name="sd-image" class="cropit-image-input" />
                            </div>
                        </li>
                    </ul>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <a id="genRokuAppBtn" onclick="CreateChannel.GenerateDownloadRokuApp()">Update Changes</a>
                    <div class="app-validation" id="appValidation"></div>
                </td>
            </tr>
           
        </table>
    </div>
</div>

