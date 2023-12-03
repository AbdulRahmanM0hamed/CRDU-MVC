using AutoMapper;
using MVC_DAL.Models;
using MVCS3PL.Models;

namespace MVCS3PL.MapperProfiles
{
	public class UserProfile:Profile
	{
		public UserProfile()
		{
			CreateMap<ApplicationUser, UserViewModel>().ReverseMap();	
		}
	}
}
