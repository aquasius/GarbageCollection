namespace GarbCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oijhfg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "zip", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "zip", c => c.Int(nullable: false));
        }
    }
}
