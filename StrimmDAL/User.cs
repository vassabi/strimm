using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace StrimmDAL
{
  public  class User
    {
      [Key]
        public int userId { get; set; }
      // public string name { get; set; }
      public string firstName { get; set; }
      public string lastName { get; set; }
      public string country { get; set; }
      public string street { get; set; }
      public string city { get; set; }
      public string state { get; set; }
      public string phoneNumber { get; set; }
      public DateTime birthDate { get; set; }
      public string recoveryEmail { get; set; }
      //public string newPassword { get; set; }
      public string email { get; set; }
      public string gender { get; set; }
      public string zipCode { get; set; }
      public string password { get; set; }
      //(all statuses should be in table statuses in future database normalisation)
      public string userRegistrationStatus { get; set; }// custom(first registration), broadcaster(second registration)
      public string userStory { get; set; }
      public string accountNumber { get; set; }
      public string company { get; set; }
      public DateTime registrationDate { get; set; }
      public bool? isPayedUser { get; set; }
      public string boardTitle { get; set; }
      public string userName { get; set; }
      public string url { get; set; }
      public bool isAgreeWithTerms { get; set; }
      public string userIp { get; set; }
      public string profileImageUrl { get; set; }
      //temp props
      public int employeeInviteCodeId { get; set; }
         public string adminNotes { get; set; }
         public bool isLocked { get; set; }
    }
}
