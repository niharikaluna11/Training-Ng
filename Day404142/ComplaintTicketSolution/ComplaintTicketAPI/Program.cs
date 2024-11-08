using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Repositories;
using ComplaintTicketAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace ComplaintTicketAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Builder
            var builder = WebApplication.CreateBuilder(args);
            #endregion

            #region Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
                    };
                });
            #endregion

            #region AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));
            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<int, Complaint>, ComplaintRepository>();
            builder.Services.AddScoped<IRepository<int, ComplaintFile>, ComplaintFileRepository>();
            builder.Services.AddScoped<IRepository<int, ComplaintCategory>, ComplaintCategoryRepository>();
            builder.Services.AddScoped<IRepository<int, ComplaintStatus>, ComplaintStatusRepository>();
            builder.Services.AddScoped<IRepository<int, ComplaintStatusDate>, ComplaintStatusDateRepository>();

            builder.Services.AddScoped<IRepository<int, UserProfile>, UserProfileRepository>();
            builder.Services.AddScoped<IRepository<string, User>, UserRepository>();
            builder.Services.AddScoped<IRepository<int, Organization>, OrganizationRepository>();
            #endregion

            #region Services    
            builder.Services.AddScoped<IOrganizationService, OrganizationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IComplaintCategoryService, ComplaintCategoryService>();
            builder.Services.AddScoped<IComplaintService, ComplaintService>();

            builder.Services.AddScoped<IUserProfileService, UserProfileService>();
            builder.Services.AddScoped<IOrganizationProfileService, OrganizationProfileService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            // Register UpdateComplaintService
            builder.Services.AddScoped<IUpdateComplaintService, UpdateComplaintService>();
            #endregion




            #region Contexts
            builder.Services.AddDbContext<ComplaintTicketContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Logger
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
            // loggerFactory.LogInformation("Starting up the application...");
            #endregion


            #region Swagger
            // Configure services
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Set ReferenceHandler.Preserve to handle circular references
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.MaxDepth = 64; // Optional: increase if needed
                });

            // Learn more about configuring Swagger/OpenAPI
            // at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
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

            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}