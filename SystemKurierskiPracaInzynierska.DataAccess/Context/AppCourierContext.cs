using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SystemKurierskiPracaInzynierska.Core.Containers;

namespace SystemKurierskiPracaInzynierska.DataAccess.Context
{

    public class AppCourierContext : DbContext
    {
       
        public DbSet<Person> Person { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Order> Orders { get; set; }


    }


}
