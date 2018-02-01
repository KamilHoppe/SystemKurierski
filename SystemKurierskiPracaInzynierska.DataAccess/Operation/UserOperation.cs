using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemKurierskiPracaInzynierska.Core.Containers;
using SystemKurierskiPracaInzynierska.DataAccess.Context;

namespace SystemKurierskiPracaInzynierska.DataAccess.Operation
{
   public class UserOperation
    {
        public void UpdateLocation(int getIdOrder, string lat,string lng) //Change location per User
        {
            AppCourierContext db = new AppCourierContext();
            var query = from ord in db.Orders
                        where ord.idOrder == getIdOrder
                        select ord;
            foreach (Order ord in query)
            {
                ord.Latitude = lat;
                ord.Longitude = lng;
            }
            db.SaveChanges();

        }
    }
}
