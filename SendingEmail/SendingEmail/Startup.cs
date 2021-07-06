using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SE.DAL.EF;
using SE.BLL.Interfaces;
using SE.BLL.Services;
using SE.BLL.Mapping;
using SE.BLL.SMTP;
using Microsoft.OpenApi.Models;
using System.IO;

namespace SendingEmail
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SubmissionResultContext>(options => options.UseNpgsql(connection));

            services.AddAutoMapper(c => c.AddProfile<MappingProfile>(), typeof(Startup));
            services.Configure<EmailSettings>(Configuration.GetSection(EmailSettings.SectionName));
            services.AddOptions();
            services.AddTransient<IEmailService, EmailService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Report API", Version = "v1" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "SendingEmail.xml");
                var filePath1 = Path.Combine(System.AppContext.BaseDirectory, "SE.BLL.xml");

                c.IncludeXmlComments(filePath);
                c.IncludeXmlComments(filePath1);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SendEmail v1");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
