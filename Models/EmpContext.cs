using Microsoft.EntityFrameworkCore;


namespace EmpSearch_API.Models
{

    public class EmpSearchContext : DbContext
    {
        public EmpSearchContext(DbContextOptions<EmpSearchContext> options)
            : base(options)
            {
            }
        
        public DbSet<Employee>Employees{get; set;}
    }
}