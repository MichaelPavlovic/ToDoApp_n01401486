namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ToDoLists", newName: "Lists");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Lists", newName: "ToDoLists");
        }
    }
}
