namespace DetroitEatz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stuff1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Restaurants", "Lat");
            DropColumn("dbo.Restaurants", "Lon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Restaurants", "Lon", c => c.Double(nullable: false));
            AddColumn("dbo.Restaurants", "Lat", c => c.Double(nullable: false));
        }
    }
}
