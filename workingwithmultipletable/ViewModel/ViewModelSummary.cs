using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace workingwithmultipletable.ViewModel
{
    public class ViewModelSummary
    {
        [Key]
        public int EmpId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + MiddleName + " " + LastName;
            }
        }

        [Required]
        public string DepartmentName { get; set; }

        // Add appropriate data annotations for DepartmentCode based on your requirements
        [Required]
        public string DepartmentCode { get; set; }
    }
}
