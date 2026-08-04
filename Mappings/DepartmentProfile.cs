using AutoMapper;
using StudentManagement.API.DTOs.Department;
using StudentManagement.API.DTOs.Student;
using StudentManagement.API.Entities;

namespace StudentManagement.API.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            //Get
            CreateMap<Department, DepartmentDto>();

            //POST
            CreateMap<CreateDepartmentDto, Department>();

            //PUT
            CreateMap<UpdateDepartmentDto, Department>();

        }
    }
}
