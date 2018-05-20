namespace team22project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdfdfd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CreditCards", c => c.String());
            DropColumn("dbo.AspNetUsers", "CreditCard1");
            DropColumn("dbo.AspNetUsers", "CreditCard2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CreditCard2", c => c.String());
            AddColumn("dbo.AspNetUsers", "CreditCard1", c => c.String());
            DropColumn("dbo.AspNetUsers", "CreditCards");
        }
    }
}
