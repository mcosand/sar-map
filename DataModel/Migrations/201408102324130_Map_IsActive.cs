namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Map_IsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Maps", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.MapObjects", "Map_Id", c => c.Guid());
            CreateIndex("dbo.MapObjects", "Map_Id");
            AddForeignKey("dbo.MapObjects", "Map_Id", "dbo.Maps", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MapObjects", "Map_Id", "dbo.Maps");
            DropIndex("dbo.MapObjects", new[] { "Map_Id" });
            DropColumn("dbo.MapObjects", "Map_Id");
            DropColumn("dbo.Maps", "IsActive");
        }
    }
}
