using System.ComponentModel.DataAnnotations;

namespace StudentManagement.API.DTOs.Student
{
    public class CreateStudentDto
    {
        [Required]
        [MaxLength(100)]
        public string studentName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Range(16, 100)]
        public int Age { get; set; } 
        [Required]
        public int DepartmentId { get; set; } 
    }
}
