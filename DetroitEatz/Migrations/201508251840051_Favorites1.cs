namespace DetroitEatz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Favorites1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Favorites", "UserID", c => c.String());
            AddColumn("dbo.Favorites", "PlaceID", c => c.String());
            AddColumn("dbo.Favorites", "RestaurantName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Favorites", "RestaurantName");
            DropColumn("dbo.Favorites", "PlaceID");
            DropColumn("dbo.Favorites", "UserID");
        }
    }
}
