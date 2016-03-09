namespace Board.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trello : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lists", "MaxCardsCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lists", "MaxCardsCount");
        }
    }
}
