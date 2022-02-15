﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CampWebsite.Models;
using System.Web.Security;
using CampWebsite.ViewModels;
using System.IO;

namespace CampWebsite.Controllers
{
    public class MemberController : Controller
    {
        dbCampEntities db = new dbCampEntities();
        /// <summary>
        /// 註冊會員
        /// </summary>
        public ActionResult Register()
        {
            RegisterViewModel newMember = new RegisterViewModel();
            return View(newMember);
        }
        [HttpPost]
        public ActionResult Register( RegisterViewModel newMember )
        {
            //string fName, string fEmail, string fPassword
            if (ModelState.IsValid == false)
            {
                return View();
            }
            var member = db.tMember.Where(i => i.fEmail == newMember.fEmail).FirstOrDefault();
            if (member == null)
            {
                try
                {
                    tMember newUser = new tMember();
                    newUser.fName = newMember.fName;
                    newUser.fEmail = newMember.fEmail;
                    newUser.fPassword = newMember.fPassword;
                    newUser.fSex = 0;
                    newUser.fGroup = "gCustomer";
                    newUser.fVerified = false;
                    newUser.fVerified = false;
                    db.tMember.Add(newUser);
                    db.SaveChanges();
                    string userData = (newUser.fGroup).ToString() + "," + newUser.fName;
                    string userID = (newUser.fMemberID).ToString();
                    new CAuthenticationFactory().SetAuthenTicket(userData, userID);
                    return RedirectToAction("List");
                }
                catch
                {
                    ViewBag.Message = "Something wrong!";
                    return View();  //在這邊返回View
                }                
            }
            ViewBag.Message = "此Email帳號已被註冊";
            return View();
        }
        // ---

        // 登入會員
        public ActionResult Login()
        {
            //得到原先頁面的完整URL
            string previousUrl = (Request.UrlReferrer == null) ? "" : Request.UrlReferrer.ToString();
            ViewData["returnUrl"] = previousUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string fEmail, string fPassword, string returnUrl)
        {
            var member = db.tMember.Where(i => i.fEmail == fEmail && i.fPassword == fPassword).FirstOrDefault();
            //if member is null,表示沒註冊
            if (member == null)
            {
                ViewBag.Message = "帳號密碼有誤" + "\n我輸入: " + fEmail + " 密碼: " + fPassword;
                ViewData["returnUrl"] = returnUrl;
                return View();
            }
            string userData = (member.fGroup).ToString() + "," + member.fName;
            string userID = (member.fMemberID).ToString();
            new CAuthenticationFactory().SetAuthenTicket(userData, userID);
            //另一種驗證方式FormsAuthentication.RedirectFromLoginPage(member.Email, true);
            return Redirect(returnUrl);
        }

        //修改個人資料
        [Authorize]
        public ActionResult personalProfile()
        {
            int myID = Convert.ToInt32(User.Identity.Name);
            var member = db.tMember.Where(i => i.fMemberID == myID).FirstOrDefault();
            if(member.fBirthday.HasValue)
            {
                DateTime myBirthday = member.fBirthday.HasValue ? member.fBirthday.Value : DateTime.MinValue;
                ViewBag.Birthday = myBirthday.ToString("yyyy-MM-dd");
            }
            //if (String.IsNullOrEmpty(member.fPhoto))
            //{
            //    ViewBag.Photo = "/Images/Members/default.jpg";
            //}
            //else
            //{
            //    ViewBag.Photo = member.fPhoto;
            //}
            
            return View(member);
        }
        [HttpPost]
        public ActionResult personalProfile(tMember viewMember)
        {
            int myID = Convert.ToInt32(User.Identity.Name);
            var editMember = db.tMember.Where(i => i.fMemberID == myID).FirstOrDefault();            
            editMember.fName = viewMember.fName;
            editMember.fPassword = viewMember.fPassword;
            editMember.fPhoneNumber = viewMember.fPhoneNumber;
            editMember.fSex = viewMember.fSex;
            editMember.fBirthday = viewMember.fBirthday;
            db.SaveChanges();
            //更新身分憑證
            string userData = (editMember.fGroup).ToString() + "," + editMember.fName;
            string userID = (editMember.fMemberID).ToString();
            new CAuthenticationFactory().SetAuthenTicket(userData, userID);
            return RedirectToAction("List", "Member");
        }
        /// <summary>
        /// 上傳會員圖片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult uploadPersonalPic() 
        {
            string path = Server.MapPath("~/Images/Members/");
            //## 如果有任何檔案類型才做
            if (Request.Files.AllKeys.Any())
            {
                //## 讀取指定的上傳檔案ID
                var httpPostedFile = Request.Files["UploadedImage"];
                int myID = Convert.ToInt32(User.Identity.Name);
                //## 真實有檔案，進行上傳
                if (httpPostedFile != null && httpPostedFile.ContentLength != 0)
                {
                    httpPostedFile.SaveAs(path + Path.GetFileName("user"+myID+".jpg"));
                    var editMember = db.tMember.Where(i => i.fMemberID == myID).FirstOrDefault();
                    editMember.fPhoto = "/Images/Members/user" + myID + ".jpg";
                    db.SaveChanges();
                }
            }
            return Json(new { isUploaded = true, result = "成功囉" }, "text/html");
        }


        // 顯示所有會員-限開發用
        public ActionResult List()
        {
            var members = db.tMember.OrderBy(m => m.fMemberID).ToList();
            return View(members);
        }

        //登出
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return Redirect("/Home/Index");
        }
        //身分群組測試頁面
        [Authorize]
        [Authorize(Roles = "gVendor")]
        public ActionResult forGroup3()
        {
            return View();
        }
        [Authorize(Roles = "gCustomer")]
        public ActionResult forGroup2()
        {
            return View();
        }
    }
}