using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatNews.Models
{
    public class Comment : DataBaseEntity
    {
        
        public DateTime DatePub { get; set; }
        public string TextComment { get; set; }
       
        public UserIdent UsersId { get; set; }

        public News News { get; set; }
        public Guid NewsId { get; set; }
    }
}
