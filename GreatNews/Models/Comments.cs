using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatNews.Models
{
    public class Comments
    {
        public int Id { get; set; }
        //public DateTime Date { get; set; }
        public string Comment { get; set; }
        //public Users Users { get; set; }
        //public int UsersId { get; set; }

        public News News { get; set; }
        public int NewsId { get; set; }
    }
}
