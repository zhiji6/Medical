using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;
using SY.Com.Medical.WebApi.Filter;
using SY.Com.Medical.WebApi.Format;
using SY.Com.Medical.WebApi.JWT;
using System;
using System.IO;
using System.Text.Json.Serialization;

namespace SY.Com.Medical.WebApi
{
    /// <summary>
    /// 启动
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置器属性
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //配置JWT,Bearer JWT
            services.AddAuthentication("Bearer")
            .AddJwtBearer(options => options.TokenValidationParameters = JWTTokenValidationParameters.getParameters());

            services.AddControllers(options => options.Filters.Add(new CustomerFilter()))
                .AddJsonOptions(option=> {
                    option.JsonSerializerOptions.Converters.Add(new DateConverter());
                    option.JsonSerializerOptions.Converters.Add(new DateTimeNullableConverter());
                    option.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
            services.AddMvc(opt =>
            {
                opt.Filters.Add<ExceptionFilter>();
            }).AddJsonOptions(option =>
            {
                //原样输出,默认会把首字母小写
                //option.JsonSerializerOptions.PropertyNamingPolicy = null;
                option.JsonSerializerOptions.Converters.Add(new DateConverter());
                option.JsonSerializerOptions.Converters.Add(new DateTimeNullableConverter());
                option.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
            //配置跨域处理
            services.AddCors(options =>
            {
                //any
                options.AddPolicy("abcany", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader();      //.AllowCredentials();//指定处理cookie                                  
                });
            });
            
            //配置Swagger
            services.AddSwaggerGen(c =>
            {
                //启用swagger验证功能
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                //添加全局安全条件
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"}
                        },new string[] { }
                    }
                });
                //显示自定义的Heard Token
                c.OperationFilter<AuthTokenHeaderParameter>();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "接口文档",
                    Description = "RESTful API for TwBusManagement"
                });
                c.OperationFilter<AddTenantIdHeaderParameter>();
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "SY.Com.Medical.WebApi.xml");//Api
                var xmlPath2 = Path.Combine(basePath, "SY.Com.Medical.Model.xml");//Model
                var xmlPath3 = Path.Combine(basePath, "SY.Com.Medical.Enum.xml");//Enum
                c.IncludeXmlComments(xmlPath);
                c.IncludeXmlComments(xmlPath2);
                c.IncludeXmlComments(xmlPath3);
                //
            });

            //配置枚举返回
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });
        }

        /// <summary>
        /// 注册配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //添加MIME
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".grf"] = "grf/gridreport";
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
            //app.UseStaticFiles(); //静态文件服务            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TwBusManagement API V1");
                //c.ShowExtensions();
            });
            app.UseRouting();            
            app.UseRequestLocalization();   
            app.UseAuthentication();//JWT验证  
            app.UseAuthorization();
            app.UseCors("abcany");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
