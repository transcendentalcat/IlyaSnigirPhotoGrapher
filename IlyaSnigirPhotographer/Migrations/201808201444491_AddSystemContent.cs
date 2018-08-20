namespace IlyaSnigirPhotographer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSystemContent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IndexAvatarPhoto = c.Binary(),
                        IndexQuote = c.String(),
                        ContactsAvatarPhoto = c.Binary(),
                        ContactsCoverPhoto = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SystemContents");
        }
    }
}
