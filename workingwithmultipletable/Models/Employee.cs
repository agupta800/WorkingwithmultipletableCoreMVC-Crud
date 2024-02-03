using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace workingwithmultipletable.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        public  string FirstName { get; set; }  
        public  string? MiddleName { get; set; }  

        public string? LastName { get; set; }
        public string? Gender { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}
