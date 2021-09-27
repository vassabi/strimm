<%@ Page Title="" Language="C#" MasterPageFile="~/admcp/mgmt/Admin.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="StrimmTube.admcp.UserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Plugins/TablePlugin/css/styleRespScroll.css" rel="stylesheet" />
    <link href="Plugins/TablePlugin/css/stylesContact.css" rel="stylesheet" />
    <link href="css/userManagment.css" rel="stylesheet" />
     <link href="DataTables-1.10.5/media/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css"/>
    <link href="../../css/CreateChannel.css" rel="stylesheet" />
      <link href="DataTables-1.10.5/TableTools/css/dataTables.tableTools.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="Plugins/TablePlugin/js/modernizr.custom.63321.js"></script>
  
   
    <script src="DataTables-1.10.5/media/js/jquery.dataTables.min.js" charset="utf-8"></script>
    <script src="DataTables-1.10.5/TableTools/js/dataTables.tableTools.min.js"></script>
    <script src="js/UserManagment.js"></script>
    <script type="text/javascript">
        var email = '<%=email%>';
        var isRedirected = '<%=isRedirected%>';
    </script>
    

        <div class="userManagementHolder">
    <h1 class="userManagementTitle">User Management</h1>
    </div>

    <div id="contentUser">
         <div id="DivSearchUser">
        <span id="spnSearch">search for user</span>
        <input type="text" id="txtSearch" class="UMserchAdmin" />
        <select>
            <option value="0">please select</option>
            <option value="1">username</option>
            <option value="2">account #</option>
            <option value="3">email</option>
            <option value="4">channel name</option>
        </select>
        <a id="ancGo" class="button blue" onclick="GetUserinfo()">go</a>
         <a id="ancResetUsers" class="button blue" onclick="Reset()">reset</a>
    </div>
        <div id="divShowUser" style="display:none;">
   
    
    <div id="divUserTable">
        <span id="spnUser">account status:</span>
        <span id="spnStatus"></span>

        <table id="table1" class="tinytable" cellspacing ="0" cellpadding="0" border="0">
         <thead>
             <tr>
                 <th>
                     <h3>
                         name
                     </h3>
                 </th>
                  <th>
                     <h3>
                       gender
                     </h3>
                 </th>
                  <th>
                     <h3>
                       age
                     </h3>
                 </th>
                  <th>
                     <h3>
                          public name
                     </h3>
                 </th>
                  <th>
                     <h3>
                         account #
                     </h3>
                 </th>

                 <th>
                     <h3>
                         email
                     </h3>
                 </th>
                  <th>
                     <h3>
                       date of sign up
                     </h3>
                 </th>
                  <th>
                     <h3>
                       pro enabled?
                     </h3>
                 </th>                  
                 <th>
                     <h3>
                       subscriber?
                     </h3>
                 </th>                  
                  <th>
                     <h3>
                       domain count
                     </h3>
                 </th> 
                 <th>
                     <h3>
                       company
                     </h3>
                 </th>
                  <th>
                     <h3>
                          country
                     </h3>
                 </th>
                  <th>
                     <h3>
                         state/province
                     </h3>

                 </th>
                 <th>
                     <h3>
                         address
                     </h3>
                 </th>
                  <th>
                     <h3>
                       city
                     </h3>
                 </th>
                  <th>
                     <h3>
                       zip/postal code
                     </h3>
                 </th>
                  <th>
                     <h3>
                          channels
                     </h3>
                 </th>
                  <th>
                     <h3>
                         board
                     </h3>
                 </th>
                 <th>
                     <h3>
                         account status
                     </h3>
                 </th>
             </tr>
         </thead>
         <tbody>
             <tr>
                 <td>
                     <span id="spnName"></span>
                 </td>
                  <td>
                     <span id="spnGender"></span>
                 </td>
                  <td>
                     <span id="spnAge"></span>
                 </td>
                  <td>
                     <span id="spnUserName"></span>
                 </td>
                  <td>
                    <span id="spnAccountNum"></span>
                 </td>

                  <td>
                     <span id="spnEmail"></span>
                 </td>
                  <td>
                     <span id="spnDateofSignUp"></span>
                 </td>
                  <td>
                     <span id="spnIsProEnabled"></span>
                 </td>
                  <td>
                     <span id="spnIsSubscriber"></span>
                 </td>
                  <td>
                     <span id="spnSubscriberDomainCount"></span>
                 </td>
                  <td>
                     <span id="spnCompany"></span>
                 </td>
                  <td>
                     <span id="spnCountry"></span>
                 </td>
                  <td>
                    <span id="spnState"></span>
                 </td>

                  <td>
                     <span id="spnAddress"></span>
                 </td>
                  <td>
                     <span id="spnCity"></span>
                 </td>
                  <td>
                     <span id="spnZipCode"></span>
                 </td>
                  <td>
                     <span id="spnChannels"></span>
                 </td>
                  <td>
                    <span id="spnBoard"></span>
                 </td>
                  <td>
                    <span style="color:red;" id="spnAccountStatus"></span>
                 </td>
             </tr>
         </tbody>
     </table>
    </div>
    <div id="divSubscriberDomains" style="display:none">
        <%--user Control per channel--%>
        <span>Authorized Domains</span>
       <div id="subscriberDomains">
           <input type="text" id="newSubscriberDomain" style="width:150px;" /><a id="ancAuthorizeNewDomain" class="button blue " onclick="AuthorizeNewDomainForUser()">Authorize</a>
           <div id="domainHolder" style="width:250px">
           </div>
       </div>
    </div>
    <div id="divChannels">
        <%--user Control per channel--%>
        <span>CHANNELS</span>
       <div id="channelBoxHolder"></div>
    </div>
             <%-- <div>
                    <a onclick="CreateTrialSubscribtion()">Create Trial Subscribtion</a>
                </div>--%>
                 <div id="createNewSubscibtion">
                    <select id="subscribtionSelect">
                     
                    </select>
                    <label for="ammount">price(optional):</label>
                    <input id="ammount" value="0.01"/>
                  
                    <a onclick="CreateSubscribtion()">Create Subscribtion</a>
                </div>
            <div id="userSubscribtionsHolder">
                <h1>Mannual Subscribtion  (channel embedding)</h1>
                
        <table id="subscribtionTable" class="tinytable" cellspacing ="0" cellpadding="0" border="0">
         <thead>
             <tr>
                 <th>
                     <h3>
                         subscribtion type
                     </h3>
                 </th>
                  <th>
                     <h3>
                       channels limit
                     </h3>
                 </th>
                  <th>
                     <h3>
                       last creation
                     </h3>
                 </th>
                  <th>
                     <h3>
                         ammount paid
                     </h3>
                 </th>
                  <th>
                     <h3>
                         period
                     </h3>
                 </th>

                 <th>
                     <h3>
                         expiration date
                     </h3>
                 </th>
                  <th>
                     <h3>
                       transaction id
                     </h3>
                 </th>
                  <th>
                     <h3>
                       order number
                     </h3>
                 </th>                  
                <th>
                     <h3>
                       subscibtion status
                     </h3>
                 </th>        
                 
             </tr>
         </thead>
         <tbody>
             <tr>
                  <td>
                     <div class="liContentWrapper">
                               
                                <div id="toggleNewBasic" class="inputBasicOFF" onclick="ToggleBasicSubscribtion()"></div>
                                 <span>New Basic</span>
                                
                            </div>
                 </td>
                  <td>
                     <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtChannelAmmount" value="1" />
                                </div>
                 </td>
                  <td>
                      <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtCreationDate" value="1" />
                                </div>
                 </td>
                  <td>
                       <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtAmmountPaid" value="1" />
                                </div>
                 </td>
                  <td>
                     <div class="inputHolder">
                      
                                    <input type="radio" class="basicPackage" id="radioMonth" value="1" />
                            <label for="radioMonth">M</label>
                        
                         <input type="radio" class="basicPackage" id="radioYear" value="1" />
                           <label for="radioYear">Y</label>
                                </div>
                 </td>

                  <td>
                       <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtExpireDate" value="1" />
                                </div>
                 </td>
                  <td>
                       <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtTransactionId" value="1" />
                                </div>
                 </td>
                  <td>
                      <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtOrderNumber" value="1" />
                                </div>
                 </td>
                 
             </tr>
                <tr>
                  <td>
                     <div class="liContentWrapper">
                               
                               <div id="toggleAdvanced" class="inputAdvancedOFF" onclick="ToggleAdvanced()"></div>
                                
                                 <span>Advanced</span>
                                
                            </div>
                 </td>
                  <td>
                     <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtChannelAmmountAdv" value="1" />
                                </div>
                 </td>
                  <td>
                      <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtCreationDateAdv" value="1" />
                                </div>
                 </td>
                  <td>
                       <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtAmmountPaidAdv" value="1" />
                                </div>
                 </td>
                  <td>
                     <div class="inputHolder">
                        
                                    <input type="radio" class="basicPackage" id="radioMonthAdv" value="1" />
                          <label for="radioMonthAdv">M</label>
                         
                         <input type="radio" class="basicPackage" id="radioYearAdv" value="1" />
                          <label for="radioYearAdv">Y</label>
                                </div>
                 </td>

                  <td>
                       <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtExpireDateAdv" value="1" />
                                </div>
                 </td>
                  <td>
                       <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtTransactionIdAdv" value="1" />
                                </div>
                 </td>
                  <td>
                      <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtOrderNumberAdv" value="1" />
                                </div>
                 </td>
                 
             </tr>
                   <tr>
                  <td>
                     <div class="liContentWrapper">
                               
                                 <div id="toggleNewProfessional" class="inputNewProfessionalOFF" onclick="ToggleNewProfessional()"></div>
                                 
                                <span>New Professional </span>
                               
                            </div>
                 </td>
                  <td>
                     <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtChannelAmmountProf" value="1" />
                                </div>
                 </td>
                  <td>
                      <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtCreationDateProf" value="1" />
                                </div>
                 </td>
                  <td>
                       <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtAmmountPaidProf" value="1" />
                                </div>
                 </td>
                  <td>
                     <div class="inputHolder">
                         
                                    <input type="radio" class="basicPackage" id="radioMonthProf" value="1" />
                         <label for="radioMonthProf">M</label>
                         
                         <input type="radio" class="basicPackage" id="radioYearProf" value="1" />
                          <label for="radioYearProf">Y</label>
                                </div>
                 </td>

                  <td>
                       <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtExpireDateProf" value="1" />
                                </div>
                 </td>
                  <td>
                       <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtTransactionIdProf" value="1" />
                                </div>
                 </td>

                  <td>
                      <div class="inputHolder">
                                    <input type="text" class="basicPackage" id="txtOrderNumberProf" value="1" />
                                </div>
                 </td>
                        <td>
                      <div class="inputHolder">
                                    <span class="basicPackage" id="txtOrderStatus"></span>
                                </div>
                 </td>
                 
             </tr>
         </tbody>
     </table>
           
              
       <%--        <ul>
                   <li></li>
                   <li></li>
                    <li><div class="liContentWrapper">
                               
                                </li>
                    <li><div class="liContentWrapper">
                               
                              </li>

                   <li style="border-top:1px solid #ccc; clear:both;"><h1>OLD SUBSCRIBTIONS</h1></li>
                    <li >
                        <div class="liContentWrapper">
                               
                                <div id="toggleBasicOld" class="inputBasicOldOFF" onclick="ToggleBasicOld()"></div>
                                  <span>Basic(old) </span>
                                <div class="inputHolder">
                                    <input type="text" id="oldBasic" value="1" />
                                </div>
                            </div></li>
                    <li><div class="liContentWrapper">
                               
                                <div id="toggleStandard" class="inoutStandardOFF" onclick="ToggleStandard()"></div>
                                 <span>Standard</span>
                                <div class="inputHolder">
                                    <input type="text" id="standard" value="1" />
                                </div>
                            </div></li>
                    <li><div class="liContentWrapper">
                               
                                 <div id="toggleProfessionalOld" class="inputProfessionalOldOFF" onclick="ToggleProfessionalOld()"></div>
                                 
                                <span>Professional (old) </span>
                                <div class="inputHolder">
                                    <input type="text" id="ProfessionalOld" value="1" />
                                </div>
                            </div>
                   </li>

                   
               </ul>--%>
                <div class="createUpdateHolder">
                    
                    <a id="btnUpdate" class="export" onclick="UpdateSubscribtion()">Update</a>
                   
                </div>
            </div>
         <div id="divAdminAdminNotes">
        <span></span>
             
             <div id="divEditNotes">
                  <div id="adminActionTable">
                
    </div>
               <span style="display:block; float:left;">Add notes</span>  
        <textarea id="txtAreaAdminNotes" rows="10" cols="160"></textarea>
                <a onclick="SaveNotes()" id="ancSaveNote" class="button blue" >save notes</a>
        </div>
           <%-- <a onclick="ShowEdit()" id="ancEditNotes" class="button blue">edit notes</a>--%>
    </div>
    <div id="channelActionsDiv">
        <a id="ancClearSchedule" class="button blue" onclick="ClearSchedules()">clear schedules</a>
        <a id="ancDeleteChannel" class="button" onclick="DeleteChannel()">delete selected channel</a>
        <a id="ancLockUnlockUser" class="button blue " onclick="LockUnlockUser()">lock access</a>
        <a id="ancDeleteAccount" onclick="DeleteAccount()">delete entire account</a>
        <a id="ancSendWelcomeEmail" class="button blue" onclick="SendWelcomeEmailToUser()">Send Welcome Email</a>
        <a id="ancEnabledDisablePro" class="button blue " onclick="EnabledDisablePro()">Enable Pro</a>
        <a id="ancAddRemoveSubscription" class="button blue " onclick="AddRemoveSubscription()">Add Subscription</a>
    </div>
            </div>
   

   

    </div>
    <div id="createSubscr" style="display:none;">
        <h4>Create new subscribtion for user <span id="userEmailSubscribtion">&nbsp; Robert</span></h4>
        <span id="firstName">firstName</span>
        <span id="lastName">lastName</span>
        <select id="selectProduct">
            <option>select subscribtion</option>
            <option value="1">Basic</option>
            <option value="2">Standart</option>
            <option value="3">Custom pricing</option>
            <option value="5">Advanced</option>
            <option value="4">Professional</option>
        </select>
        <label for="subPrice"></label>
       <input id="subPrice" />
        <label for="channelCountSubsbcr"></label>
        <input id="channelCountSubsbcr" />

        <a>Create</a>
        <a>Cancel</a>
        
    </div>
</asp:Content>
