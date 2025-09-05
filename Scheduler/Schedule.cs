using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Schedule 
    {
        public IContent Content { get; set; }
      
       public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
      
       

    }
}
