namespace TaxiCloud.Core.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddDateCreatedToNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "DateCreated", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "DateCreated");
        }
    }
}