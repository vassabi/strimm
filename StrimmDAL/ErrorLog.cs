using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmDAL
{
   public class ErrorLog
    {
       [Key]
       public int errorId { get; set; }
       public DateTime dateStamp { get; set; }
       public string errorMessage { get; set; }
       public string errorUrl { get; set; }
       public string userIP { get; set; }
       public bool isChecked { get; set; }
      

    }
}
