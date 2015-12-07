namespace RISH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 500),
                        City = c.String(nullable: false, maxLength: 300),
                        OwnerName = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "LocationNameIndex");
            
            AddColumn("dbo.Hardwares", "LocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Hardwares", "LocationId");
            AddForeignKey("dbo.Hardwares", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hardwares", "LocationId", "dbo.Locations");
            DropIndex("dbo.Locations", "LocationNameIndex");
            DropIndex("dbo.Hardwares", new[] { "LocationId" });
            DropColumn("dbo.Hardwares", "LocationId");
            DropTable("dbo.Locations");
        }
    }
}
