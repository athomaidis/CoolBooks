namespace CoolBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingImageFildesIntoUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Image", c => c.Binary());
            AddColumn("dbo.Users", "MimeType", c => c.String());
            AlterColumn("dbo.Users", "ImagePath", c => c.String(maxLength: 512));
            DropColumn("dbo.Users", "Picture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Picture", c => c.Binary(storeType: "image"));
            AlterColumn("dbo.Users", "ImagePath", c => c.String());
            DropColumn("dbo.Users", "MimeType");
            DropColumn("dbo.Users", "Image");
        }
    }
}
