using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models.ViewModels
{
    public class ShowTasks
    {
        //specific list
        public List List { get; set; }

        //list of tasks associated with the list
        public List<ToDoTask> ToDoTasks { get; set; }
    }
}