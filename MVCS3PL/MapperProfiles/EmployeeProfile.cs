using AutoMapper;
using MVC_DAL.Models;
using MVCS3PL.Models;

namespace MVCS3PL.MapperProfiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
