using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemKurierskiPracaInzynierska.Core.Containers
{
    public class Order
    {
        [Key]
        [Required(ErrorMessage = "Number Order is required")]
        public int idOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ModificationOrderDate { get; set; }
        public DateTime? OrderCollectionDate { get; set; }
        [ForeignKey("Status")]
        public int Status_idStatus { get; set; }
        public Status Status { get; set; }
        [ForeignKey("Package")]
        public int Package_idPackage { get; set; }
        public Package Package { get; set; }
        [ForeignKey("Person")]
        public int idPersonSender { get; set; }
        public int idPersonReceiver { get; set; }
        public Person Person { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        [Required(ErrorMessage = "Courier is required")]
        public string idCourier { get; set; }
        public string idDispatcher { get; set; }
    }
}
