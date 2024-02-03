using System.ComponentModel.DataAnnotations;

namespace workingwithmultipletable.Models
{
    public class Department
    {
        [Key]
        [Required]
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
    }
}
