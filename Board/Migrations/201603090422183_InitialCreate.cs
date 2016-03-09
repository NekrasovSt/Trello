namespace Board.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.Boolean(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BoardId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.BoardId)
                .Index(t => t.BoardId);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        PlaneDate = c.DateTime(nullable: false),
                        Archived = c.Boolean(nullable: false),
                        ListId = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lists", t => t.ListId)
                .Index(t => t.ListId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.CardId)
                .Index(t => t.CardId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lists", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.Cards", "ListId", "dbo.Lists");
            DropForeignKey("dbo.Comments", "CardId", "dbo.Cards");
            DropIndex("dbo.Comments", new[] { "CardId" });
            DropIndex("dbo.Cards", new[] { "ListId" });
            DropIndex("dbo.Lists", new[] { "BoardId" });
            DropTable("dbo.Comments");
            DropTable("dbo.Cards");
            DropTable("dbo.Lists");
            DropTable("dbo.Boards");
        }
    }
}
