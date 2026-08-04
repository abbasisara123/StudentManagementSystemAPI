namespace StudentManagement.API.Entities
{
    public class Department
    {
        public int  Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public ICollection<Student> Students { get; set; } = new List<Student>();   //Navigation Property
    }
}
