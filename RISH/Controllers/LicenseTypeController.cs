using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RISH.Models;

namespace RISH.Controllers
{
    public class LicenseTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LicenseType
        public async Task<ActionResult> Index()
        {
            return View(await db.LicenseTypes.ToListAsync());
        }

        // GET: LicenseType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LicenseType licenseType = await db.LicenseTypes.FindAsync(id);
            if (licenseType == null)
            {
                return HttpNotFound();
            }
            return View(licenseType);
        }

        // GET: LicenseType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LicenseType/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] LicenseType licenseType)
        {
            if (ModelState.IsValid)
            {
                db.LicenseTypes.Add(licenseType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(licenseType);
        }

        // GET: LicenseType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LicenseType licenseType = await db.LicenseTypes.FindAsync(id);
            if (licenseType == null)
            {
                return HttpNotFound();
            }
            return View(licenseType);
        }

        // POST: LicenseType/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] LicenseType licenseType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(licenseType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(licenseType);
        }

        // GET: LicenseType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LicenseType licenseType = await db.LicenseTypes.FindAsync(id);
            if (licenseType == null)
            {
                return HttpNotFound();
            }
            return View(licenseType);
        }

        // POST: LicenseType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LicenseType licenseType = await db.LicenseTypes.FindAsync(id);
            db.LicenseTypes.Remove(licenseType);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
