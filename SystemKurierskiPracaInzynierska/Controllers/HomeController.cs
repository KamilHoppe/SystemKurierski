using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemKurierskiPracaInzynierska.Core.Containers;
using SystemKurierskiPracaInzynierska.DataAccess.Context;
using SystemKurierskiPracaInzynierska.DataAccess.Operation;
using SystemKurierskiPracaInzynierska.Domain.BusinesLogic;

namespace SystemKurierskiPracaInzynierska.Controllers
{

    public class HomeController : Controller
    {

        public ActionResult Index() //load layout for clients
        {
            if (User.IsInRole("Courier"))
            {
                return RedirectToAction("Index", "Courier");
            }
            if (User.IsInRole("Dispatcher"))
            {
                return RedirectToAction("Index", "Dispatcher");
            }
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Roles");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(Order order) //get order for user
        {
            AppCourierContext dbcontext = new AppCourierContext();
            bool ifExists = dbcontext.Orders.Any(x => x.idOrder == order.idOrder);
            if (ifExists)
            {
                OrderAction orderAction = new OrderAction();
                return View(orderAction.GetOrder(order.idOrder));
            }
            else
            {
                ModelState.AddModelError("idOrder", "Number order not found. Please check your order number.");
                return View();
            }
           
        }

        public ActionResult ChangeLocation(int id = 0)
        {
          
            AppCourierContext dbcontext = new AppCourierContext();
            Order order = dbcontext.Orders.Find(id);
          
            if (order == null)
            {
                return HttpNotFound();
            }
            
            return View(order);
        }
        [HttpPost]
        public ActionResult ChangeLocation(int id, Markers marker) // change location for client
        {          
            var lat = marker.lat.Replace(",", ".");
            var lng = marker.lng.Replace(",", ".");
            AppCourierContext dbcontext = new AppCourierContext();
            if (ModelState.IsValid)
            {
                UserOperation userOperation = new UserOperation();
                userOperation.UpdateLocation(id,lat,lng);
                return Json(new { result = "Redirect", url = Url.Action("Index", "Home") });
            }
            else
            {
                return View();
            }
        }

    }
}