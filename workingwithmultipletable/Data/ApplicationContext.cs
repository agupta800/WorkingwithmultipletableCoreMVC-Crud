using Microsoft.EntityFrameworkCore;
using workingwithmultipletable.Models;

namespace workingwithmultipletable.Data
{
    public class ApplicationContext :DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }


        public DbSet<Employee> employees { get; set; }
        public DbSet<Department> departments { get; set; }
    }
}
