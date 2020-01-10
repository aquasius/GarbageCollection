namespace GarbCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "ExtraPickUpDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "ExtraPickUpDate", c => c.Int(nullable: false));
        }
    }
}
