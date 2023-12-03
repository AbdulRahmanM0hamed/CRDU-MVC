using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC_DAL.Contexts;
using MVC_DAL.Models;
using MVC3_BLL.interfaces;
using MVC3_BLL.Repository;
using MVCS3PL.MapperProfiles;
using MVCS3PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCS3PL
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Configure Services that allow DI

			builder.Services.AddControllersWithViews();
			
			
			builder.Services.AddDbContext<CompanyDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});


			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
			builder.Services.AddAutoMapper(M => M.AddProfile(new DepartmentProfile()));
			builder.Services.AddAutoMapper(M => M.AddProfile(new UserProfile()));
			builder.Services.AddAutoMapper(M => M.AddProfile(new RoleProfile()));
		

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
			{
				option.Password.RequireNonAlphanumeric = true;
				option.Password.RequireDigit = true;
				option.Password.RequireUppercase = true;

			}).AddEntityFrameworkStores<CompanyDbContext>()
			.AddDefaultTokenProviders();


			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(option =>
				{
					option.LoginPath = "Account/Login";
					option.AccessDeniedPath = "Home/Error";
				});

			#endregion

			var app = builder.Build();

			#region configure http 

			var env = builder.Environment;
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
			#endregion

			app.Run();

		}


	}
}
