using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Schedule 
    {
        public int Id { get; set; }
        public IContent Content { get; set; }
      
       public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
      
       

    }
}
