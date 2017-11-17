using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemKurierskiPracaInzynierska.Core.Containers
{
    public class Status
    {
        [Key]
        public int idStatus { get; set; }
        public string StatusName { get; set; }
    }
}
