namespace DetroitEatz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Favorites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        FavoriteID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.FavoriteID);
            
            AddColumn("dbo.Restaurants", "Favorite_FavoriteID", c => c.Int());
            CreateIndex("dbo.Restaurants", "Favorite_FavoriteID");
            AddForeignKey("dbo.Restaurants", "Favorite_FavoriteID", "dbo.Favorites", "FavoriteID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurants", "Favorite_FavoriteID", "dbo.Favorites");
            DropIndex("dbo.Restaurants", new[] { "Favorite_FavoriteID" });
            DropColumn("dbo.Restaurants", "Favorite_FavoriteID");
            DropTable("dbo.Favorites");
        }
    }
}
