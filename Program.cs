using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SqlSugar;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using webapi.Common;
using webapi.Configs;
using webapi.Jobs;
using webapi.Middleware;
using webapi.Models.LIS;
using webapi.Repository;
using webapi.Services;
using webapi.Services.Lis;
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
            builder.Services.AddSingleton<SqlSugarScope>(sp =>
            {
                var configs = new List<ConnectionConfig>
                {
        new ConnectionConfig { ConfigId = "BaseData", ConnectionString = builder.Configuration.GetConnectionString("BaseData"), DbType = DbType.SqlServer, IsAutoCloseConnection = true, InitKeyType = InitKeyType.Attribute },
        new ConnectionConfig { ConfigId = "LIS", ConnectionString = builder.Configuration.GetConnectionString("LIS"), DbType = DbType.SqlServer, IsAutoCloseConnection = true, InitKeyType = InitKeyType.Attribute }
                };
                return new SqlSugarScope(configs);
            });
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddSingleton<Func<string, ISqlSugarClient>>(sp =>
            {
                var scope = sp.GetRequiredService<SqlSugarScope>();
                return dbName => scope.GetConnectionScope(dbName);
            });

            builder.Services.AddScoped<SqlSugarTransactionHelper>();
            builder.Services.AddScoped<Common.ICacheService, CacheService>();
            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IDeptServices, DeptServices>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            builder.Services.AddScoped<IAppsetingsServices, AppsetingsServices>();

            builder.Services.AddScoped<IBaseDrugServices, BaseDrugServices>();
            builder.Services.AddScoped<IBaseCureServices, BaseCureServices>();
            builder.Services.AddScoped<IBaseMatServices, BaseMatServices>();

            builder.Services.AddScoped<IWwfPersonServices, WwfPersonServices>();
            builder.Services.AddScoped<IWwfDeptServices, WwfDeptServices>();
            builder.Services.AddScoped<IWwfSysServices, WwfSysServices>();

            builder.Services.AddScoped<IWwfOrgFuncServices, WwfOrgFuncServices>();
            builder.Services.AddScoped<IWwfFuncServices, WwfFuncServices>();
            builder.Services.AddScoped<IWwfOrgServices, WwfOrgServices>();
            builder.Services.AddScoped<IWwfPositionServices, WwfPositionServices>();

            builder.Services.AddScoped<ISamUrgentvaluepromptServices, SamUrgentvaluepromptServices>();
            builder.Services.AddScoped<ISamApplyServices, SamApplyServices>();

            builder.Services.AddScoped<ISamSampleItemServices, SamSampleItemServices>();
            builder.Services.AddScoped<ISamSampleTypeServices, SamSampleTypeServices>();
            builder.Services.AddScoped<ISamTypeServices, SamTypeServices>();
            builder.Services.AddScoped<ISamItemServices, SamItemServices>();
            builder.Services.AddScoped<ISamInstrServices, SamInstrServices>();
            builder.Services.AddScoped<ISamCheckTypeServices, SamCheckTypeServices>();

            builder.Services.AddScoped<ISamCheckTypeServices, SamCheckTypeServices>();





            builder.Services.AddScoped<IDictionaryService, DictionaryService>();



            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //当前是开发环境
            if (builder.Environment.IsDevelopment())
            {
                var configs = new List<ConnectionConfig>
                 {
                     new ConnectionConfig
                     {
                         ConfigId = "BaseData",
                         ConnectionString = builder.Configuration.GetConnectionString("BaseData"),
                         DbType = DbType.SqlServer,
                         IsAutoCloseConnection = true,
                         InitKeyType = InitKeyType.Attribute
                     },
                     new ConnectionConfig
                     {
                         ConfigId = "LIS",
                         ConnectionString = builder.Configuration.GetConnectionString("LIS"),
                         DbType = DbType.SqlServer,
                         IsAutoCloseConnection = true,
                         InitKeyType = InitKeyType.Attribute
                     }
                 };

                var db = new SqlSugarScope(configs);

                webapi.Tools.DbFirstGenerator.Generate(db); // ✅ 一次性生成两个库的实体
                webapi.Tools.PermissionScanner.GeneratePermissions(db.GetConnectionScope("BaseData"));
            }
            // builder.Services.AddHostedService<BackgroundJob>(); //定时任务
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
