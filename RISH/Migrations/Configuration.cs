using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RISH.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace RISH.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(ApplicationDbContext context)
		{
			var types = new List<HardwareType>
			{
				new HardwareType { Name = "Desktop" },
				new HardwareType { Name = "Notebook" },
				new HardwareType { Name = "Impresora" },
				new HardwareType { Name = "Teclado" },
				new HardwareType { Name = "Monitor" },
				new HardwareType { Name = "Servidor" },
				new HardwareType { Name = "Dispositivos Comunicación" },
				new HardwareType { Name = "BAM" },
				new HardwareType { Name = "R-BAM" },
			};
			types.ForEach(i => context.HardwareTypes.AddOrUpdate(t => t.Name, i));
			context.SaveChanges();

			var statuses = new List<HardwareStatus>
			{
				new HardwareStatus { Name = "Activo" },
				new HardwareStatus { Name = "Inactivo" },
				new HardwareStatus { Name = "En bodega" },
				new HardwareStatus { Name = "Para baja" },
			};
			statuses.ForEach(i => context.HardwareStatuses.AddOrUpdate(s => s.Name, i));
			context.SaveChanges();

			var architectures = new List<SoftwareArchitecture>
			{
				new SoftwareArchitecture { Name = "Cualquiera" },
				new SoftwareArchitecture { Name = "32 bits (x86)" },
				new SoftwareArchitecture { Name = "64 bits (x64)" },
			};
			architectures.ForEach(i => context.SoftwareArchitectures.AddOrUpdate(a => a.Name, i));
			context.SaveChanges();

			var vendors = new List<Vendor>
			{
				new Vendor { Name = "Microsoft" },
				new Vendor { Name = "Adobe" },
				new Vendor { Name = "Symantec" },
				new Vendor { Name = "Autodesk" },
				new Vendor { Name = "Kaspersky" },
				new Vendor { Name = "Cysco" },
				new Vendor { Name = "SAP" },
			};
			vendors.ForEach(i => context.Vendors.AddOrUpdate(v => v.Name, i));
			context.SaveChanges();

			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

			var roles = new List<IdentityRole>
			{
				new IdentityRole("Administrador"),
				new IdentityRole("Supervisor"),
				new IdentityRole("Solicitante"),
			};
			foreach (var role in roles)
			{
				if (roleManager.FindByName(role.Name) == null)
				{
					roleManager.Create(role);
				}
			}

			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
			var user = userManager.FindByName("esteban.rodriguez@outlook.com");
			if (user == null)
			{
				user = new ApplicationUser
				{
					UserName = "esteban.rodriguez@outlook.com",
					Email = "esteban.rodriguez@outlook.com",
					FirstName = "Esteban",
					LastName = "Rodriguez"
				};
				if (userManager.Create(user, "Pa$$w0rd") == IdentityResult.Success)
				{
					userManager.AddToRoles(user.Id, "Administrador", "Supervisor", "Solicitante");
				}
			}

			user = userManager.FindByName("latapiatalfaro@gmail.com");
			if (user == null)
			{
				user = new ApplicationUser
				{
					UserName = "latapiatalfaro@gmail.com",
					Email = "latapiatalfaro@gmail.com",
					FirstName = "Maritza",
					LastName = "Latapiat"
				};
				if (userManager.Create(user, "Pa$$w0rd") == IdentityResult.Success)
				{
					userManager.AddToRoles(user.Id, "Supervisor", "Solicitante");
				}
			}
		}
	}
}
