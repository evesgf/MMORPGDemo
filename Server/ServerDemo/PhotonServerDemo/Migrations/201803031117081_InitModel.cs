namespace PhotonServerDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Game_User", "Game_Character_Id", c => c.Int());
            CreateIndex("dbo.Game_User", "Game_Character_Id");
            AddForeignKey("dbo.Game_User", "Game_Character_Id", "dbo.Game_Character", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Game_User", "Game_Character_Id", "dbo.Game_Character");
            DropIndex("dbo.Game_User", new[] { "Game_Character_Id" });
            DropColumn("dbo.Game_User", "Game_Character_Id");
        }
    }
}
