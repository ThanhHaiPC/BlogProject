using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Enum
{
    public enum Active
    {
        [Display(Name = "Chưa đánh giá")]
        no = 0,
        [Display(Name = "Đã đánh giá")]
        yes = 1
    }
}
