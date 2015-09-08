namespace DetroitEatz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class favoritedetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Favorites", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Favorites", "PhoneNumber");
        }
    }
}
