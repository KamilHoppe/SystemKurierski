using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemKurierskiPracaInzynierska.Core.Containers
{
   public class PersonSenderReceiver
    {
        public Person PersonSender { get; set; }
        public Person PersonReceiver{ get; set; }
        public Order Order { get; set; }
        public Package Package { get; set; }
    }
}
