namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Maps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Lat = c.Double(nullable: false),
                        Lng = c.Double(nullable: false),
                        Zoom = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MapTrackables",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MapId = c.Guid(nullable: false),
                        MapObjectId = c.Guid(),
                        ServiceType = c.String(),
                        ServiceId = c.String(),
                        ServiceStatus = c.String(),
                        LastUpdate = c.DateTime(),
                        LastQuery = c.DateTime(),
                        MapType = c.String(),
                        MapTypeRef = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Maps", t => t.MapId, cascadeDelete: true)
                .ForeignKey("dbo.MapObjects", t => t.MapObjectId)
                .Index(t => t.MapId)
                .Index(t => t.MapObjectId);
            
            CreateTable(
                "dbo.MapObjects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Class = c.String(),
                        Text = c.String(),
                        Shape = c.Geography(),
                        Data = c.String(),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MapObjects", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MapTrackables", "MapObjectId", "dbo.MapObjects");
            DropForeignKey("dbo.MapObjects", "ParentId", "dbo.MapObjects");
            DropForeignKey("dbo.MapTrackables", "MapId", "dbo.Maps");
            DropIndex("dbo.MapObjects", new[] { "ParentId" });
            DropIndex("dbo.MapTrackables", new[] { "MapObjectId" });
            DropIndex("dbo.MapTrackables", new[] { "MapId" });
            DropTable("dbo.MapObjects");
            DropTable("dbo.MapTrackables");
            DropTable("dbo.Maps");
        }
    }
}
