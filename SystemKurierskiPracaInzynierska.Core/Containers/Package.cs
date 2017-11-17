using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemKurierskiPracaInzynierska.Core.Containers
{
    public class Package
    {
        [Key]
        public int idPackage { get; set; }
        public string PackageType { get; set; }
        public byte SizeX { get; set; }
        public byte SizeY { get; set; }
        public byte SizeZ { get; set; }
        public byte Weight { get; set; }
        public byte Price { get; set; }
        public byte insurance { get; set; }

    }
}
