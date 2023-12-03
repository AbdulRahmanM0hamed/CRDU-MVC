using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MVCS3PL.Models;

namespace MVCS3PL.MapperProfiles
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViweModel,IdentityRole>().
                ForMember(d=>d.Name ,o=>o.MapFrom(s=>s.RoleName))
                .ReverseMap();
        }
    }
}
