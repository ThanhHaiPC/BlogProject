using BlogProject.Data.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.System.Users
{
    public class UserVm
    {
        public Guid Id { get; set; }

      
        public string? FirstName { get; set; }


        public string? LastName { get; set; }

   
        public string? PhoneNumber { get; set; }

 
        public string? UserName { get; set; }

        public string? Email { get; set; }

 
        public Gender? Gender { get; set; }


        public DateTime? DateOfBir { get; set; }
        public string? Address { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
