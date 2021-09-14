using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmDAL
{
  public  class ReservedNames
    {
      [Key]
      public int reservedNameId { get; set; }
      public string reservedName { get; set; }
    }
}
