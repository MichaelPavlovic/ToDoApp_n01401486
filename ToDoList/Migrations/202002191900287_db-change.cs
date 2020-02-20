namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbchange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoTasks", "DueDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoTasks", "DueDate", c => c.DateTime());
        }
    }
}
