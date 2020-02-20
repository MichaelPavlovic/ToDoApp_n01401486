using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Entity;
using ToDoList.Data;
using ToDoList.Models;
using System.Diagnostics;
using System.Net;
using ToDoList.Models.ViewModels;

namespace ToDoList.Controllers
{
    public class ToDoTaskController : Controller
    {

        //controller was built based off of this provided repository https://github.com/christinebittle/PetGroomingMVC

        private ToDoListContext db = new ToDoListContext();

        // GET: ToDoTask
        public ActionResult Index(string searchkey)
        {
            //get a list of tasks from the database
            List<ToDoTask> tasks = db.Tasks.SqlQuery("SELECT * FROM ToDoTasks").ToList();
            return View(tasks);
        }
        public ActionResult Show(int? id)
        {
            //if id is null return a bad http request
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //get the task from the database using the id passed in
            ToDoTask Task = db.Tasks.SqlQuery("SELECT * FROM ToDoTasks WHERE TaskID=@TaskID", new SqlParameter("@TaskID", id)).FirstOrDefault();

            //if the task does not exist return http not found error
            if(Task == null)
            {
                return HttpNotFound();
            }

            return View(Task);
        }

        //Creating a new task
        public ActionResult Add(int? id)
        {
            //if id is null return a bad http request
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //get the list to add the task to
            List selectedList = db.Lists.SqlQuery("SELECT * FROM Lists WHERE ListID = @ListID", new SqlParameter("@ListID", id)).FirstOrDefault();
            //get a list of tasks
            List<ToDoTask> Tasks = db.Tasks.SqlQuery("SELECT * FROM ToDoTasks").ToList();

            //create a view model and bind the list and tasks to it
            AddTask AddTaskViewModel = new AddTask();
            AddTaskViewModel.Tasks = Tasks;
            AddTaskViewModel.List = selectedList;

            //return the view model
            return View(AddTaskViewModel);
        }
        [HttpPost]
        public ActionResult Add(int? id, string Task, string DueDate, int ListID)
        {
            Debug.WriteLine("Creating a new task " + Task);

            //create sql query to add a new task
            string query = "INSERT INTO ToDoTasks (Task, DueDate, ListID) VALUES (@Task, @DueDate, @ListID)";

            //bind paramaters to passed in values
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@Task", Task);
            sqlparams[1] = new SqlParameter("@DueDate", DueDate);
            sqlparams[2] = new SqlParameter("@ListID", ListID);

            Debug.WriteLine("Attempting to execute " + query);

            //execute query
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("../List/Show/" + ListID);
        }

        //update a task
        public ActionResult Update(int id)
        {
            //get the selected task to update
            ToDoTask selectedTask = db.Tasks.SqlQuery("SELECT * FROM ToDoTasks WHERE TaskID = @TaskID", new SqlParameter("@TaskID", id)).FirstOrDefault();

            //get the list that is associated with the selected task
            List list = db.Lists.SqlQuery("SELECT * FROM Lists INNER JOIN ToDoTasks ON Lists.ListID = ToDoTasks.ListID WHERE ToDoTasks.TaskID = @TaskID", 
                new SqlParameter("TaskID", id)).FirstOrDefault();

            //bind task and list to view model
            ShowTask ShowTaskViewModel = new ShowTask();
            ShowTaskViewModel.Task = selectedTask;
            ShowTaskViewModel.List = list;

            return View(ShowTaskViewModel);
        }
        [HttpPost]
        public ActionResult Update(int id, string Task, string DueDate, bool Completed, int ListID)
        {
            Debug.WriteLine("Attempting to update " + Task);

            //create query to update task
            string query = "UPDATE ToDoTasks SET Task = @Task, DueDate = @DueDate, Completed = @Completed WHERE TaskID = @TaskID";

            //bind the passed in parameters
            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@Task", Task);
            sqlparams[1] = new SqlParameter("@DueDate", DueDate);
            sqlparams[2] = new SqlParameter("@Completed", Completed);
            sqlparams[3] = new SqlParameter("@TaskID", id);

            Debug.WriteLine("Attempting to run " + query);

            //execute query
            db.Database.ExecuteSqlCommand(query, sqlparams);

            //redirect to the associated list
            return RedirectToAction("/Show/" + id);
        }
        //TODO
        //custom update for being able to set a task as completed or not completed on the List Index page by checkboxes
        [HttpPost]
        public ActionResult ListUpdate(int? id)
        {
            //TODO: logic for updating a task

            return View();
        }

        //delete a task
        public ActionResult DeleteConfirm(int id)
        {
            //get the selected task to update
            ToDoTask selectedTask = db.Tasks.SqlQuery("SELECT * FROM ToDoTasks WHERE TaskID = @TaskID", new SqlParameter("@TaskID", id)).FirstOrDefault();

            //get the list that is associated with the selected task
            List list = db.Lists.SqlQuery("SELECT * FROM Lists INNER JOIN ToDoTasks ON Lists.ListID = ToDoTasks.ListID WHERE ToDoTasks.TaskID = @TaskID",
                new SqlParameter("TaskID", id)).FirstOrDefault();

            ShowTask ShowTaskViewModel = new ShowTask();
            ShowTaskViewModel.Task = selectedTask;
            ShowTaskViewModel.List = list;

            return View(ShowTaskViewModel);
        }
        [HttpPost]
        public ActionResult Delete(int id, int ListID)
        {
            Debug.WriteLine("Attemping to delete task " + id);
            
            //create query to delete a task
            string query = "DELETE FROM ToDoTasks WHERE TaskID = @TaskID";

            //bind passed in parameter
            SqlParameter param = new SqlParameter("@TaskID", id);

            Debug.WriteLine("Attempting to run " + query);

            //execute query
            db.Database.ExecuteSqlCommand(query, param);

            //redirect
            return RedirectToAction("../List/Show/" + ListID);
        }
    }
}