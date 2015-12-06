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
    public class SoftwareArchitectureController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SoftwareArchitecture
        public async Task<ActionResult> Index()
        {
            return View(await db.SoftwareArchitectures.ToListAsync());
        }

        // GET: SoftwareArchitecture/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoftwareArchitecture softwareArchitecture = await db.SoftwareArchitectures.FindAsync(id);
            if (softwareArchitecture == null)
            {
                return HttpNotFound();
            }
            return View(softwareArchitecture);
        }

        // GET: SoftwareArchitecture/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SoftwareArchitecture/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] SoftwareArchitecture softwareArchitecture)
        {
            if (ModelState.IsValid)
            {
                db.SoftwareArchitectures.Add(softwareArchitecture);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(softwareArchitecture);
        }

        // GET: SoftwareArchitecture/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoftwareArchitecture softwareArchitecture = await db.SoftwareArchitectures.FindAsync(id);
            if (softwareArchitecture == null)
            {
                return HttpNotFound();
            }
            return View(softwareArchitecture);
        }

        // POST: SoftwareArchitecture/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] SoftwareArchitecture softwareArchitecture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(softwareArchitecture).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(softwareArchitecture);
        }

        // GET: SoftwareArchitecture/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoftwareArchitecture softwareArchitecture = await db.SoftwareArchitectures.FindAsync(id);
            if (softwareArchitecture == null)
            {
                return HttpNotFound();
            }
            return View(softwareArchitecture);
        }

        // POST: SoftwareArchitecture/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SoftwareArchitecture softwareArchitecture = await db.SoftwareArchitectures.FindAsync(id);
            db.SoftwareArchitectures.Remove(softwareArchitecture);
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
