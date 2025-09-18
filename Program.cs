using log4net;
using log4net.Config;
using System.Reflection;
using webapi.Services;
using webapi.Repository;
using webapi.Middleware;
using SqlSugar;
using webapi.Configs;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using webapi.Common;
namespace webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            var builder = WebApplication.CreateBuilder(args);
            // 注册 CacheService
            // 绑定配置
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ValidateLifetime = true
                };
            });
            builder.Services.AddControllers();
            //builder.Services.AddSingleton<SqlSugarService>();
            builder.Services.AddSingleton<JwtHelper>();
            builder.Services.AddScoped<PermissionFilter>();
            builder.Services.AddMemoryCache();

            builder.Services.AddAuthorization();

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<PermissionFilter>(); // 全局权限过滤器
            });
            builder.Services.AddSingleton<ISqlSugarClient>(sp =>
            {
                var config = new ConnectionConfig
                {
                    ConnectionString = builder.Configuration.GetConnectionString("Default"),
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true, 
                    InitKeyType = InitKeyType.Attribute,
                };
                return new SqlSugarScope(config); 
            });
            builder.Services.AddScoped<SqlSugarTransactionHelper>();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<Common.ICacheService, CacheService>();
            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IDeptServices, DeptServices>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            builder.Services.AddScoped<IAppsetingsServices, AppsetingsServices>();

            builder.Services.AddScoped<IBaseDrugServices, BaseDrugServices>();
            builder.Services.AddScoped<IBaseCureServices, BaseCureServices>();
            builder.Services.AddScoped<IBaseMatServices, BaseMatServices>();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //当前是开发环境
            if (builder.Environment.IsDevelopment())
            {
               var db = new SqlSugarScope(new ConnectionConfig
               {
                   ConnectionString = builder.Configuration.GetConnectionString("Default"),
                   DbType = DbType.SqlServer,
                   IsAutoCloseConnection = true,
                   InitKeyType = InitKeyType.Attribute,
               });

               webapi.Tools.DbFirstGenerator.Generate(db);
               webapi.Tools.PermissionScanner.GeneratePermissions(db);
            }
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");


            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
