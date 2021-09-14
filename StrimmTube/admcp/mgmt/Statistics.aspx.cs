using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Strimm.Model.Projections;
using Strimm.Model;

namespace StrimmTube.admcp.mgmt
{
    public partial class Statistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<UserPo> userlist = UserManage.GetAllUserPos();
            //create common table statistics
            lblTotalUsers.Text = userlist.Count.ToString();
            int usersWithCahnnelsCount=0;
            int usersNoChannels = 0;
            foreach (var user in userlist)
            {
                 List<ChannelTubePo> channelTubelistForCount = ChannelManage.GetChannelTubesForUser(user.UserId);
                if (channelTubelistForCount.Count!=0)
                {
                    usersWithCahnnelsCount++;
                }
                else
                {
                    usersNoChannels++;

                }
            }
            lblWithChannels.Text = usersWithCahnnelsCount.ToString();
            lblViewersOnly.Text = usersNoChannels.ToString();
            List<ChannelTube> channelTubeAllList = ChannelManage.GetAllChannelTubes();
            lblTotalChannels.Text = channelTubeAllList.Count.ToString();
           // List<CheckList> allBoards = UserManage.getAllCheckLists();
            int boardsCount = 0;
            //foreach (var check in allBoards)
            //{
            //    if(check.isHasProfile==true)
            //    {
            //        boardsCount++;
            //    }
            //}
            lblTotalBoards.Text = boardsCount.ToString();
            //create user statistics

           

            var table = new Table();
            table.ID = "table";
            table.ClientIDMode = ClientIDMode.Static;
            table.Attributes.Add("cellpadding", "0");
            table.Attributes.Add("cellspacing", "0");
            table.Attributes.Add("border", "0");
            table.Attributes.Add("class", "tinytable cf");
            TableRow hRow = new TableHeaderRow();
          


            var fullNameCell = new TableHeaderCell();
            HtmlGenericControl h1 = new HtmlGenericControl("h3");
            h1.InnerText="User Name";
            fullNameCell.Controls.Add(h1);
            var genderCell = new TableHeaderCell();
            HtmlGenericControl h2 = new HtmlGenericControl("h3");
            h2.InnerText="gender";
            genderCell.Controls.Add(h2);

            var ageCell = new TableHeaderCell();
             HtmlGenericControl h3 = new HtmlGenericControl("h3");
             h3.InnerText = "age";
             ageCell.Controls.Add(h3);


            var accountNumCell = new TableHeaderCell();
            HtmlGenericControl h4 = new HtmlGenericControl("h3");
            h4.InnerText= "Account #";
            accountNumCell.Controls.Add(h4);

            var emailCell = new TableHeaderCell();
            HtmlGenericControl h15 = new HtmlGenericControl("h3");
            h15.InnerText = "email";
            emailCell.Controls.Add(h15);

            var userNameCell = new TableHeaderCell();
            HtmlGenericControl h5 = new HtmlGenericControl("h3");
            h5.InnerText = "Username";
            userNameCell.Controls.Add(h5);


            var dateOfSignUpCell = new TableHeaderCell();
            HtmlGenericControl h6 = new HtmlGenericControl("h3");
            h6.InnerText = "Date of Sign Up";
            dateOfSignUpCell.Controls.Add(h6);

            var companyCell = new TableHeaderCell();
            HtmlGenericControl h14 = new HtmlGenericControl("h3");
            h14.InnerText = "Company";
            companyCell.Controls.Add(h14);

            var countryCell = new TableHeaderCell();
            HtmlGenericControl h7 = new HtmlGenericControl("h3");
            h7.InnerText = "Country";
            countryCell.Controls.Add(h7);

            var stateOrProvinceCell = new TableHeaderCell();           
            HtmlGenericControl h8 = new HtmlGenericControl("h3");
            h8.InnerText = "State/Province";
            stateOrProvinceCell.Controls.Add(h8);

            var address = new TableHeaderCell();
            HtmlGenericControl h13 = new HtmlGenericControl("h3");
            h13.InnerText = "address";
            address.Controls.Add(h13);

            var city = new TableHeaderCell();
            HtmlGenericControl h9 = new HtmlGenericControl("h3");
            h9.InnerText = "City";
            city.Controls.Add(h9);

            var zipCell = new TableHeaderCell();
            HtmlGenericControl h10 = new HtmlGenericControl("h3");
            h10.InnerText = "Zip/Postal Code";
            zipCell.Controls.Add(h10);

            var channelCountCell = new TableHeaderCell();
            HtmlGenericControl h11 = new HtmlGenericControl("h3");
            h11.InnerText = "Channels";
            channelCountCell.Controls.Add(h11);

            var isHasBoardCell = new TableHeaderCell();
            HtmlGenericControl h12 = new HtmlGenericControl("h3");
            h12.InnerText = "Board";
            isHasBoardCell.Controls.Add(h12);
            hRow.TableSection = TableRowSection.TableHeader;
            hRow.Cells.Add(fullNameCell);
            hRow.Cells.Add(genderCell);
            hRow.Cells.Add(ageCell);
            hRow.Cells.Add(userNameCell);
            hRow.Cells.Add(accountNumCell);
            hRow.Cells.Add(emailCell);
            hRow.Cells.Add(dateOfSignUpCell);
            hRow.Cells.Add(companyCell);
            hRow.Cells.Add(countryCell);
            hRow.Cells.Add(stateOrProvinceCell);
            hRow.Cells.Add(address);
            hRow.Cells.Add(city);
            hRow.Cells.Add(zipCell);
            hRow.Cells.Add(channelCountCell);
            hRow.Cells.Add(isHasBoardCell);


            table.Rows.Add(hRow);



            for (int i = 0; i < userlist.Count; i++)
            {

                var rowToReneder = new TableRow();
                rowToReneder.TableSection = TableRowSection.TableBody;
                var fullNameCell1 = new TableCell();
                fullNameCell1.Text = userlist[i].FirstName + " " + userlist[i].LastName;
                var genderCell1 = new TableCell() { Text = userlist[i].Gender };
                int age = DateTime.Now.Year - userlist[i].BirthDate.Year;
                var ageCell1 = new TableCell() { Text = age.ToString() };
                var accountNumCell1 = new TableCell() { Text = userlist[i].AccountNumber };
                var emailCell1 = new TableCell();
                HtmlGenericControl aEmail = new HtmlGenericControl("a");
                aEmail.Attributes.Add("href", "#");
                aEmail.Attributes.Add("class", "button-email");
                aEmail.Attributes.Add("title", userlist[i].Email);
                aEmail.InnerText = userlist[i].Email;
                emailCell1.Controls.Add(aEmail);   
                //CheckList check = UserManage.isCheckListExists(userlist[i].userId);
                var userNameCell1 = new TableCell();
                //if (check != null)
                //{
                //    if (check.isHasProfile == true)
                //    {
                //        HtmlGenericControl a = new HtmlGenericControl("a");
                //        a.Attributes.Add("href", "../../board/" + userlist[i].userName);
                //        a.Attributes.Add("target", "_blank");
                //        a.InnerText = userlist[i].userName;
                //        userNameCell1.Controls.Add(a);
                //    }
                //    else
                //    {
                //        userNameCell1.Text = userlist[i].userName ;
                //    }
                //}
                //var companyCell1 = new TableCell() { Text = userlist[i].Company };
                //var dateOfSignUpCell1 = new TableCell() { Text = userlist[i].UserProfileCreatedDate.ToShortDateString() };
                //var countryCell1 = new TableCell() { Text = userlist[i].Country };
                //var address1 = new TableCell() { Text = userlist[i].Address };
            
                //List<ChannelTube> channelTubelist = ChannelManage.GetChannelTubesForUser(userlist[i].UserId);
                //var channelCountCell1 = new TableCell() { Text = channelTubelist.Count.ToString() };
               
                //var isHasBoardCell1 = new TableCell();
                //if (check != null)
                //{
                //    if (check.isHasProfile == true)
                //    {
                       
                //        isHasBoardCell1.Text = "yes";
                //    }
                //    else
                //    {
                //        isHasBoardCell1.Text = "no";
                //    }
                //}
                //else
                //{
                //    isHasBoardCell1.Text = "now";
                //}
               // rowToReneder.Cells.Add(fullNameCell1);
               // rowToReneder.Cells.Add(genderCell1);
               // rowToReneder.Cells.Add(ageCell1);
               // rowToReneder.Cells.Add(userNameCell1);
               // rowToReneder.Cells.Add(accountNumCell1);
               // rowToReneder.Cells.Add(emailCell1);
               // rowToReneder.Cells.Add(dateOfSignUpCell1);
               // rowToReneder.Cells.Add(companyCell1);
               // rowToReneder.Cells.Add(countryCell1);
               ////rowToReneder.Cells.Add(stateOrProvinceCell1);
               // rowToReneder.Cells.Add(address1);
               // //rowToReneder.Cells.Add(city1);
               //// rowToReneder.Cells.Add(zipCell1);
               // rowToReneder.Cells.Add(channelCountCell1);
               // rowToReneder.Cells.Add(isHasBoardCell1);
               // table.Rows.Add(rowToReneder);
            }
            usersTableHolder.Controls.Add(table);
          
           
          //  ScriptManager.RegisterStartupScript(usersTableHolder, usersTableHolder.GetType(), "TableUp()", "TableUp()", true);
        }
        private void AddUsersStatistics(Repeater repeater, List<User>userList)
        {
            
        }
    }
}