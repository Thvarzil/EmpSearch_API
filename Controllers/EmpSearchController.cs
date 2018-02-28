using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmpSearch_API.Models;
using System.Data.Sqlite;


namespace EmpSearch_API.Controllers
{
    [Route("api/[controller]")]
    public class SqlSearchController : Controller
    {
        public SQLiteConnection sQLite;

        public void DataClass(){
            sQLite = new SQLiteConnection("Data Source = emps.db");
        }
        [HttpGet("{searchterm}")]
        public string Get(string searchterm){
            
            // Inserts search term into query
            // NOTE: This is vulnerable to SQL Injection: I'm aware of this issue, not sure how to prevent it. I don't think there's a way
            // to detect if a string is code in a certain language.
            string query = "SELECT * FROM emps WHERE name LIKE '%"+searchterm+"%'";
            string results = "";
            
            
                sQLite.Open();
                

                using(sQLite){
                    SQLiteCommand command = new SQLiteCommand(query, sQLite);
                    SQLiteDataReader reader = command.ExecuteReader();

                    // I will format DB results into HTML, stuff it in a string, and return to the JS, to
                    // then be inserted into the display
                    
                    if(reader.HasRows){
                    // This while loop with the DataReader function is super convenient. 
                    while(reader.Read()){
                        // temporary variable to store formatted HTML, before being concatted to results string
                        string temp_html_builder = "";
                        // console.log as test.
                        Console.WriteLine("ID: "+reader["id"]+" || "+reader["name"]);

                        temp_html_builder = "<div class='row'>"+
                        "<div class='col-md-3'><h4>"+reader["id"]+"</h4></div>"+
                        "<div class='col-md-3'><h4>"+reader["name"]+"</h4></div>"+
                        "</div>";

                        results+=temp_html_builder;


                    }
                    }
                    else{
                        results = "<div class='row'><div class='col-md-6'><h4>There were no results</h4></div</div>";
                    }
                }

            return "You are connected";
        }
    }
}


    }
    public class EmpSearchController : Controller
    {
        private readonly EmpSearchContext _context;

        public EmpSearchController(EmpSearchContext context)
        {
            _context = context;

            if(_context.Employees.Count() == 0)
            {
                _context.Employees.Add(new Employee { FirstName = "Item1", LastName = "Employee", Skill = "MySQL" });
                _context.SaveChanges();
            }
        }
        // GET api/values
       [HttpGet]
        public IEnumerable<Employee> Get(string searchTerm)
        {
            // If route was api/EmpSearch - run trad. GetAll method
        if(searchTerm==null) {
            return _context.Employees.ToList();
        }
        // Else, filter results by the search term.
        return _context.Employees.Where( e => e.FirstName == searchTerm || e.LastName == searchTerm);
        }
    }
}
