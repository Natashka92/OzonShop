namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adress",
                c => new
                    {
                        AdressId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Country = c.String(nullable: false, maxLength: 10),
                        Town = c.String(nullable: false, maxLength: 10),
                        Street = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.AdressId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Login = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 60),
                        Distribution = c.Boolean(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Basket",
                c => new
                    {
                        BasketsId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BasketsId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Description",
                c => new
                    {
                        DescriptionId = c.Int(nullable: false, identity: true),
                        TextDescription = c.String(nullable: false, maxLength: 200),
                        Data = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.DescriptionId)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        Url = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 250),
                        Price = c.Double(nullable: false),
                        Description = c.String(maxLength: 2000),
                        Barcode = c.String(maxLength: 13),
                        Discount = c.Double(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Currency_CurrencyId = c.Int(),
                        Vendor_VendorId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Currency", t => t.Currency_CurrencyId)
                .ForeignKey("dbo.Vendor", t => t.Vendor_VendorId)
                .Index(t => t.CategoryId)
                .Index(t => t.Currency_CurrencyId)
                .Index(t => t.Vendor_VendorId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Parent_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Category", t => t.Parent_CategoryId)
                .Index(t => t.Parent_CategoryId);
            
            CreateTable(
                "dbo.Currency",
                c => new
                    {
                        CurrencyId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 3),
                        Rate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CurrencyId);
            
            CreateTable(
                "dbo.Parameter",
                c => new
                    {
                        ParameterId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ParamNameId = c.Int(nullable: false),
                        ParamValueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParameterId)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Picture",
                c => new
                    {
                        PictureId = c.Int(nullable: false, identity: true),
                        PictureUrl = c.String(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PictureId)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.Vendor",
                c => new
                    {
                        VendorId = c.Int(nullable: false, identity: true),
                        VendorCode = c.String(),
                        VendorName_VendorNameId = c.Int(),
                    })
                .PrimaryKey(t => t.VendorId)
                .ForeignKey("dbo.VendorName", t => t.VendorName_VendorNameId)
                .Index(t => t.VendorName_VendorNameId);
            
            CreateTable(
                "dbo.VendorName",
                c => new
                    {
                        VendorNameId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.VendorNameId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        AdressId = c.Int(nullable: false),
                        OrderData = c.DateTime(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Currency", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CurrencyId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OrderProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ParamName",
                c => new
                    {
                        ParamNameId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ParamNameId);
            
            CreateTable(
                "dbo.ParamValue",
                c => new
                    {
                        ParamValueId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ParamValueId);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductDescriptions",
                c => new
                    {
                        Product_ProductId = c.Int(nullable: false),
                        Description_DescriptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ProductId, t.Description_DescriptionId })
                .ForeignKey("dbo.Product", t => t.Product_ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Description", t => t.Description_DescriptionId, cascadeDelete: true)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Description_DescriptionId);
            
            CreateTable(
                "dbo.TagProducts",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Product_ProductId })
                .ForeignKey("dbo.Tag", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_ProductId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "UserId", "dbo.User");
            DropForeignKey("dbo.OrderProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderProduct", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Order", "CurrencyId", "dbo.Currency");
            DropForeignKey("dbo.Description", "User_UserId", "dbo.User");
            DropForeignKey("dbo.Product", "Vendor_VendorId", "dbo.Vendor");
            DropForeignKey("dbo.Vendor", "VendorName_VendorNameId", "dbo.VendorName");
            DropForeignKey("dbo.TagProducts", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.TagProducts", "Tag_TagId", "dbo.Tag");
            DropForeignKey("dbo.Picture", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Parameter", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductDescriptions", "Description_DescriptionId", "dbo.Description");
            DropForeignKey("dbo.ProductDescriptions", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "Currency_CurrencyId", "dbo.Currency");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Category", "Parent_CategoryId", "dbo.Category");
            DropForeignKey("dbo.Basket", "UserId", "dbo.User");
            DropForeignKey("dbo.Adress", "UserId", "dbo.User");
            DropIndex("dbo.Order", new[] { "UserId" });
            DropIndex("dbo.OrderProduct", new[] { "ProductId" });
            DropIndex("dbo.OrderProduct", new[] { "OrderId" });
            DropIndex("dbo.Order", new[] { "CurrencyId" });
            DropIndex("dbo.Description", new[] { "User_UserId" });
            DropIndex("dbo.Product", new[] { "Vendor_VendorId" });
            DropIndex("dbo.Vendor", new[] { "VendorName_VendorNameId" });
            DropIndex("dbo.TagProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.TagProducts", new[] { "Tag_TagId" });
            DropIndex("dbo.Picture", new[] { "ProductId" });
            DropIndex("dbo.Parameter", new[] { "ProductId" });
            DropIndex("dbo.ProductDescriptions", new[] { "Description_DescriptionId" });
            DropIndex("dbo.ProductDescriptions", new[] { "Product_ProductId" });
            DropIndex("dbo.Product", new[] { "Currency_CurrencyId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropIndex("dbo.Category", new[] { "Parent_CategoryId" });
            DropIndex("dbo.Basket", new[] { "UserId" });
            DropIndex("dbo.Adress", new[] { "UserId" });
            DropTable("dbo.TagProducts");
            DropTable("dbo.ProductDescriptions");
            DropTable("dbo.Store");
            DropTable("dbo.ParamValue");
            DropTable("dbo.ParamName");
            DropTable("dbo.OrderProduct");
            DropTable("dbo.Order");
            DropTable("dbo.VendorName");
            DropTable("dbo.Vendor");
            DropTable("dbo.Tag");
            DropTable("dbo.Picture");
            DropTable("dbo.Parameter");
            DropTable("dbo.Currency");
            DropTable("dbo.Category");
            DropTable("dbo.Product");
            DropTable("dbo.Description");
            DropTable("dbo.Basket");
            DropTable("dbo.User");
            DropTable("dbo.Adress");
        }
    }
}
