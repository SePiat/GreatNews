using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatNews.Models
{
    public class UserDB : DataBaseEntity
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public News news { get; set; }
        
    }
}
