using AutoMapper;
using EmployeeManagement.DTOs;
using EmployeeManagement.Entities;

namespace EmployeeManagement.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));

            CreateMap<EmployeeDto, Employee>();
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (today < dateOfBirth.AddYears(age))
                age--;
            return age;
        }
    }

}
