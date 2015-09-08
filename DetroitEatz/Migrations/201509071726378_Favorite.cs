namespace DetroitEatz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Favorite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Favorites", "Lat", c => c.Double(nullable: false));
            AddColumn("dbo.Favorites", "Lon", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Favorites", "Lon");
            DropColumn("dbo.Favorites", "Lat");
        }
    }
}
