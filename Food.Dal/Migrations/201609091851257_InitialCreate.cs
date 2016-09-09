namespace Food.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diaries",
                c => new
                    {
                        DiaryId = c.Int(nullable: false, identity: true),
                        CurrentDate = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ApiUser_ApiUserId = c.Int(),
                    })
                .PrimaryKey(t => t.DiaryId)
                .ForeignKey("dbo.ApiUsers", t => t.ApiUser_ApiUserId)
                .Index(t => t.ApiUser_ApiUserId);
            
            CreateTable(
                "dbo.ApiUsers",
                c => new
                    {
                        ApiUserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ApiUserId);
            
            CreateTable(
                "dbo.DiaryEntries",
                c => new
                    {
                        DiaryEntryId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        DiaryId = c.Int(nullable: false),
                        FoodId = c.Int(nullable: false),
                        MeasureId = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.DiaryEntryId)
                .ForeignKey("dbo.Diaries", t => t.DiaryId, cascadeDelete: true)
                .ForeignKey("dbo.Foods", t => t.FoodId)
                .ForeignKey("dbo.Measures", t => t.MeasureId)
                .Index(t => t.DiaryId)
                .Index(t => t.FoodId)
                .Index(t => t.MeasureId);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        FoodId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.FoodId);
            
            CreateTable(
                "dbo.Measures",
                c => new
                    {
                        MeasureId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Calories = c.Int(nullable: false),
                        TotalFat = c.Int(),
                        SaturatedFat = c.Int(),
                        Protein = c.Int(),
                        Sugar = c.Int(),
                        Sodium = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        FoodId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MeasureId)
                .ForeignKey("dbo.Foods", t => t.FoodId)
                .Index(t => t.FoodId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DiaryEntries", "MeasureId", "dbo.Measures");
            DropForeignKey("dbo.Measures", "FoodId", "dbo.Foods");
            DropForeignKey("dbo.DiaryEntries", "FoodId", "dbo.Foods");
            DropForeignKey("dbo.DiaryEntries", "DiaryId", "dbo.Diaries");
            DropForeignKey("dbo.Diaries", "ApiUser_ApiUserId", "dbo.ApiUsers");
            DropIndex("dbo.Measures", new[] { "FoodId" });
            DropIndex("dbo.DiaryEntries", new[] { "MeasureId" });
            DropIndex("dbo.DiaryEntries", new[] { "FoodId" });
            DropIndex("dbo.DiaryEntries", new[] { "DiaryId" });
            DropIndex("dbo.Diaries", new[] { "ApiUser_ApiUserId" });
            DropTable("dbo.Measures");
            DropTable("dbo.Foods");
            DropTable("dbo.DiaryEntries");
            DropTable("dbo.ApiUsers");
            DropTable("dbo.Diaries");
        }
    }
}
