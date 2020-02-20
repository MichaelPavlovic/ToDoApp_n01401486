using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models.ViewModels
{
    public class AddTask
    {
        //info about a task
        public List<ToDoTask> Tasks { get; set; }

        //info about the list its associated with
        public List List { get; set; }
    }
}