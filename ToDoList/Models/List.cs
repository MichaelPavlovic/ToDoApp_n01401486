using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    public class List
    {
        [Key]
        public int ListID { get; set; }
        public string Title { get; set; }

        //reference list (one list to many tasks)
        public ICollection<ToDoTask> ToDoTasks { get; set; }

        //reference user who made list and users that have access to the list (many users to many lists)

    }
}