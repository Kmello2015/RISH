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
    public class SoftwareController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Software
        public async Task<ActionResult> Index()
        {
            var softwares = db.Softwares.Include(s => s.LicenseType).Include(s => s.Provider).Include(s => s.SoftwareArchitecture).Include(s => s.Vendor);
            return View(await softwares.ToListAsync());
        }

        // GET: Software/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Software software = await db.Softwares.FindAsync(id);
            if (software == null)
            {
                return HttpNotFound();
            }
            return View(software);
        }

        // GET: Software/Create
        public ActionResult Create()
        {
            ViewBag.LicenseTypeId = new SelectList(db.LicenseTypes, "Id", "Name");
            ViewBag.ProviderId = new SelectList(db.Providers, "Id", "RUT");
            ViewBag.SoftwareArchitectureId = new SelectList(db.SoftwareArchitectures, "Id", "Name");
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name");
            return View();
        }

        // POST: Software/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,VendorId,LicenseTypeId,SoftwareArchitectureId,ProviderId,PurchaseDate,InvoiceNumber,InvoiceDate")] Software software)
        {
            if (ModelState.IsValid)
            {
                db.Softwares.Add(software);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LicenseTypeId = new SelectList(db.LicenseTypes, "Id", "Name", software.LicenseTypeId);
            ViewBag.ProviderId = new SelectList(db.Providers, "Id", "RUT", software.ProviderId);
            ViewBag.SoftwareArchitectureId = new SelectList(db.SoftwareArchitectures, "Id", "Name", software.SoftwareArchitectureId);
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name", software.VendorId);
            return View(software);
        }

        // GET: Software/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Software software = await db.Softwares.FindAsync(id);
            if (software == null)
            {
                return HttpNotFound();
            }
            ViewBag.LicenseTypeId = new SelectList(db.LicenseTypes, "Id", "Name", software.LicenseTypeId);
            ViewBag.ProviderId = new SelectList(db.Providers, "Id", "RUT", software.ProviderId);
            ViewBag.SoftwareArchitectureId = new SelectList(db.SoftwareArchitectures, "Id", "Name", software.SoftwareArchitectureId);
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name", software.VendorId);
            return View(software);
        }

        // POST: Software/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,VendorId,LicenseTypeId,SoftwareArchitectureId,ProviderId,PurchaseDate,InvoiceNumber,InvoiceDate")] Software software)
        {
            if (ModelState.IsValid)
            {
                db.Entry(software).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LicenseTypeId = new SelectList(db.LicenseTypes, "Id", "Name", software.LicenseTypeId);
            ViewBag.ProviderId = new SelectList(db.Providers, "Id", "RUT", software.ProviderId);
            ViewBag.SoftwareArchitectureId = new SelectList(db.SoftwareArchitectures, "Id", "Name", software.SoftwareArchitectureId);
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name", software.VendorId);
            return View(software);
        }

        // GET: Software/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Software software = await db.Softwares.FindAsync(id);
            if (software == null)
            {
                return HttpNotFound();
            }
            return View(software);
        }

        // POST: Software/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Software software = await db.Softwares.FindAsync(id);
            db.Softwares.Remove(software);
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
