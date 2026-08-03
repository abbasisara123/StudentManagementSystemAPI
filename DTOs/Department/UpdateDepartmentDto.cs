using System.ComponentModel.DataAnnotations;

namespace StudentManagement.API.DTOs.Department
{
    public class UpdateDepartmentDto
    {
        [Required]
        [MaxLength]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
