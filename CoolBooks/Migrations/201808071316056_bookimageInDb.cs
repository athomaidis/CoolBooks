namespace CoolBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookimageInDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Image", c => c.Binary());
            AddColumn("dbo.Books", "MimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "MimeType");
            DropColumn("dbo.Books", "Image");
        }
    }
}
