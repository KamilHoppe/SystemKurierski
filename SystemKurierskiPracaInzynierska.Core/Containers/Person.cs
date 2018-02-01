using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemKurierskiPracaInzynierska.Core.Containers
{
    public class Person
    {
        [Key]
        public int idPerson { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "PostCode is required")]
        [RegularExpression(@"^\d{2}-\d{3}?$", ErrorMessage = "Invalid PostCode")]
        public string PostCode { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^\d{9}?$", ErrorMessage = "Invalid PhoneNumber")]
        public long PhoneNumber { get; set; }
        [Required(ErrorMessage = "House Number is required")]
        public byte HouseNumber { get; set; }
        public byte? ApartmentNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [ForeignKey("City")]
        [Required(ErrorMessage = "City is required")]
        public int idCity { get; set; }
        public City City { get; set; }
        
    }
 
}
