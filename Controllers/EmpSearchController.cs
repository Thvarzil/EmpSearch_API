using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmpSearch_API.Models;

namespace EmpSearch_API.Controllers
{
    [Route("api/[controller]")]
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
