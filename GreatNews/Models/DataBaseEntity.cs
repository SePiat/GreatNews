using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatNews.Models
{
    public abstract class DataBaseEntity
    {
        public Guid Id { get; set; }
    }
}
