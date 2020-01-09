namespace GarbCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DayOfWeekList : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Zip", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Zip", c => c.String());
        }
    }
}
