using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemKurierskiPracaInzynierska.Core.Containers
{
    public class Province
    {
        [Key]
        public int idProvince { get; set; }
        public string ProvinceName { get; set; }
   
    }
}
