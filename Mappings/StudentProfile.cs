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
            CreateMap<Student,StudentDto>();

            //Post
            CreateMap<CreateStudentDto, Student>();

            //Put (Update)
            CreateMap<UpdateStudentDto, Student>();
        }
    }
}
