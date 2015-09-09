namespace DetroitEatz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalfavorites : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Restaurants", "Favorite_FavoriteID", "dbo.Favorites");
            DropIndex("dbo.Restaurants", new[] { "Favorite_FavoriteID" });
            AddColumn("dbo.Favorites", "Address", c => c.String());
            DropColumn("dbo.Restaurants", "Favorite_FavoriteID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Restaurants", "Favorite_FavoriteID", c => c.Int());
            DropColumn("dbo.Favorites", "Address");
            CreateIndex("dbo.Restaurants", "Favorite_FavoriteID");
            AddForeignKey("dbo.Restaurants", "Favorite_FavoriteID", "dbo.Favorites", "FavoriteID");
        }
    }
}
