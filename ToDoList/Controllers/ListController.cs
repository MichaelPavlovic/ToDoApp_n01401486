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
    public class ListController : Controller
    {

        //controller was built based off of this provided repository https://github.com/christinebittle/PetGroomingMVC

        private ToDoListContext db = new ToDoListContext();
        // GET: ToDoList
        public ActionResult Index(string searchkey)
        {
            //write the basic query
            string query = "SELECT * FROM Lists ";

            //if the query is not empty
            if(searchkey != "")
            {
                //add the searchkey onto the basic query
                query += "WHERE Title LIKE '%" + searchkey + "%'";
                Debug.WriteLine("Attempting to search " + searchkey);
            }

            //get a list of lists from the database
            List<List> lists = db.Lists.SqlQuery(query).ToList();
            return View(lists);
        }
        public ActionResult Show(int? id)
        {
            //if the id does not exist return http bad request error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //get the list from the database using the passed in id
            List List = db.Lists.SqlQuery("SELECT * FROM Lists WHERE ListID=@ListID", new SqlParameter("@ListID", id)).FirstOrDefault();

            //if the list does not exist return http not found error
            if(List == null)
            {
                return HttpNotFound();
            }

            //get a list of tasks associated with the specific list
            List<ToDoTask> Tasks = db.Tasks.SqlQuery("SELECT * FROM ToDoTasks WHERE ListID = @ListID", new SqlParameter("@ListID", id)).ToList();

            ShowTasks ShowTaskViewModel = new ShowTasks();
            ShowTaskViewModel.List = List;
            ShowTaskViewModel.ToDoTasks = Tasks;

            return View(ShowTaskViewModel);
        }

        //creating a new list
        public ActionResult Add()
        {
            //get existing lists
            List<List> List = db.Lists.SqlQuery("SELECT * FROM Lists").ToList();
            return View(List);
        }
        [HttpPost]
        public ActionResult Add(string Title)
        {
            Debug.WriteLine("Attemping to create a new list called " + Title);

            //create query to add new list to the database
            string query = "INSERT INTO Lists (Title) values (@Title)";
            //bind the passed in id for the query
            var param = new SqlParameter("@Title", Title);

            Debug.WriteLine("Attemping to run " + query);

            //execute the sql query
            db.Database.ExecuteSqlCommand(query, param);

            //redirect to the index page when it is added
            return RedirectToAction("Index");
        }

        //updating a list
        public ActionResult Update(int id)
        {
            //get the list from the database using the passed in id
            string query = "SELECT * FROM Lists WHERE ListID = @ListID";
            //bind the passed in id for the query
            var param = new SqlParameter("@ListID", id);

            //create a variable to hold the list and return it
            List list = db.Lists.SqlQuery(query, param).FirstOrDefault();
            return View(list);
        }
        [HttpPost]
        public ActionResult Update(int id, string Title)
        {
            Debug.WriteLine("Attemping to update " + Title);

            //create the query to update the list
            string query = "UPDATE Lists SET Title = @Title WHERE ListID = @ListID";
            //bind the passed in values for the query
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@Title", Title);
            sqlparams[1] = new SqlParameter("@ListID", id);

            Debug.WriteLine("Attemping to run " + query);

            //execute the query
            db.Database.ExecuteSqlCommand(query, sqlparams);

            //redirect to the index page when it is updated
            return RedirectToAction("/Show/" + id);
        }

        //deleting a list
        public ActionResult Delete(int id)
        {
            Debug.WriteLine("Attemping to delete list " + id);
            
            //create the query to delete the list using the passed in id
            string query = "DELETE FROM Lists WHERE ListID = @ListID";
            //the passed in id for the query
            SqlParameter param = new SqlParameter("@ListID", id);

            Debug.WriteLine("Attemping to run " + query);

            //execute the query
            db.Database.ExecuteSqlCommand(query, param);

            //delete all tasks that are in the list
            string query2 = "DELETE FROM ToDoTasks WHERE ListID = @ListID";

            Debug.WriteLine("Attemping to run " + query2);

            //execute query
            db.Database.ExecuteSqlCommand(query2, param);

            //redirect to the index page when it is deleted
            return RedirectToAction("Index");
        }
    }
}