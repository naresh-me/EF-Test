namespace WPF_EF_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReagentBatches",
                c => new
                    {
                        BatchLotNumber = c.String(nullable: false, maxLength: 128),
                        ExpiryDate = c.DateTime(nullable: false),
                        Manufacturer = c.String(),
                        ManufacturerDate = c.DateTime(nullable: false),
                        ManufacturingSourceCode = c.String(),
                    })
                .PrimaryKey(t => t.BatchLotNumber);
            
            CreateTable(
                "dbo.ReagentLots",
                c => new
                    {
                        SerialNumber = c.String(nullable: false, maxLength: 128),
                        ExpiryDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Volume = c.Double(nullable: false),
                        ReactionTarget = c.Double(nullable: false),
                        ReactionRange = c.Double(nullable: false),
                        BatchLotNumber = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SerialNumber)
                .ForeignKey("dbo.ReagentBatches", t => t.BatchLotNumber)
                .Index(t => t.BatchLotNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReagentLots", "BatchLotNumber", "dbo.ReagentBatches");
            DropIndex("dbo.ReagentLots", new[] { "BatchLotNumber" });
            DropTable("dbo.ReagentLots");
            DropTable("dbo.ReagentBatches");
        }
    }
}
