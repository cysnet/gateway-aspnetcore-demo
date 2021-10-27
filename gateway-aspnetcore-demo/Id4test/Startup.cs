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
            services.AddIdentityServer()//ע�����
              //.AddDeveloperSigningCredential()
              .AddSigningCredential(new X509Certificate2("chester.pfx","123456") )
              .AddInMemoryApiResources(Config.GetApiResources())//�����ඨ�����Ȩ��Χ
              .AddInMemoryClients(Config.GetClients())//�����ඨ�����Ȩ�ͻ���
               .AddInMemoryApiScopes(Config.ApiScopes)
              .AddTestUsers(new List<TestUser> { new TestUser { Username = "Admin", Password = "123456", SubjectId = "001", IsActive = true } });//ģ������û�������͵���ˣ��û����Ե���������ò�Ҫֱ��������New
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();//����м��
            app.UseRouting();
         

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
