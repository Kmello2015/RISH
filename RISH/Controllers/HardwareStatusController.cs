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
    public class HardwareStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HardwareStatus
        public async Task<ActionResult> Index()
        {
            return View(await db.HardwareStatuses.ToListAsync());
        }

        // GET: HardwareStatus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HardwareStatus hardwareStatus = await db.HardwareStatuses.FindAsync(id);
            if (hardwareStatus == null)
            {
                return HttpNotFound();
            }
            return View(hardwareStatus);
        }

        // GET: HardwareStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HardwareStatus/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] HardwareStatus hardwareStatus)
        {
            if (ModelState.IsValid)
            {
                db.HardwareStatuses.Add(hardwareStatus);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hardwareStatus);
        }

        // GET: HardwareStatus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HardwareStatus hardwareStatus = await db.HardwareStatuses.FindAsync(id);
            if (hardwareStatus == null)
            {
                return HttpNotFound();
            }
            return View(hardwareStatus);
        }

        // POST: HardwareStatus/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] HardwareStatus hardwareStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hardwareStatus).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hardwareStatus);
        }

        // GET: HardwareStatus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HardwareStatus hardwareStatus = await db.HardwareStatuses.FindAsync(id);
            if (hardwareStatus == null)
            {
                return HttpNotFound();
            }
            return View(hardwareStatus);
        }

        // POST: HardwareStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HardwareStatus hardwareStatus = await db.HardwareStatuses.FindAsync(id);
            db.HardwareStatuses.Remove(hardwareStatus);
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
