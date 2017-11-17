using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using System.Xml.Linq;
using SystemKurierskiPracaInzynierska.Core.Containers;
using SystemKurierskiPracaInzynierska.DataAccess.Context;
using SystemKurierskiPracaInzynierska.DataAccess.Operation;

namespace SystemKurierskiPracaInzynierska.Controllers
{
    public class CourierController : Controller
    {
        [Authorize(Roles = "Courier")]
        public ActionResult Index()
        {
            
            OrderOperation orderOperation = new OrderOperation();
            var userID = User.Identity.GetUserId();
            var lista = orderOperation.getLocation(userID);
            return View(lista);

        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            OrderOperation orderOperation = new OrderOperation();

            string txtValue = formCollection["LatAndLng"];
            string[] words = txtValue.Split(' ');
            string lat = words[0];
            string lng = words[1];
            orderOperation.ChangeStatusToDelivered(lat, lng);
            var userID = User.Identity.GetUserId();
            var lista = orderOperation.getLocation(userID);

            return View(lista);

        }
        [Authorize(Roles = "Courier")]
        public ActionResult RoutePackage()
        {
            OrderOperation orderOperation = new OrderOperation();
            var userID = User.Identity.GetUserId();
            var lista = orderOperation.getLocation(userID);
            return View(lista);

        }

    }
}