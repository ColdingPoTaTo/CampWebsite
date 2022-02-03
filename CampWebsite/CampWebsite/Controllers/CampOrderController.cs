using CampWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampWebsite.Controllers
{
    public class CampOrderController : Controller
    {
        dbCampEntities db = new dbCampEntities();
        // GET: CampOrder
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult GenerateOrder(int TentID)
        {
            tOrder newOrder = new tOrder();
            DateTime CheckinDate = DateTime.Today;
            newOrder.fTentID = TentID;
            newOrder.fMemberID = Convert.ToInt32(User.Identity.Name);
            newOrder.fCheckinDate = CheckinDate;
            return View(newOrder);
        }
    }
}