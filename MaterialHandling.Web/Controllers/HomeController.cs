using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaterialHandling.Data;

namespace MaterialHandling.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        /*This will show the data in the database
         * ordered by PartNum
         * if there is a string from the searchbox
         * this will show all PartNums that contain
         * the given string*/
        
        public ActionResult Index(string PartString, string AisleString)
        {        
            /* By default sort by PartNumber, then Type*/
            var model = Database.Materials.OrderBy( m => m.Time).OrderBy(m => m.Type).ToList();

            /* If PartNum has value and Aisle has value, then sort by PartNum */
            if ((!string.IsNullOrEmpty(PartString)) && !string.IsNullOrEmpty(AisleString))
            {
                model = Database.Materials.Where(s => s.PartNum.Contains(PartString)).OrderBy(m => m.PartNum).OrderByDescending( m => m.Depth).OrderBy( m => m.Type).ToList();
            }

            /* If PartNum has value and Aisle is null or empty, then sort by PartNum*/
            if ((!string.IsNullOrEmpty(PartString)) && string.IsNullOrEmpty(AisleString))
            {
                model = Database.Materials.Where(s => s.PartNum.Contains(PartString)).OrderBy(m => m.PartNum).OrderByDescending(m => m.Depth).OrderBy(m => m.Type).ToList();
            }

            /* If PartNum is null or empty and Aisle has value, then sort by Aisle*/
            if ((string.IsNullOrEmpty(PartString)) && !string.IsNullOrEmpty(AisleString))
            {
                model = Database.Materials.Where(s => s.Aisle == AisleString).OrderByDescending(m => m.Depth).OrderBy(m => m.Type).ToList();
            }

            /*This will return the model from above to the index view*/
            return View(model);
        }

        /*This will create a new instance of Material and go to edit mode*/        
        public ActionResult Create()
        {
            var model = new Material();
            return View("Edit", model);
        }

        /*This will post the data for the new instance of Material including submit time*/        
        [HttpPost]
        public ActionResult Create(Material item)
        {
            if (ModelState.IsValid)
            {
                Database.Materials.Add(item);
                item.Time = DateTime.Now;
                Database.SaveChanges();

                return RedirectToAction("Create");
            }
            else
                return View("Edit", item);
        }

        /*This will show the edit screen for the current id*/        
        public ActionResult Edit(int id)
        {
            var model = Database.Materials.Find(id);
            return View(model);
        }

        /*This will submit the changed data for the current id*/        
        [HttpPost]
        public ActionResult Edit(int id, Material item)
        {
            if (ModelState.IsValid)
            {
                var dbItem = Database.Materials.Find(id);
                TryUpdateModel(dbItem);
                dbItem.Time = DateTime.Now;
                Database.SaveChanges();
                return RedirectToAction("Create");
            }
            else
                return View(item);
        }

        /*This will delete the current item specified by id*/
        /***This is currently not used***/
        public ActionResult Delete(int id)
        {
            var item = Database.Materials.Find(id);
            Database.Materials.Remove(item);
            Database.SaveChanges();
            return RedirectToAction("Index");
        }

       

    }
}