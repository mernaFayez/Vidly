namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c9f2f452-0b23-459d-9f49-32b68a7bac92', N'guest@vidly.com', 0, N'AG9zg2znKXlvSNub0iKBC5ZOHbvPCheY/4+5LLW5osRmKtMSYaleGmHUjaQFtUOZmg==', N'49a3442e-5544-490b-a6ae-eee9b2f65d15', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd81a8533-f2cc-4b1c-8981-a25d11c1c463', N'admin@vidly.com', 0, N'AGvvWAIgh5F45Gq8B9tuALHlcLSP9g3jtaNMR+RtcnAaWYkGhHqSJgX+NMFfHIY6zw==', N'7897aca5-83d7-490f-b3c2-efa9c39c047c', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'04b8e450-db0c-4bcb-b2f7-1e409cec2ec6', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd81a8533-f2cc-4b1c-8981-a25d11c1c463', N'04b8e450-db0c-4bcb-b2f7-1e409cec2ec6')


            ");

        }
        
        public override void Down()
        {
        }
    }
}
