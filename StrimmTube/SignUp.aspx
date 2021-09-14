<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="StrimmTube.SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Strimm Sign Up | Join Strimm
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Join Strimm Public TV Network of the 21st Century" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://www.strimm.com/sign-up" />

</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <%--  <link href="css/signUpCSS.css" rel="stylesheet" />--%>
    <%: System.Web.Optimization.Styles.Render("~/bundles/signup/css") %>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/signup/js") %>

    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <script src="JS/SignUp.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
</script>
    <div id="container">
        <div class="mainContentWrapper">
            <div class="createSignUp">
                <h1 class="signUpHeader">Sign Up & Broadcast!</h1>
                <%--    <h2 class="signUpSlogan">Be Extraordinary. Strimm it!</h2>--%>
            </div>

            <div class="MainSignUpHolder">

                <div class="signUpTextHolder">
                    <%--                <div class="createSignUp">
                <h1 class="signUpHeader">Sign Up</h1>
                <h2 class="signUpSlogan">Be Extraordinary. Create. Strimm it!</h2>
             </div>--%>
                </div>
                <div id="contentSignUp">

                    <div id="divSignUpContent">

                        <%--        <h1 class="pageTitle">Sign Up! It's free!</h1>--%>

                        <div id="divSignUp">

                            <ul>
                                <li class="haveAnAccount">Do you have an account?  <a onclick="loginModal('sameLocation')">Login </a></li>

                                

                                <li>
                                    <div class="sign-up-divider">
                                        <span>OR</span>
                                    </div>
                                </li>
                                <li id="validateFirstNameLi">
                                    <div onclick="ShowSnippetPopup(this)" class="infoPlasAbs" title="We will use this information in order to verify you, and to provide you with an access to your account, and in order to contact you with important information about any changes to your account. Check our  Privacy Policy (www.strimm.com/privacy-policy ) for details. 
">
                                    </div>
                                    <asp:TextBox runat="server" ID="txtFirstName" ClientIDMode="Static" placeholder="First Name" class="cap"></asp:TextBox>
                                    <div class="divError" id="erFirstName">
                                        <div class="spnErrorCloseMobile"></div>
                                        <span class="spnError">First Name is required</span></div>

                                </li>



                                <li id="validateLastNameLi">
                                    <div onclick="ShowSnippetPopup(this)" class="infoPlasAbs" title="We will use this information in order to verify you, and to provide you with an access to your account, and in order to contact you with important information about any changes to your account. Check our  Privacy Policy (www.strimm.com/privacy-policy ) for details. 
">
                                    </div>
                                    <asp:TextBox runat="server" ID="txtLastName" ClientIDMode="Static" placeholder="Last Name" class="cap"></asp:TextBox>
                                    <div class="divError" id="erLastName">
                                        <div class="spnErrorCloseMobile"></div>
                                        <span class="spnError">Last Name is required</span></div>
                                </li>



                                <li id="validateEmailLi">
                                    <div onclick="ShowSnippetPopup(this)" class="infoPlasAbs" title="We will use this information in order to verify you, and to provide you with an access to your account, and in order to contact you with important information about any changes to your account. Check our  Privacy Policy (www.strimm.com/privacy-policy ) for details. 
">
                                    </div>
                                    <asp:TextBox runat="server" ID="txtEmail" ClientIDMode="Static" placeholder="Email" type="email" autocomplete="off"></asp:TextBox>
                                    <div class="divError" id="erEmail">
                                        <div class="spnErrorCloseMobile"></div>
                                        <span class="spnError">Email is required</span></div>
                                </li>


                                <li id="validateReenterEmailLi">
                                    <div onclick="ShowSnippetPopup(this)" class="infoPlasAbs" title="We will use this information in order to verify you, and to provide you with an access to your account, and in order to contact you with important information about any changes to your account. Check our  Privacy Policy (www.strimm.com/privacy-policy ) for details. 
">
                                    </div>
                                    <asp:TextBox runat="server" ID="txtReenterEmail" onpaste="MakePasteError()" ClientIDMode="Static" placeholder="Re-enter E-mail" type="email" autocomplete="off"></asp:TextBox>
                                    <div class="divError" id="erReEmail">
                                        <div class="spnErrorCloseMobile"></div>
                                        <span class="spnError">Emails do not match</span></div>
                                </li>


                                <li class="infoPlusRel" id="validateUserNameLi">

                                    <div class="infoPlasAbs" title="Public name is a name shown to public on your Network and Channels. 
It can be your name or a company name. 
It should have 3-15 characters and may include space, dash, 
apostrophe and/or '&' characters only. We will use this information to confirm your age, local laws and regulations and to provide you with a better user experience. Check our Privacy Policy (www.strimm.com/privacy-policy ) for details. 
">
                                        <%--<img src="/images/infoIcon.png" />--%>
                                    </div>
                                    <asp:TextBox runat="server" ID="txtUserNameSignUp" MaxLength="15" ClientIDMode="Static"
                                        placeholder="Public Name" title="Public name is a name shown to public on your Network and Channels. 
It can be your name or a company name. 
It should have 3-15 characters and may include space, dash, 
apostrophe and/or '&' characters only.‘We will use this information to confirm your age, local laws and regulations and to provide you with a better user experience. Check our Privacy Policy (www.strimm.com/privacy-policy ) for details. 
"></asp:TextBox>
                                    <div class="divError" id="erUserName">
                                        <div class="spnErrorCloseMobile"></div>
                                        <span class="spnError">Public Name must have 3-15 characters and may include space, dash, apostrophe and/or '&' characters only.</span>
                                    </div>
                                </li>



                                <li id="validatePasswordLi">
                                    <div onclick="ShowSnippetPopup(this)" class="infoPlasAbs" title="We will use this information in order to verify you, and to provide you with an access to your account, and in order to contact you with important information about any changes to your account. Check our  Privacy Policy (www.strimm.com/privacy-policy ) for details. 
">
                                    </div>
                                    <asp:TextBox runat="server" ID="txtPasswordSignUp" ClientIDMode="Static" TextMode="Password" placeholder="Password"></asp:TextBox>
                                    <label id="lblShowHidePassSignUp">
                                        <input type="checkbox" id="chkBxShowHidePasswordSignUp" onchange="ShowHidePasswordSignUp()" /><span class="spnShowPassword">Show password</span> </label>
                                    <div class="divError" id="erPassword">
                                        <div class="spnErrorCloseMobile"></div>
                                        <span clientidmode="static" runat="server" id="spnErPass" class="spnError">Password must have 8-12 characters with no spaces and cannot contain any special characters</span>
                                    </div>
                                </li>



                                <li id="validateCountryLi">
                                    <asp:DropDownList runat="server" class="cap" ID="ddlCountry" AutoPostBack="false" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="ddlCountry" onchange="validateCountry();"></asp:DropDownList>
                                    <div class="divError" id="erCountry">
                                        <div class="spnErrorCloseMobile"></div>
                                        <span class="spnError">Please select a valid country</span>
                                    </div>
                                </li>


                                <li><span class="birthday">Birthday <span class="ageConfirmation">(for age confirmation)</span></span></li>




                                <li id="validateDateLi">
                                    <asp:UpdatePanel runat="server" ID="up">
                                        <ContentTemplate>
                                            <div class="divError">
                                                <div class="spnErrorCloseMobile"></div>
                                                <span id="erBirthdate" runat="server" class="spnError">You have to be over the age of 18 in order to signup</span>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlMonth" ClientIDMode="static" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" CssClass="ddlBirthday" onchange="validateDay(this);"></asp:DropDownList>
                                            <asp:DropDownList runat="server" ID="ddlDay" ClientIDMode="static" AutoPostBack="true" OnSelectedIndexChanged="ddlDay_SelectedIndexChanged" CssClass="ddlBirthday" onchange="validateMonth(this);"></asp:DropDownList>
                                            <asp:DropDownList runat="server" ID="ddlYear" ClientIDMode="static" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" CssClass="ddlBirthday" onchange="validateYear(this);"></asp:DropDownList>


                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlYear" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlMonth" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlDay" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </li>




                                <%--                    <li>
                        <span class="spnBirthday">(year)</span><span class="spnBirthday cap">(month)</span><span class="spnBirthday cap">(day)</span>
                    </li>--%>
                                <li>
                                    <input type="checkbox" runat="server" clientidmode="Static" id="chkTerms" />
                                    <span class="spnTerms">I agree with <a href="/terms-of-use" target="_blank">Terms of Use</a> and <a href="privacy-policy" target="_blank">Privacy Policy</a></span>
                                </li>
                                <li>
                                    <div class="g-recaptcha" data-sitekey="6LeUkVEaAAAAACfXKL5PVDtqNvchx-On8zHnWgS9"></div>
                                </li>
                                <li>
                                    <div class="buttonHolder">
                                       <asp:Label runat="server" ID="lblMessage" ClientIDMode="Static"></asp:Label>
                                       <asp:Button OnClientClick="return ValidateRequired();" runat="server" ID="btnJoin" Text="Become a Strimmer" OnClick="btnJoin_Click" ClientIDMode="Static" />
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <%-- <div id="divChkTerms">

                    <span id="spnTerms">By clicking <span class="textHigh">Sign Up</span>, you certify that you agree with our</br> <a href="Terms.aspx" target="_blank">Terms of Use</a> and <a href="privacy-policy" target="_blank">Privacy Policy</a> </span>
                </div>--%>
                    <%--      <span class="spnPleaseComplete">A confirmation link will be sent to email provided<br />
                    Your age and location will be hidden in public profiles.
                </span>--%>

                  
                    <%-- </div>--%>
                    <uc:FeedBack runat="server" ID="feedBack" pageName="sign up" />
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
