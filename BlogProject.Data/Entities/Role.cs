
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
