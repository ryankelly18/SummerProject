namespace team22project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialsetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        DiscountID = c.Int(nullable: false, identity: true),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Transaction_TransactionID = c.Int(),
                    })
                .PrimaryKey(t => t.DiscountID)
                .ForeignKey("dbo.Transactions", t => t.Transaction_TransactionID)
                .Index(t => t.Transaction_TransactionID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        TransactionNumber = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        OrderSubtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TStatus = c.Int(nullable: false),
                        gift = c.Boolean(nullable: false),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CustomerNumber = c.Int(nullable: false),
                        EmployeeType = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        PopcornPoints = c.Int(nullable: false),
                        CreditCard1 = c.String(),
                        CreditCard2 = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        Stars = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(maxLength: 100),
                        UpVoteCount = c.Int(nullable: false),
                        DownVoteCount = c.Int(nullable: false),
                        AppUser_Id = c.String(maxLength: 128),
                        Movie_MovieID = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Movie_MovieID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieID = c.Int(nullable: false, identity: true),
                        MovieNumber = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Overview = c.String(),
                        Tagline = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Revenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MPAARating = c.Int(nullable: false),
                        RunTime = c.Int(nullable: false),
                        Actors = c.String(),
                    })
                .PrimaryKey(t => t.MovieID);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreID = c.Int(nullable: false, identity: true),
                        GenreName = c.String(),
                    })
                .PrimaryKey(t => t.GenreID);
            
            CreateTable(
                "dbo.Showings",
                c => new
                    {
                        ShowingID = c.Int(nullable: false, identity: true),
                        ShowingDate = c.DateTime(nullable: false),
                        Display = c.DateTime(nullable: false),
                        DayOfWeek = c.Int(nullable: false),
                        Theatre = c.Int(nullable: false),
                        SpecialEvent = c.Boolean(nullable: false),
                        Movie_MovieID = c.Int(),
                    })
                .PrimaryKey(t => t.ShowingID)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID)
                .Index(t => t.Movie_MovieID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        ExtendedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Seat = c.String(),
                        Showing_ShowingID = c.Int(),
                        Transaction_TransactionID = c.Int(),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.Showings", t => t.Showing_ShowingID)
                .ForeignKey("dbo.Transactions", t => t.Transaction_TransactionID)
                .Index(t => t.Showing_ShowingID)
                .Index(t => t.Transaction_TransactionID);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatID = c.Int(nullable: false, identity: true),
                        SeatName = c.String(),
                    })
                .PrimaryKey(t => t.SeatID);
            
            CreateTable(
                "dbo.GenreMovies",
                c => new
                    {
                        Genre_GenreID = c.Int(nullable: false),
                        Movie_MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_GenreID, t.Movie_MovieID })
                .ForeignKey("dbo.Genres", t => t.Genre_GenreID, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieID, cascadeDelete: true)
                .Index(t => t.Genre_GenreID)
                .Index(t => t.Movie_MovieID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Discounts", "Transaction_TransactionID", "dbo.Transactions");
            DropForeignKey("dbo.Transactions", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "Transaction_TransactionID", "dbo.Transactions");
            DropForeignKey("dbo.Tickets", "Showing_ShowingID", "dbo.Showings");
            DropForeignKey("dbo.Showings", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.Reviews", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Movie_MovieID", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.Reviews", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.GenreMovies", new[] { "Movie_MovieID" });
            DropIndex("dbo.GenreMovies", new[] { "Genre_GenreID" });
            DropIndex("dbo.Tickets", new[] { "Transaction_TransactionID" });
            DropIndex("dbo.Tickets", new[] { "Showing_ShowingID" });
            DropIndex("dbo.Showings", new[] { "Movie_MovieID" });
            DropIndex("dbo.Reviews", new[] { "Movie_MovieID" });
            DropIndex("dbo.Reviews", new[] { "AppUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Transactions", new[] { "AppUser_Id" });
            DropIndex("dbo.Discounts", new[] { "Transaction_TransactionID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.GenreMovies");
            DropTable("dbo.Seats");
            DropTable("dbo.Tickets");
            DropTable("dbo.Showings");
            DropTable("dbo.Genres");
            DropTable("dbo.Movies");
            DropTable("dbo.Reviews");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Transactions");
            DropTable("dbo.Discounts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
