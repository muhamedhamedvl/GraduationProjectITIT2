using CourseManagement.BLL.Interfaces;
using CourseManagement.BLL.Services;
using CourseManagement.BLL.Services.Interfaces;
using CourseManagement.DAL.Data;
using CourseManagement.DAL.Interfaces;
using CourseManagement.DAL.Repositores;
using CourseManagement.DAL.Repositores.Interfaces;
using CourseManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
                            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IGradeService, GradeService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<IGradeRepository, GradeRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();


            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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
