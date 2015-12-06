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
	[Authorize(Roles = "Administrador")]
	public class HardwareController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Hardware
		public async Task<ActionResult> Index()
		{
			var hardwares = db.Hardwares.Include(h => h.Provider).Include(h => h.Status).Include(h => h.Type);
			return View(await hardwares.ToListAsync());
		}

		// GET: Hardware/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Hardware hardware = await db.Hardwares.FindAsync(id);
			if (hardware == null)
			{
				return HttpNotFound();
			}
			return View(hardware);
		}

		// GET: Hardware/Create
		public ActionResult Create()
		{
			ViewBag.ProviderId = new SelectList(db.Providers, "Id", "RUT");
			ViewBag.HardwareStatusId = new SelectList(db.HardwareStatuses, "Id", "Name");
			ViewBag.HardwareTypeId = new SelectList(db.HardwareTypes.OrderBy(t => t.Id), "Id", "Name");
			return View();
		}

		// POST: Hardware/Create
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,Brand,Model,SerialNumber,HardwareTypeId,Property01,Property02,Property03,Property04,Property05,Property06,Property07,Property08,Property09,Property10,ProviderId,PurchaseDate,WarrantyExpirationDate,InvoiceNumber,InvoiceDate,HardwareStatusId")] Hardware hardware)
		{
			if (ModelState.IsValid)
			{
				db.Hardwares.Add(hardware);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			ViewBag.ProviderId = new SelectList(db.Providers, "Id", "RUT", hardware.ProviderId);
			ViewBag.HardwareStatusId = new SelectList(db.HardwareStatuses, "Id", "Name", hardware.HardwareStatusId);
			ViewBag.HardwareTypeId = new SelectList(db.HardwareTypes, "Id", "Name", hardware.HardwareTypeId);
			return View(hardware);
		}

		// GET: Hardware/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Hardware hardware = await db.Hardwares.FindAsync(id);
			if (hardware == null)
			{
				return HttpNotFound();
			}
			ViewBag.ProviderId = new SelectList(db.Providers, "Id", "RUT", hardware.ProviderId);
			ViewBag.HardwareStatusId = new SelectList(db.HardwareStatuses, "Id", "Name", hardware.HardwareStatusId);
			ViewBag.HardwareTypeId = new SelectList(db.HardwareTypes, "Id", "Name", hardware.HardwareTypeId);
			return View(hardware);
		}

		// POST: Hardware/Edit/5
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,Brand,Model,SerialNumber,HardwareTypeId,Property01,Property02,Property03,Property04,Property05,Property06,Property07,Property08,Property09,Property10,ProviderId,PurchaseDate,WarrantyExpirationDate,InvoiceNumber,InvoiceDate,HardwareStatusId")] Hardware hardware)
		{
			if (ModelState.IsValid)
			{
				db.Entry(hardware).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewBag.ProviderId = new SelectList(db.Providers, "Id", "RUT", hardware.ProviderId);
			ViewBag.HardwareStatusId = new SelectList(db.HardwareStatuses, "Id", "Name", hardware.HardwareStatusId);
			ViewBag.HardwareTypeId = new SelectList(db.HardwareTypes, "Id", "Name", hardware.HardwareTypeId);
			return View(hardware);
		}

		// GET: Hardware/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Hardware hardware = await db.Hardwares.FindAsync(id);
			if (hardware == null)
			{
				return HttpNotFound();
			}
			return View(hardware);
		}

		// POST: Hardware/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			Hardware hardware = await db.Hardwares.FindAsync(id);
			db.Hardwares.Remove(hardware);
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
