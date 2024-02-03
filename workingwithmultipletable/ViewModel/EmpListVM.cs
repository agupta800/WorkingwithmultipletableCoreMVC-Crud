using System.Collections.Generic;
using workingwithmultipletable.Models;

namespace workingwithmultipletable.ViewModel
{
    public class EmpListVM
    {
        public List<Employee> employees { get; set; }
        public List<Department> departments { get; set; }

        public string FullName
        {
            get
            {
                // Assuming the Employee class has FirstName and LastName properties
                if (employees != null && employees.Count > 0)
                {
                    Employee firstEmployee = employees[0]; // Assuming you want the full name of the first employee
                    return $"{firstEmployee.FirstName} {firstEmployee.MiddleName} {firstEmployee.LastName}";
                }
                return string.Empty; // Return an empty string if there are no employees
            }
        }
    }
}
