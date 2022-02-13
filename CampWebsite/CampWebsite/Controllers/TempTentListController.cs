﻿using CampWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampWebsite.Controllers
{
    public class TempTentListController : Controller
    {
        dbCampEntities db = new dbCampEntities();
        // GET: TempTentList
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListTent()
        {
            var tent = db.tTent.OrderBy(t => t.fTentID).ToList();
            return View(tent);
        }

        public ActionResult ListOrder()
        {
            var order = db.tOrder.OrderBy(t => t.fOrderID).ToList();
            return View(order);
        }
    }
}