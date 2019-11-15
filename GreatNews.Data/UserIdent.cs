using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GreatNews.Models
{
    public class UserIdent : IdentityUser
    {
        public int Year { get; set; }
    }
}
