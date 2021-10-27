using IdentityServer4.Test;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Id4test
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
            services.AddIdentityServer()//注册服务
              //.AddDeveloperSigningCredential()
              .AddSigningCredential(new X509Certificate2("chester.pfx","123456") )
              .AddInMemoryApiResources(Config.GetApiResources())//配置类定义的授权范围
              .AddInMemoryClients(Config.GetClients())//配置类定义的授权客户端
               .AddInMemoryApiScopes(Config.ApiScopes)
              .AddTestUsers(new List<TestUser> { new TestUser { Username = "Admin", Password = "123456", SubjectId = "001", IsActive = true } });//模拟测试用户，这里偷懒了，用户可以单独管理，最好不要直接在这里New
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();//添加中间件
            app.UseRouting();
         

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
