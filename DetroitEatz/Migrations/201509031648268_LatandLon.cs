namespace DetroitEatz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatandLon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "Lat", c => c.Double(nullable: false));
            AddColumn("dbo.Restaurants", "Lon", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "Lon");
            DropColumn("dbo.Restaurants", "Lat");
        }
    }
}
