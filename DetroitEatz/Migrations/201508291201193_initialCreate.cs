namespace DetroitEatz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        FavoriteID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        PlaceID = c.String(),
                        RestaurantName = c.String(),
                    })
                .PrimaryKey(t => t.FavoriteID);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantID = c.Int(nullable: false, identity: true),
                        PlaceID = c.String(nullable: false),
                        Name = c.String(),
                        PriceLevel = c.String(),
                        WebSite = c.String(),
                        Rating = c.String(),
                        OpenNow = c.Boolean(nullable: false),
                        Time = c.String(),
                        Day = c.String(),
                        Street = c.String(),
                        Zip = c.String(),
                        City = c.String(),
                        AddressNumber = c.String(),
                        State = c.String(),
                        PhoneNumber = c.String(),
                        Favorite_FavoriteID = c.Int(),
                    })
                .PrimaryKey(t => t.RestaurantID)
                .ForeignKey("dbo.Favorites", t => t.Favorite_FavoriteID)
                .Index(t => t.Favorite_FavoriteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurants", "Favorite_FavoriteID", "dbo.Favorites");
            DropIndex("dbo.Restaurants", new[] { "Favorite_FavoriteID" });
            DropTable("dbo.Restaurants");
            DropTable("dbo.Favorites");
        }
    }
}
