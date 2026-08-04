using System.ComponentModel.DataAnnotations;

namespace StudentManagement.API.DTOs.Department
{
    public class CreateDepartmentDto
    {
        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

    }
}
