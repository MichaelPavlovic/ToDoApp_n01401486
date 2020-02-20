using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models.ViewModels
{
    public class ShowTask
    {
        //specific task
        public ToDoTask Task { get; set; }

        //specific list associated with that task
        public List List { get; set; }
    }
}