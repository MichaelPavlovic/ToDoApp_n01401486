namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class task : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoTasks", "Completed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoTasks", "Completed");
        }
    }
}
