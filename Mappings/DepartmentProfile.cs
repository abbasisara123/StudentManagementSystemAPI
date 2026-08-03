using AutoMapper;
using StudentManagement.API.DTOs.Department;
using StudentManagement.API.Entities;

namespace StudentManagement.API.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            //POST
            CreateMap<CreateDepartmentDto, Department>();

            //Get
            CreateMap<Department, DepartmentDto>().ReverseMap();
            //PUT
            CreateMap<UpdateDepartmentDto, Department>();

        }
    }
}
