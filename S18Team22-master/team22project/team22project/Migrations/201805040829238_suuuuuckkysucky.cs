namespace team22project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class suuuuuckkysucky : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Seats");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatID = c.Int(nullable: false, identity: true),
                        SeatName = c.String(),
                    })
                .PrimaryKey(t => t.SeatID);
            
        }
    }
}
