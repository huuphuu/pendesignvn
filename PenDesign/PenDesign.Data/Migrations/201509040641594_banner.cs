namespace PenDesign.Data.MigrationConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class banner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banners", "MediaUrl", c => c.String());
            AddColumn("dbo.Banners", "ZOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Banners", "ZOrder");
            DropColumn("dbo.Banners", "MediaUrl");
        }
    }
}
