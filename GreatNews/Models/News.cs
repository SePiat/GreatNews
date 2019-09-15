using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatNews.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Heading { get; set; }
        public DateTime Date { get; set; }
        public int PositiveIndex { get; set; }
        public string Content { get; set; }


    }
}
