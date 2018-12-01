﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using quizartsocial_backend;
using quizartsocial_backend.Models;
using Swashbuckle.AspNetCore.Swagger;
namespace backEnd
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
           // services.AddDbContext<efmodel>();
            var connString = Environment.GetEnvironmentVariable("SQLSERVER_HOST") ?? "Server=localhost\\SQLEXPRESS;Database=QuizRTSocialDb;Trusted_Connection=True;";
            services.AddDbContext<SocialContext>(options => options.UseSqlServer(connString));
            Console.WriteLine("dfkadjakjsdkajdajdskasdjaksdsdssssssssss"+connString);
            services.AddScoped<ITopic, TopicRepo>();
            services.AddSingleton<GraphDbConnection>();

            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CORS",
                corsPolicyBuilder => corsPolicyBuilder
                // Apply CORS policy for any type of origin
                .AllowAnyMethod()
                // Apply CORS policy for any type of http methods
                .AllowAnyHeader()
                // Apply CORS policy for any headers
                .AllowCredentials()
                .AllowAnyOrigin()
                // .WithOrigins ("http://localhost:4200","http:localhost:4201")                
                );
                // Apply CORS policy for all users

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors("CORS");
            // app.UseHttpsRedirection();
            app.UseMvc();
            
      
        }
    }
}
