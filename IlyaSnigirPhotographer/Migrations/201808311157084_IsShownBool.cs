namespace IlyaSnigirPhotographer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsShownBool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "IsShown", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "IsShown", c => c.String());
        }
    }
}
