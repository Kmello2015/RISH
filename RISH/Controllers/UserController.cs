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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RISH.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UserController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

        }
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // GET: User
        public async Task<ActionResult> Index()
        {
            return View(await userManager.Users.ToListAsync());
        }

        // GET: User/Details/5
        //public async Task<ActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser applicationUser = await db.ApplicationUsers.FindAsync(id);
        //    if (applicationUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(applicationUser);
        //}

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, FirstName = model.FirstName, LastName = model.LastName };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User");
                }
                AddErrors(result);
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        public async Task<ActionResult> AddRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            PopulateRoles(user.Roles);
            //ViewBag.RolesId = new MultiSelectList(roleManager.Roles, "Name", "Name", user.Roles);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRole(string id, string[] rolesId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await userManager.FindByIdAsync(id);
            //ViewBag.RolesId = new MultiSelectList(roleManager.Roles, "Id", "Name", user.Roles);
            PopulateRoles(user.Roles);
            try
            {
                List<string> lista = new List<string>();
                foreach (var item in user.Roles)
                {
                    lista.Add(item.RoleId);
                }

                var selectedRoles = new HashSet<string>(rolesId);
                var userRoles = new HashSet<string>(lista);
                foreach (var role in roleManager.Roles)
                {
                    if (selectedRoles.Contains(role.Id))
                    {
                        if (!userRoles.Contains(role.Id))
                        {
                            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)))
                            {
                                await um.AddToRoleAsync(user.Id, role.Name);
                            }
                        }
                    }
                    else
                    {
                        if (userRoles.Contains(role.Id))
                        {
                            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)))
                            {
                                await um.RemoveFromRoleAsync(user.Id, role.Name);
                            }
                            //await userManager.RemoveFromRoleAsync(user.Id, role.Name);
                        }
                    }
                }
                PopulateRoles(user.Roles);
                return RedirectToAction("Index");
                //foreach (var item in rolesId)
                //{
                //    var role = await roleManager.FindByIdAsync(item);
                //    var result = await userManager.AddToRoleAsync(id, role.Name);
                //    if (!result.Succeeded)
                //    {
                //        ModelState.AddModelError("", result.Errors.First().ToString());
                //        //ViewBag.RolesId = new MultiSelectList(roleManager.Roles, "Id", "Name", user.Roles);
                //        return View(user);
                //    }
                //    return RedirectToAction("Index");
                //}
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", "Ha ocurrido un error al agregar los roles");
            }
            return View(user);
        }


        //// GET: User/Edit/5
        //public async Task<ActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser applicationUser = await db.ApplicationUsers.FindAsync(id);
        //    if (applicationUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(applicationUser);
        //}

        //// POST: User/Edit/5
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(applicationUser).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(applicationUser);
        //}

        //// GET: User/Delete/5
        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser applicationUser = await db.ApplicationUsers.FindAsync(id);
        //    if (applicationUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(applicationUser);
        //}

        //// POST: User/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(string id)
        //{
        //    ApplicationUser applicationUser = await db.ApplicationUsers.FindAsync(id);
        //    db.ApplicationUsers.Remove(applicationUser);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private void PopulateRoles(ICollection<IdentityUserRole> roles)
        {
            //var userRoles = new List<IdentityRole>();
            //foreach (var role in roles)
            //{
            //    userRoles.Add(roleManager.FindById(role.RoleId));
            //}
            var userRoles = new List<string>();
            foreach (var role in roles)
            {
                userRoles.Add(role.RoleId);
            }
            ViewBag.RolesId = new MultiSelectList(roleManager.Roles, "Id", "Name", userRoles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                userManager.Dispose();
                roleManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
