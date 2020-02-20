using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    public class ToDoTask
    {
        [Key]
        public int TaskID { get; set; }
        public string Task { get; set; }
        public string DueDate { get; set; }
        public bool Completed { get; set; }
        //reference list (one list to many tasks)
        public int ListID { get; set; }
        [ForeignKey("ListID")]
        public virtual List ToDoList { get; set; }
    }
}