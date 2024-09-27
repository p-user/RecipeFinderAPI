using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationUser :IdentityUser
    {
       
        public DateTime? LastUpdatedDate { get; set; }
        public Status Status { get; set; }
        
    }
}
