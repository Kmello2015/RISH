namespace RISH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hardwares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false, maxLength: 100),
                        Model = c.String(nullable: false, maxLength: 100),
                        SerialNumber = c.String(nullable: false, maxLength: 50),
                        HardwareTypeId = c.Int(nullable: false),
                        Property01 = c.String(maxLength: 50),
                        Property02 = c.String(maxLength: 50),
                        Property03 = c.String(maxLength: 50),
                        Property04 = c.String(maxLength: 50),
                        Property05 = c.String(maxLength: 50),
                        Property06 = c.String(maxLength: 50),
                        Property07 = c.String(maxLength: 50),
                        Property08 = c.String(maxLength: 50),
                        Property09 = c.String(maxLength: 50),
                        Property10 = c.String(maxLength: 50),
                        ProviderId = c.Int(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                        WarrantyExpirationDate = c.DateTime(nullable: false),
                        InvoiceNumber = c.Int(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                        HardwareStatusId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("dbo.HardwareStatus", t => t.HardwareStatusId, cascadeDelete: true)
                .ForeignKey("dbo.HardwareTypes", t => t.HardwareTypeId, cascadeDelete: true)
                .Index(t => t.SerialNumber, unique: true, name: "HardwareSerialNumberIndex")
                .Index(t => t.HardwareTypeId)
                .Index(t => t.ProviderId)
                .Index(t => t.HardwareStatusId);
            
            CreateTable(
                "dbo.InstalledSoftwares",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        HardwareId = c.Int(nullable: false),
                        SoftwareId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hardwares", t => t.HardwareId, cascadeDelete: true)
                .ForeignKey("dbo.Softwares", t => t.SoftwareId, cascadeDelete: true)
                .Index(t => new { t.HardwareId, t.SoftwareId }, unique: true, name: "HardwareSoftwareIndex");
            
            CreateTable(
                "dbo.Softwares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        VendorId = c.Int(nullable: false),
                        LicenseTypeId = c.Short(nullable: false),
                        SoftwareArchitectureId = c.Short(nullable: false),
                        ProviderId = c.Int(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                        InvoiceNumber = c.Int(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LicenseTypes", t => t.LicenseTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("dbo.SoftwareArchitectures", t => t.SoftwareArchitectureId, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId)
                .Index(t => t.LicenseTypeId)
                .Index(t => t.SoftwareArchitectureId)
                .Index(t => t.ProviderId);
            
            CreateTable(
                "dbo.LicenseTypes",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "LicenseTypeNameIndex");
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RUT = c.String(nullable: false, maxLength: 12),
                        Name = c.String(nullable: false, maxLength: 200),
                        BusinessName = c.String(nullable: false, maxLength: 200),
                        Address = c.String(nullable: false, maxLength: 250),
                        PhoneNumber = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.RUT, unique: true, name: "ProviderRUTIndex")
                .Index(t => t.Name, unique: true, name: "ProviderNameIndex");
            
            CreateTable(
                "dbo.SoftwareArchitectures",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "SoftwareArchitectureNameIndex");
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "VendorNameIndex");
            
            CreateTable(
                "dbo.HardwareStatus",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "HardwareStatusNameIndex");
            
            CreateTable(
                "dbo.HardwareTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "HardwareTypeNameIndex");
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 200),
                        LastName = c.String(nullable: false, maxLength: 200),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Hardwares", "HardwareTypeId", "dbo.HardwareTypes");
            DropForeignKey("dbo.Hardwares", "HardwareStatusId", "dbo.HardwareStatus");
            DropForeignKey("dbo.Hardwares", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Softwares", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Softwares", "SoftwareArchitectureId", "dbo.SoftwareArchitectures");
            DropForeignKey("dbo.Softwares", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Softwares", "LicenseTypeId", "dbo.LicenseTypes");
            DropForeignKey("dbo.InstalledSoftwares", "SoftwareId", "dbo.Softwares");
            DropForeignKey("dbo.InstalledSoftwares", "HardwareId", "dbo.Hardwares");
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.HardwareTypes", "HardwareTypeNameIndex");
            DropIndex("dbo.HardwareStatus", "HardwareStatusNameIndex");
            DropIndex("dbo.Vendors", "VendorNameIndex");
            DropIndex("dbo.SoftwareArchitectures", "SoftwareArchitectureNameIndex");
            DropIndex("dbo.Providers", "ProviderNameIndex");
            DropIndex("dbo.Providers", "ProviderRUTIndex");
            DropIndex("dbo.LicenseTypes", "LicenseTypeNameIndex");
            DropIndex("dbo.Softwares", new[] { "ProviderId" });
            DropIndex("dbo.Softwares", new[] { "SoftwareArchitectureId" });
            DropIndex("dbo.Softwares", new[] { "LicenseTypeId" });
            DropIndex("dbo.Softwares", new[] { "VendorId" });
            DropIndex("dbo.InstalledSoftwares", "HardwareSoftwareIndex");
            DropIndex("dbo.Hardwares", new[] { "HardwareStatusId" });
            DropIndex("dbo.Hardwares", new[] { "ProviderId" });
            DropIndex("dbo.Hardwares", new[] { "HardwareTypeId" });
            DropIndex("dbo.Hardwares", "HardwareSerialNumberIndex");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.HardwareTypes");
            DropTable("dbo.HardwareStatus");
            DropTable("dbo.Vendors");
            DropTable("dbo.SoftwareArchitectures");
            DropTable("dbo.Providers");
            DropTable("dbo.LicenseTypes");
            DropTable("dbo.Softwares");
            DropTable("dbo.InstalledSoftwares");
            DropTable("dbo.Hardwares");
        }
    }
}
