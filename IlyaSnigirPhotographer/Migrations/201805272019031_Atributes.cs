namespace IlyaSnigirPhotographer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Atributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Reviews", "Message", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Message", c => c.String());
            AlterColumn("dbo.Reviews", "Name", c => c.String());
        }
    }
}
