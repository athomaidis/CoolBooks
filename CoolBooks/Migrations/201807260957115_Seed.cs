namespace CoolBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 2000),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        AuthorId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 256),
                        AlternativeTitle = c.String(maxLength: 256),
                        Part = c.Short(),
                        Description = c.String(maxLength: 2000),
                        ISBN = c.String(maxLength: 50),
                        PublishDate = c.DateTime(),
                        ImagePath = c.String(maxLength: 512),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .ForeignKey("dbo.Authors", t => t.AuthorId)
                .Index(t => t.AuthorId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 2000),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(maxLength: 50),
                        Text = c.String(maxLength: 4000),
                        Rating = c.Byte(),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Gender = c.String(maxLength: 1, fixedLength: true),
                        Birthdate = c.DateTime(),
                        Picture = c.Binary(storeType: "image"),
                        Phone = c.String(maxLength: 50),
                        Address = c.String(maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        City = c.String(maxLength: 100),
                        Country = c.String(maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 100),
                        Info = c.String(maxLength: 50),
                        Created = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Reviews", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "GenreId", "dbo.Genres");
            DropIndex("dbo.Reviews", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "GenreId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Genres");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
