namespace TaxiCloud.Core.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Services = c.Int(nullable: false),
                        Category = c.Int(nullable: false),
                        Signal = c.String(unicode: false),
                        Number = c.String(unicode: false),
                        DateCreate = c.DateTime(nullable: false, precision: 0),
                        Transmission = c.String(unicode: false),
                        Mark = c.String(unicode: false),
                        ModelName = c.String(unicode: false),
                        Year = c.Int(nullable: false),
                        Color = c.String(unicode: false),
                        Status = c.String(unicode: false),
                        LicenseNumber = c.String(unicode: false),
                        LicenseSeries = c.String(unicode: false),
                        LicenseDocument = c.String(unicode: false),
                        Distance = c.Int(nullable: false),
                        ChairCount = c.Int(nullable: false),
                        BusterCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        YandexId = c.String(unicode: false),
                        Status = c.String(unicode: false),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        Surname = c.String(unicode: false),
                        DateCreate = c.DateTime(nullable: false, precision: 0),
                        PersonalPostTerminal = c.Boolean(nullable: false),
                        DateStart = c.DateTime(nullable: false, precision: 0),
                        LicenseSeries = c.String(unicode: false),
                        LicenseNumber = c.String(unicode: false),
                        LicenseDateOf = c.DateTime(nullable: false, precision: 0),
                        LicenseDateEnd = c.DateTime(nullable: false, precision: 0),
                        WorkStatusType = c.Int(nullable: false),
                        RuleId = c.String(unicode: false),
                        Phones = c.String(unicode: false),
                        CarId = c.String(unicode: false),
                        Balance = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(unicode: false),
                        Message = c.String(unicode: false),
                        InternalId = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(unicode: false),
                        Password = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Phone = c.String(unicode: false),
                        Fio = c.String(unicode: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Notifications");
            DropTable("dbo.Drivers");
            DropTable("dbo.Cars");
        }
    }
}
