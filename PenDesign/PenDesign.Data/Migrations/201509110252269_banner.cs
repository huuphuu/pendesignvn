namespace PenDesign.Data.MigrationConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class banner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "ZOrder", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "ZOrder");
        }
    }
}
