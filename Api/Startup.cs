using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Context.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Repository.Conta;
using Services.Authenticate;

namespace Api
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
            services.AddControllers();

            services.AddTransient<AuthenticateService>();
            services.AddTransient<IContaRepository, ContaRepository>();

            services.AddDbContext<BibliotecaContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("BibliotecaConnection"));
            });

            var key = Encoding.UTF8.GetBytes(Configuration["Token:Secret"]);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Bearer";

            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters.ValidIssuer = "BIBLIOTECA-API";
                o.TokenValidationParameters.ValidAudience = "BIBLIOTECA-API";
                o.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(key);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
