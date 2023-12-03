using AutoMapper;
using MVC_DAL.Models;
using MVCS3PL.Models;

namespace MVCS3PL.MapperProfiles
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
