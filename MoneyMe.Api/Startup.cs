using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MoneyMe.Application;
using MoneyMe.Application.Contracts;
using MoneyMe.Domain;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.Repositories;
using MoneyMe.Domain.Services;
using MoneyMe.Infrastructure;
using MoneyMe.Infrastructure.Database;
using MoneyMe.Infrastructure.Repositories;
using MoneyMe.Infrastructure.Services;
using Serilog;

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
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
            });

            services.Configure<Settings>(Configuration.GetSection(nameof(Settings)));

            var logger = new LoggerConfiguration().WriteTo.File("log.txt").CreateLogger();
            services.AddSingleton<ILogger>(logger);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new InfrastructureMappingProfile());
                mc.AddProfile(new ApplicationMappingProfile());
                mc.AddProfile(new ApiMappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<ISecurityService, SecurityService>();

            services.AddSingleton<IQuoteFactory, QuoteFactory>();
            services.AddSingleton<ICustomerFactory, CustomerFactory>();
            services.AddSingleton<ILoanFactory, LoanFactory>();
            services.AddSingleton<IProductFactory, ProductFactory>();
            services.AddSingleton<IFeeFactory, FeeFactory>();
            services.AddSingleton<IRuleFactory, RuleFactory>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IQuoteRepository, QuoteRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IFeeRepository, FeeRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IRuleRepository, RuleRepository>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFeeService, FeeService>();
            services.AddScoped<IRuleService, RuleService>();

            services.AddScoped<IQuoteAggregateService, QuoteAggregateService>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "moneyme",
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                                  });
            });

            services.AddApiVersioning();

            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c=>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MoneyMe API", Version = "v1" });
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

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "api";
            });
        }
    }
}