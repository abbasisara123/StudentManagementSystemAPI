using AutoMapper;
using StudentManagement.API.DTOs.User;
using StudentManagement.API.Entities;

namespace StudentManagement.API.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserDto, User>()
                .ForMember(
                dest => dest.PasswordHash,
                opt => opt.Ignore()
                );
        }
    }
}
