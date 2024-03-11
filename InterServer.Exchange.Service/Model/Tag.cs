using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterServer.Exchange.Service.Model
{
    public class Tag 
    {
        public int devId { get; set; }
        public string tagName { get; set; }
        public double tagValue { get; set; }
        public DateTime readTime { get; set; }


    }
}
