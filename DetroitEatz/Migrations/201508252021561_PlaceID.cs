namespace DetroitEatz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlaceID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "PlaceID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "PlaceID");
        }
    }
}
