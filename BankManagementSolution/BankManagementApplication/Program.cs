using BankManagementApplication.Context;
using BankManagementApplication.Interfaces.Repositories;
using BankManagementApplication.Interfaces.Services;
using BankManagementApplication.Repositories;
using BankManagementApplication.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace BankManagementApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        RoleClaimType = "Roles",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
                    };
                });
            #endregion

            #region Contexts
            builder.Services.AddDbContext<BankManagementContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Services and Repositories and Controllers
            
            builder.Services.AddScoped<ICustomerService, CustomerService>();
      

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
           

            builder.Services.AddControllers();
            #endregion

            builder.Services.AddAutoMapper(typeof(Program)); // Use the Program class to scan for AutoMapper profiles

            #region CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            #endregion


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BankManagementApplication", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
