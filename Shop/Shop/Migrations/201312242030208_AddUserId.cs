namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Description", "UserId", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Description", "UserId");
        }
    }
}
