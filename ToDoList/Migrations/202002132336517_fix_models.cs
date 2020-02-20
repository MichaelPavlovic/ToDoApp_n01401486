namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_models : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToDoLists", "TaskID", "dbo.ToDoTasks");
            DropIndex("dbo.ToDoLists", new[] { "TaskID" });
            AddColumn("dbo.ToDoTasks", "ListID", c => c.Int(nullable: false));
            CreateIndex("dbo.ToDoTasks", "ListID");
            AddForeignKey("dbo.ToDoTasks", "ListID", "dbo.ToDoLists", "ListID", cascadeDelete: true);
            DropColumn("dbo.ToDoLists", "TaskID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ToDoLists", "TaskID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ToDoTasks", "ListID", "dbo.ToDoLists");
            DropIndex("dbo.ToDoTasks", new[] { "ListID" });
            DropColumn("dbo.ToDoTasks", "ListID");
            CreateIndex("dbo.ToDoLists", "TaskID");
            AddForeignKey("dbo.ToDoLists", "TaskID", "dbo.ToDoTasks", "TaskID", cascadeDelete: true);
        }
    }
}
