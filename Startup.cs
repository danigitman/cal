using apical.Interfaces;
using apical.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace apical
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
         
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddOptions();
            var appSettingsOptions = Configuration.GetSection(nameof(AppSettings));
            services.Configure<AppSettings>(appSettingsOptions);
            //INJECTIONS 
            services.AddTransient<IDataService, DataService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
         
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {  
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
