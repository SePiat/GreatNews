using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatNews.Models
{
    public class Comment : DataBaseEntity
    {
        //public int Id { get; set; }
        //public DateTime Date { get; set; }
        public string Comment_ { get; set; }
        //public Users Users { get; set; }
        //public int UsersId { get; set; }

        public News News { get; set; }
        public int NewsId { get; set; }
    }
}
