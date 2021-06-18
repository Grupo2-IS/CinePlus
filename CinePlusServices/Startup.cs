using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using CinePlus.Context;
using CinePlus.Context.Repositories;
using CinePlus.Entities;
using CinePlus.Helpers;
using CinePlus.Authorization;
using Microsoft.AspNetCore.Identity;
using BCryptNet = BCrypt.Net.BCrypt;




namespace CinePlus.Services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CinePlusDb>(options => options.UseSqlServer(@"Server=(localDB)\MSSQLLocalDB;Database=CinePlusDB;Integrated Security=true;"));
            services.AddCors(options =>
                options.AddDefaultPolicy(p => p.AllowAnyOrigin()));

            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CinePlusServices", Version = "v1" });
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<CinePlusDb>();

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRepository<Film>, FilmRepository>();
            services.AddScoped<IRepository<Room>, RoomRepository>();
            services.AddScoped<IRepository<Seat>, SeatRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Artist>, ArtistRepository>();
            services.AddScoped<IMemberPurchaseRepository, MemberPurchaseRepository>();
            services.AddScoped<INormalPurchaseRepository, NormalPurchaseRepository>();
            services.AddScoped<IShowingRepository, ShowingRepository>();
            services.AddScoped<IRepository<Member>, MemberRepository>();
            services.AddScoped<IPerformerRepository, PerformerRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();


            services.AddAuthentication();


            services.AddAuthorization();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CinePlusDb context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CinePlusServices v1"));
            }
            createTestUser(context);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(policy => policy
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseMiddleware<JwtMiddleware>();
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void createTestUser(CinePlusDb context)
        {
            // add hardcoded test user to db on startup
            var testUser = new User
            {
                Name = "Test",
                Nick = "test",
                PasswordHash = BCryptNet.HashPassword("test")
            };
            context.Users.Add(testUser);
            context.SaveChanges();
        }
    }
}
