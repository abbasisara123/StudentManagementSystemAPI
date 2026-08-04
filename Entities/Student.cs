namespace StudentManagement.API.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string  studentName { get; set; }
        public string  Email { get; set; }
        public int Age { get; set; }
        public int DepartmentId { get; set; }   //Forgein Key    
        public Department Department { get; set; }  //One To Many relation       //Navigation Property   
    }
}
