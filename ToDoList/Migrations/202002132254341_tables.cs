namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoLists",
                c => new
                    {
                        ListID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        TaskID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ListID)
                .ForeignKey("dbo.ToDoTasks", t => t.TaskID, cascadeDelete: true)
                .Index(t => t.TaskID);
            
            CreateTable(
                "dbo.ToDoTasks",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        Task = c.String(),
                        DueDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TaskID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoLists", "TaskID", "dbo.ToDoTasks");
            DropIndex("dbo.ToDoLists", new[] { "TaskID" });
            DropTable("dbo.ToDoTasks");
            DropTable("dbo.ToDoLists");
        }
    }
}
