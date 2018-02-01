using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemKurierskiPracaInzynierska.Core.Containers
{
    public class City
    {
       
        [Key]
        [Required(ErrorMessage = "City is required")]
        public int idCity { get; set; }
        public string CityName { get; set; }
        [ForeignKey("Province")]
        public int idProvince { get; set; }
        public Province Province { get; set; }

    }
}
