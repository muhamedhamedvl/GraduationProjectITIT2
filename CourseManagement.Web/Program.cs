using CourseManagement.BLL.Services;
using CourseManagement.BLL.Services.Interfaces;
using CourseManagement.DAL.Data;
using CourseManagement.DAL.Repositores;
using CourseManagement.DAL.Repositores.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
                            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Services and Repositories
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IInstructorService, InstructorService>();

            builder.Services.AddControllersWithViews();
            var app = builder.Build();
           

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
