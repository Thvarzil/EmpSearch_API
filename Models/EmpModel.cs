
using System;
using System.Collections.Generic;

namespace EmpSearch_API.Models
{
    public class Employee
    {
        // Creates Employee table model with Id, First Name, Last Name, and Skillset columns
        public long Id {get; set;}
        public string FirstName { get; set; }
        public string LastName {get; set;}
        public string Skill { get; set; }

    }
}