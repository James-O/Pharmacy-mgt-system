using PharmacyMgtSys.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyMgtSys.Controllers
{
    public class UsersController : Controller
    {
       private InventoryMgtSysEntities db = new InventoryMgtSysEntities();
        // GET: Users
        public ActionResult GetUsers()//This method list all users from db
        {
            var ListAll = db.Users.ToList();
            return View(ListAll);
        }
        // GET: Users
        public ActionResult Create()//This method create new user in the db
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(int Roleid, string Fname, string Lname, string Email, string Password, string Address, bool Isdeleted, bool Isactive)
        {
            var users = new User()
            {
                RoleId = Roleid, FirstName = Fname, LastName = Lname, Email = Email,
                Password = Password, Address = Address, Isdeleted = Isdeleted, IsActive = Isactive
            };
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                //if (users.Id > 0) { ViewBag.Success = "User inserted successfully"; }
                //ModelState.Clear();
            }
            return RedirectToAction("GetUsers");
        }
        public ActionResult Edit(Int32 id)
        {
            var edit = db.Users.Where(s => s.Id == id).FirstOrDefault();
            if(edit != null)
            {
                TempData["Id"] = id;
                TempData.Keep();
                return View(edit);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            Int32 edit = (int)TempData["Id"];
            var edituser = db.Users.Where(s => s.Id == edit).FirstOrDefault();
            if(edituser != null)
            {
                edituser.FirstName = user.FirstName;
                edituser.LastName = user.LastName;
                edituser.Email = user.Email;
                edituser.Address = user.Address;
                edituser.IsActive = user.IsActive;
                edituser.Isdeleted = user.Isdeleted;

                db.Entry(edituser).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Success = "User updated successfully";

            }
            return RedirectToAction("GetUsers");
        }

        //public ActionResult Delete(int id)
        //{
        //    if (id > 0)
        //    {
        //        var delete = db.Users.Where(s => s.Id == id).FirstOrDefault();
        //        if(delete != null)
        //        {
        //            db.Entry(delete).State = EntityState.Deleted;
        //            db.SaveChanges();
        //            return View(delete);
        //        }

        //    }
        //    return RedirectToAction("GetUsers");
        //}

        public ActionResult Delete(int id)
        {
            var data = db.Users.FirstOrDefault(a => a.Id == id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)//Studentid
        {
            //var data = db.Users.FirstOrDefault(a => a.Id == Studentid);
            //if (data != null)
            //{
            //    db.Users.Remove(data);
            //    db.SaveChanges();
            //    return RedirectToAction("GetUsers");
            //}
            //else
            //    return View();
            db.Users.Remove(db.Users.Find(id));
            db.SaveChanges();
            return RedirectToAction("GetUsers");

        }
    }
}