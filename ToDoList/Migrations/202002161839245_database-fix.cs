namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databasefix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoTasks", "DueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoTasks", "DueDate", c => c.DateTime(nullable: false));
        }
    }
}
