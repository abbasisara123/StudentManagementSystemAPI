using AutoMapper;
using StudentManagement.API.DTOs.Student;
using StudentManagement.API.Entities;

namespace StudentManagement.API.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            //Get
            CreateMap<Student,StudentDto>().ForMember(
                dest=> dest.DepartmentName, //Student table ka department name    (idhr copied hoga)
                opt=>opt.MapFrom(src=>src.Department.DepartmentName)); //Student table ky andr jo department object usky andr sy department name la kr du 

            //Post
            CreateMap<CreateStudentDto, Student>();

            //Put (Update)
            CreateMap<UpdateStudentDto, Student>();
        }
    }
}
