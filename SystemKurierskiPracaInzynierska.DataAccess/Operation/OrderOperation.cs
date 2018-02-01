using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SystemKurierskiPracaInzynierska.DataAccess.Context;
using SystemKurierskiPracaInzynierska.Core.Containers;


namespace SystemKurierskiPracaInzynierska.DataAccess.Operation
{
  public  class OrderOperation
    {
        public Order getOrder(int idOrder)
        {
            Order order = new Order();
           
            AppCourierContext db = new AppCourierContext();
            var result = from o in db.Orders
                         join s in db.Status on o.Status_idStatus equals s.idStatus
                         join p in db.Packages on o.Package_idPackage equals p.idPackage                      
                         join pp in db.Person on o.idPersonReceiver equals pp.idPerson
                         join cc in db.Cities on pp.idCity equals cc.idCity
                         where o.idOrder == idOrder
                         select new{
                                 o,s,p,pp,cc

                         };
            foreach (var item in result)
            {
               order = item.o;
               order.Status = item.s;
               order.Package = item.p;
               order.Person = item.pp;
               order.Person.City = item.cc;
            }
            return order;
        }

        public List<Markers> getLocation(string id)  //Get package per Courier
        {     
            List<Markers> markers = new List<Markers>();
            using (var db = new AppCourierContext())
            {
                //var result = db.Orders.Where(o=>o.idCourier==id
                //                && o.Status_idStatus == 1);
                var result = from o in db.Orders
                             join pp in db.Person on o.idPersonReceiver equals pp.idPerson
                             join cc in db.Cities on o.Person.idCity equals cc.idCity
                             where o.idCourier == id && (o.Status_idStatus == 1 || o.Status.idStatus==2)
                             select new
                             {
                                 o,
                                 pp,
                                 cc

                             };
                foreach (var item in result)
                {
                    Markers marker = new Markers();
                    
                    marker.lat = item.o.Latitude;
                    marker.lng = item.o.Longitude;
                    if (item.pp.ApartmentNumber != null)
                    {
                        marker.description = item.pp.Name + " " + item.pp.Surname + " " + 
                        item.pp.Street + " " + 
                        item.pp.HouseNumber + "/" + item.pp.ApartmentNumber+" Phone: "+item.pp.PhoneNumber;
                    }
                    else
                    {
                        marker.description = item.pp.Name + " " + item.pp.Surname + " " +
                        item.pp.Street + " " +
                        item.pp.HouseNumber + " Phone: " + item.pp.PhoneNumber;
                    }
                    markers.Add(marker);
                }

            }
            return markers;
    }

        public void ChangeStatusToDelivered(string lat,string lng) // if status =3 and currier click 'submit' on marker then package deliveried
        {

            var db = new AppCourierContext();

            var cust = (from c in db.Orders  where c.Latitude == lat && c.Longitude == lng
                        select c).First();

            cust.Status_idStatus = 3;
            cust.ModificationOrderDate = System.Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cust.OrderCollectionDate = System.Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            db.SaveChanges();

        }


        public void AddOrder(int idPackage, string EmailReceiver, string EmailSender,string latitude,string longitude,string idCourier,
            string idDispatcher)
        {
            Order order = new Order();
            int idReceiver = 0;
            int idSender = 0;

            using (var db = new AppCourierContext())
            {
                var Receiverresult = from p in db.Person
                                     where p.Email == EmailReceiver
                                     select p;

                foreach (var item in Receiverresult)
                {
                    idReceiver = item.idPerson;
                }

                var Senderresult = from p in db.Person
                                   where p.Email == EmailSender
                                   select p;

                foreach (var item in Senderresult)
                {
                    idSender = item.idPerson;
                }
                order.OrderDate = System.Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                order.idPersonReceiver = idReceiver;
                order.idPersonSender = idSender;
                order.Status_idStatus = 1;
                order.Package_idPackage = idPackage;
                order.Latitude = latitude;
                order.Longitude = longitude;
                order.idCourier = idCourier;
                order.idDispatcher = idDispatcher;
                db.Orders.Add(order);
                db.SaveChanges();
            }


        }


    }
}
