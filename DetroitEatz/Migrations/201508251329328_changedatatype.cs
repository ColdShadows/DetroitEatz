namespace DetroitEatz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedatatype : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRestaurants", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.UserRestaurants", "Restaurant_RestaurantID", "dbo.Restaurants");
            DropIndex("dbo.UserRestaurants", new[] { "User_UserID" });
            DropIndex("dbo.UserRestaurants", new[] { "Restaurant_RestaurantID" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRestaurants");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserRestaurants",
                c => new
                    {
                        User_UserID = c.Int(nullable: false),
                        Restaurant_RestaurantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserID, t.Restaurant_RestaurantID });
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateIndex("dbo.UserRestaurants", "Restaurant_RestaurantID");
            CreateIndex("dbo.UserRestaurants", "User_UserID");
            AddForeignKey("dbo.UserRestaurants", "Restaurant_RestaurantID", "dbo.Restaurants", "RestaurantID", cascadeDelete: true);
            AddForeignKey("dbo.UserRestaurants", "User_UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
    }
}
