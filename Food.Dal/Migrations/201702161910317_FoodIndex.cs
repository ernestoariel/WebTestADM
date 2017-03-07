namespace Food.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FoodIndex : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Foods", "Description", c => c.String(maxLength: 100));
            CreateIndex("dbo.Foods", "Description", unique: true, name: "IX_Food_Description");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Foods", "IX_Food_Description");
            AlterColumn("dbo.Foods", "Description", c => c.String());
        }
    }
}
