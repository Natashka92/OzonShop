namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Description", "User_UserId", "dbo.User");
            DropIndex("dbo.Description", new[] { "User_UserId" });
            DropColumn("dbo.Description", "UserId");
            RenameColumn(table: "dbo.Description", name: "User_UserId", newName: "UserId");
            AlterColumn("dbo.Description", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Description", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Description", "UserId");
            AddForeignKey("dbo.Description", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Description", "UserId", "dbo.User");
            DropIndex("dbo.Description", new[] { "UserId" });
            AlterColumn("dbo.Description", "UserId", c => c.Int());
            AlterColumn("dbo.Description", "UserId", c => c.DateTime(nullable: false));
            RenameColumn(table: "dbo.Description", name: "UserId", newName: "User_UserId");
            AddColumn("dbo.Description", "UserId", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Description", "User_UserId");
            AddForeignKey("dbo.Description", "User_UserId", "dbo.User", "UserId");
        }
    }
}
