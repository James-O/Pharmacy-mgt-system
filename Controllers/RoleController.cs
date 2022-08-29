using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacyMgtSys.Models;

namespace PharmacyMgtSys.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            using(InventoryMgtSysEntities db = new InventoryMgtSysEntities())
            {
                var role = db.Roles.ToList();
                return View(role);
            }
            
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string name, string description)
        {
            var role = new Role
            {
                RoleName = name,
                RoleDescription = description
            };
            using(InventoryMgtSysEntities db = new InventoryMgtSysEntities())
            {
                db.Roles.Add(role);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");
            }
           
        }
       
        
        
        
        public ActionResult Delete(int identity)
        {
            using (InventoryMgtSysEntities db = new InventoryMgtSysEntities())
            {
                db.Roles.Remove(db.Roles.Find(identity));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}