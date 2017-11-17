using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemKurierskiPracaInzynierska.Core.Containers;
using System.Data.Entity;
using SystemKurierskiPracaInzynierska.DataAccess.Context;
namespace SystemKurierskiPracaInzynierska.DataAccess.Operation
{
    public class DispatcherOperation
    {
        public void SavePerson(Person person) //Add Person from DispatcherController to DB
        {
            using (var db = new AppCourierContext())
            {
                var result = from p in db.Person
                             where p.Email == person.Email
                             select new
                             {
                                 p
                             };
                int count = result.Count();
                if (count == 0)
                {
                    db.Person.Add(person);
                    db.SaveChanges();
                }
            }
        }

        public City getCityName(int idCity) // get CityName for DispatcherControler to add to DB
        {
            City city = new City();

            string name="";

            AppCourierContext db = new AppCourierContext();
            var result = from c in db.Cities
                         where c.idCity == idCity
                         select c;
            foreach (var item in result)
            {
                name = item.CityName;
            }

            city.CityName = name;

            return city;
        }
        public List<Order> getListOrder(string getIdDispatcher) // get List Order for Dispatcher
        {
            Order order = new Order();
            List<Order> orders = new List<Order>();
            AppCourierContext db = new AppCourierContext();
            var result = from o in db.Orders
                         join s in db.Status on o.Status_idStatus equals s.idStatus
                         join p in db.Packages on o.Package_idPackage equals p.idPackage
                         join pp in db.Person on o.idPersonReceiver equals pp.idPerson
                         join cc in db.Cities on o.Person.idCity equals cc.idCity
                         where o.idDispatcher==getIdDispatcher
                         select new
                         {
                             o,s,p,pp,cc
                         };
            foreach (var item in result)
            {
                order = item.o;
                order.Status = item.s;
                order.Package = item.p;
                order.Person = item.pp;
                order.Person.City = item.cc;
                orders.Add(order);
            }
            return orders;
        }
        public void UpdateStatus(int getIdOrder, int getStatus)
        {
            AppCourierContext db = new AppCourierContext();
            var query = from ord in db.Orders
                        join s in db.Status on ord.Status_idStatus equals s.idStatus
                        join cc in db.Cities on ord.Person.idCity equals cc.idCity
                        join pp in db.Person on ord.idPersonReceiver equals pp.idPerson
                        where ord.idOrder == getIdOrder
                        select ord;    
            foreach (Order ord in query)
            {          
                   ord.Status_idStatus = getStatus;
                   ord.ModificationOrderDate= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            db.SaveChanges();

        }


    }
}
