using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MoneyMe.Application;
using MoneyMe.Application.Contracts;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Repositories;
using MoneyMe.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMe.Api
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

            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<ISecurityService, SecurityService>();

            services.AddSingleton<IQuoteFactory, QuoteFactory>();
            services.AddSingleton<ICustomerFactory, CustomerFactory>();

            services.AddSingleton<IQuoteRepository, QuoteRepository>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();

            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IQuoteService, QuoteService>();
            services.AddSingleton<ILoanService, LoanService>();


            services.AddCors(options =>
            {
                options.AddPolicy(name: "moneyme",
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                                  });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("moneyme");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}