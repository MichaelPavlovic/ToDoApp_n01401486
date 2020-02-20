using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ToDoList.Data
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext() : base("name=ToDoListContext")
        {

        }

        public System.Data.Entity.DbSet<ToDoList.Models.ToDoTask> Tasks { get; set; }
        public System.Data.Entity.DbSet<ToDoList.Models.List> Lists { get; set; }
    }
}