using PharmacyMgtSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyMgtSys.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string password)
        {
            using (InventoryMgtSysEntities db = new InventoryMgtSysEntities())
            {
                var user = db.Users.FirstOrDefault(m => m.Email == Email && m.Password == password);
                if (user == null)
                {
                    return View();
                }
                else
                {
                    var del = db.Users;

                    Session["TotalNumberOfUsers"]= del.Where(x => x.Isdeleted == false).Count();
                    
                    Session["TotalNumberOfDeletedUser"] = del.Where(x => x.Isdeleted == true).Count();
                    
                    //Session["Isdeleted"] = del.Count(a => a.Isdeleted == false);
                    
                    //Session["deleted"] = db.Users.Count(a => a.Isdeleted == true);

                    Session["FirstName"] = user.FirstName.ToString();
                    Session["LastName"] = user.LastName.ToString();
                    Session["Email"] = user.Email.ToString();
                    Session["Address"] = user.Address.ToString();
                    Session["RoleId"] = user.RoleId.ToString();
                    Session["IsActive"] = user.IsActive == true ? "Yes" : "No";
                    Session["LastLoginDate"] = user.LastLoginDate;
                    user.LastLoginDate = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Dashboard");
                }
            }
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}