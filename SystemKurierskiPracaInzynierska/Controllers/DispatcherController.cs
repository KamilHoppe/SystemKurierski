using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
using SystemKurierskiPracaInzynierska.Core.Containers;
using SystemKurierskiPracaInzynierska.DataAccess.Context;
using SystemKurierskiPracaInzynierska.DataAccess.Operation;
using SystemKurierskiPracaInzynierska.Domain.BusinesLogic;

namespace SystemKurierskiPracaInzynierska.Controllers
{
    public class DispatcherController : Controller
    {
        [Authorize(Roles = "Dispatcher")]
        public ActionResult Index() // load all dropdownlist
        {
            AppCourierContext dbcontext = new AppCourierContext();

            var citylist = dbcontext.Cities.OrderBy(r => r.CityName).ToList().Select(rr =>
            new SelectListItem { Value = rr.idCity.ToString(), Text = rr.CityName }).ToList();
            ViewBag.city = dbcontext.Cities.ToList();

            var packagetypelist = dbcontext.Packages.OrderBy(r => r.PackageType).ToList().Select(rr =>
            new SelectListItem { Value = rr.idPackage.ToString(), Text = rr.PackageType }).ToList();
            ViewBag.package = dbcontext.Packages.ToList();

            var context = new Models.ApplicationDbContext();
            string roleId = "8064ad2d-1e93-42db-9eb4-8908546e8d42";
            var user = context.Users.Include(u => u.Roles).Where(u => u.Roles.Any(r => r.RoleId == roleId)).Select(uu=>
            new SelectListItem { Value = uu.Id.ToString(), Text = uu.FirstName+" "+uu.LastName}).ToList();
            ViewBag.Users = user;

            return View();
        }

        [HttpPost]
        public ActionResult Index(PersonSenderReceiver person,PersonSenderReceiver p) //Controller who add personreceiver and personsender to database per dispatcher
        {
            DispatcherAction dispatcherAction = new DispatcherAction();
            OrderAction orderAction = new OrderAction();
            int IdPackage = p.Package.idPackage;
            string EmailReceiver = person.PersonReceiver.Email;
            string EmailSender = person.PersonSender.Email;
            string idCourier = person.Order.idCourier;
            var dispatcherID = User.Identity.GetUserId();
          
            if (!ModelState.IsValid)
            {
                AppCourierContext dbcontext = new AppCourierContext();

                var citylist = dbcontext.Cities.OrderBy(r => r.CityName).ToList().Select(rr =>
                new SelectListItem { Value = rr.idCity.ToString(), Text = rr.CityName }).ToList();
                ViewBag.city = dbcontext.Cities.ToList();

                var packagetypelist = dbcontext.Packages.OrderBy(r => r.PackageType).ToList().Select(rr =>
                new SelectListItem { Value = rr.idPackage.ToString(), Text = rr.PackageType }).ToList();
                ViewBag.package = dbcontext.Packages.ToList();

                var context = new Models.ApplicationDbContext();
                string roleId = "8064ad2d-1e93-42db-9eb4-8908546e8d42";
                var user = context.Users.Include(u => u.Roles).Where(u => u.Roles.Any(r => r.RoleId == roleId)).Select(uu =>
                new SelectListItem { Value = uu.Id.ToString(), Text = uu.FirstName + " " + uu.LastName }).ToList();
                ViewBag.Users = user;

                return View();
            }

            int idCity = person.PersonReceiver.idCity;
            var results = dispatcherAction.NameCity(idCity);
            string name = results.CityName;


            var address = name+", "+person.PersonReceiver.Street + " " + person.PersonReceiver.HouseNumber;
            var requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&key=AIzaSyAWOeHV0GwBAdUD7aBSGO1mgaKNlnwvGh4", Uri.EscapeDataString(address));

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());

            var result = xdoc.Element("GeocodeResponse").Element("result");
            var locationElement = result.Element("geometry").Element("location");
            var lat = locationElement.Element("lat");
            var lng = locationElement.Element("lng");
            String lngstring = lng.ToString();
            String latstring = lat.ToString();

            String resultStringlng = System.Text.RegularExpressions.Regex.Match(lngstring, @"\d+(\.\d+)").Value;
            String resultStringlat = System.Text.RegularExpressions.Regex.Match(latstring, @"\d+(\.\d+)").Value;

            float latitude = float.Parse(resultStringlat, CultureInfo.InvariantCulture);
            float longitude = float.Parse(resultStringlng, CultureInfo.InvariantCulture);

            dispatcherAction.AddPerson(person.PersonReceiver);
            dispatcherAction.AddPerson(person.PersonSender);

            orderAction.AddOrder(IdPackage, EmailReceiver, EmailSender, resultStringlat, resultStringlng,idCourier,dispatcherID);

            return RedirectToAction("ListOrder");

     }
        [Authorize(Roles = "Dispatcher")]
        public ActionResult ListOrder() //List Order for Dispatcher
        {
            DispatcherOperation dispatcherOperation = new DispatcherOperation();
            var dispatcherID = User.Identity.GetUserId();
            var list =dispatcherOperation.getListOrder(dispatcherID);
            return View(list);
        }
        [HttpGet]
        [Authorize(Roles = "Dispatcher")]
        public ActionResult EditStatus(int id = 0) //Get id for select package
        {
            AppCourierContext dbcontext = new AppCourierContext();
            Order order = dbcontext.Orders.Find(id);
            var resultStatus = dbcontext.Status.OrderBy(s => s.StatusName).ToList().Select(ss =>
            new SelectListItem { Value = ss.idStatus.ToString(), Text = ss.StatusName }).ToList();
            ViewBag.StatusList = dbcontext.Status.ToList();
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = id;
            return View(order);
        }
        [HttpPost]
        public ActionResult EditStatus(Order order,int id) //Edit Status Package per Dispatcher
        {
            int Status = order.Status_idStatus; 
            AppCourierContext dbcontext = new AppCourierContext();
            if (!ModelState.IsValid)
            {
                DispatcherOperation dispatcherOperation = new DispatcherOperation();
                dispatcherOperation.UpdateStatus(id, Status);
                return RedirectToAction("ListOrder");
            }
            else
            {
                return RedirectToAction("ListOrder");
            }
        }
    }
}