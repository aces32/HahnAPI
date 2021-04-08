using FluentValidation.AspNetCore;
using Hahn.ApplicationProcess.February2021.Data.DBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hahn.ApplicationProcess.February2021.Data.Validators;
using Hahn.ApplicationProcess.February2021.Data.Interfaces;
using Hahn.ApplicationProcess.February2021.Data.Services;
using Hahn.ApplicationProcess.February2021.Domain.ConfigurationSettings;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using System.Reflection;
using System.IO;

namespace Hahn.ApplicationProcess.Web
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
            services.Configure<APIUrlConfigurationSettings>(Configuration.GetSection("APIUrl"));
            services.AddScoped<IValidateCountryRepository, ValidateCountryRepository>();
            services.AddScoped<IRepository<Asset>, AssetRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMvc(setup => {
                //...mvc setup...
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AssetValidator>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn.ApplicationProcess.Application", Version = "v1" });

                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                c.IncludeXmlComments(xmlCommentsFullPath);


            });

            services.AddDbContext<AssetDBContext>(options => options.UseInMemoryDatabase(databaseName: "AssetsData"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();

            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicationProcess.Application v1"));


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
