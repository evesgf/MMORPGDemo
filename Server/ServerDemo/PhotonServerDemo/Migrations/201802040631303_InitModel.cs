namespace PhotonServerDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sys_User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userName = c.String(unicode: false),
                        passWord = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sys_User");
        }
    }
}
