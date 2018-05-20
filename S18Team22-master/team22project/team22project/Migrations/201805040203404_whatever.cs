namespace team22project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whatever : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "ExtendedPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "ExtendedPrice");
        }
    }
}
