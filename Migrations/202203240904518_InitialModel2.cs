namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MembershipTypes", "name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MembershipTypes", "name", c => c.String());
        }
    }
}
