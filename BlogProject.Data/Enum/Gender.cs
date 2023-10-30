using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Enum
{
    public enum Gender
    {

        [Display(Name = "Nam")]
        Nam = 0,
        [Display(Name = "Nữ")]
        Nữ = 1,
        [Display(Name = "Không")]
        Không = 2
    }
}
