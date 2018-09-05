namespace CoolBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BooksUser : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Books", "UserId");
            AddForeignKey("dbo.Books", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "UserId", "dbo.Users");
            DropIndex("dbo.Books", new[] { "UserId" });
        }
    }
}
