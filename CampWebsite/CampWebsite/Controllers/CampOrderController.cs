using CampWebsite.Models;
using CampWebsite.Models.ViewModel;
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
            PreOrderInfoViewModel newOrder = new PreOrderInfoViewModel();            
            newOrder.tTent = db.tTent.Where(t => t.fTentID == TentID).FirstOrDefault();
            int userID = Convert.ToInt32(User.Identity.Name);
            newOrder.tMember = db.tMember.Where(m => m.fMemberID == userID).FirstOrDefault();
            DateTime CheckinDate = DateTime.Today;
            newOrder.fCheckinDate = CheckinDate;
            return View(newOrder);
        }
    }
}