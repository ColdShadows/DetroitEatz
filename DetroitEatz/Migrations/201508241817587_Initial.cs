namespace DetroitEatz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantID = c.Int(nullable: false, identity: true),
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
                    })
                .PrimaryKey(t => t.RestaurantID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.UserRestaurants",
                c => new
                    {
                        User_UserID = c.Int(nullable: false),
                        Restaurant_RestaurantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserID, t.Restaurant_RestaurantID })
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_RestaurantID, cascadeDelete: true)
                .Index(t => t.User_UserID)
                .Index(t => t.Restaurant_RestaurantID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRestaurants", "Restaurant_RestaurantID", "dbo.Restaurants");
            DropForeignKey("dbo.UserRestaurants", "User_UserID", "dbo.Users");
            DropIndex("dbo.UserRestaurants", new[] { "Restaurant_RestaurantID" });
            DropIndex("dbo.UserRestaurants", new[] { "User_UserID" });
            DropTable("dbo.UserRestaurants");
            DropTable("dbo.Users");
            DropTable("dbo.Restaurants");
        }
    }
}
