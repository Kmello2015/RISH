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
	public class HardwareTypeController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: HardwareType
		public async Task<ActionResult> Index()
		{
			return View(await db.HardwareTypes.ToListAsync());
		}

		// GET: HardwareType/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			HardwareType hardwareType = await db.HardwareTypes.FindAsync(id);
			if (hardwareType == null)
			{
				return HttpNotFound();
			}
			return View(hardwareType);
		}

		// GET: HardwareType/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: HardwareType/Create
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,Name")] HardwareType hardwareType)
		{
			try
			{
				if (ModelState.IsValid)
				{
					db.HardwareTypes.Add(hardwareType);
					await db.SaveChangesAsync();
					return RedirectToAction("Index");
				}
			}
			catch (DataException)
			{
				ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
			}
			return View(hardwareType);
		}

		// GET: HardwareType/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			HardwareType hardwareType = await db.HardwareTypes.FindAsync(id);
			if (hardwareType == null)
			{
				return HttpNotFound();
			}
			return View(hardwareType);
		}

		// POST: HardwareType/Edit/5
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] HardwareType hardwareType)
		{
			if (ModelState.IsValid)
			{
				db.Entry(hardwareType).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(hardwareType);
		}

		// GET: HardwareType/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			HardwareType hardwareType = await db.HardwareTypes.FindAsync(id);
			if (hardwareType == null)
			{
				return HttpNotFound();
			}
			return View(hardwareType);
		}

		// POST: HardwareType/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			HardwareType hardwareType = await db.HardwareTypes.FindAsync(id);
			db.HardwareTypes.Remove(hardwareType);
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
