using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StrimmDAL
{
   public class Country
    {
       [Key]
       public int countryId { get; set; }
       public string country { get; set; }
    }
}
