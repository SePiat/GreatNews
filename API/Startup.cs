using API.Filters;
using CORE_;
using GreatNews;
using GreatNews.Models;
using GreatNews.Repository;
using GreatNews.Services;
using GreatNews.UoW;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API
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
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GreatNews", Version = "v1" });
            });

            

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            services.AddIdentity<UserIdent, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();

            //add hangfire
            services.AddHangfire(config =>
                     config.UseSqlServerStorage(Configuration.GetConnectionString("IdentityConnection")));



            services.AddTransient<IGenericRepository<News>, NewsRepository>();
            services.AddTransient<IGenericRepository<Comment>, CommentRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IHtmlArticleServiceS13, ArticleServiceS13>();
            services.AddTransient<IHtmlArticleServiceOnliner, ArticleServiceOnliner>();
            services.AddTransient<INewsGetterService, NewsGetterService>();


            services.AddTransient<IAFINNService, AFINNService>();
            services.AddTransient<ILemmatizationService, LemmatizationService>();
            services.AddTransient<IPositivityIndexService, PositivityIndexService>();




            //Regisetr Mediatr

            services.AddMediatR(AppDomain.CurrentDomain.Load("MediatR_"));
            services.AddTransient<IMediator, Mediator>();

            //Add JWT
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))

                    };
                });
            

            services.AddMvc();
            //доступ к сервису откуда угодно
            services.AddCors(options =>
            {
                options.AddPolicy("CORS_Policy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, INewsGetterService getnews, IPositivityIndexService getPosIndex)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GreatNews V1");
            });

            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            app.UseCors("CORS_Policy");
            app.UseAuthentication();
            app.UseMvc();


            
            //Hangfire
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });
            
            RecurringJob.AddOrUpdate(
                () => getnews.AutoRefresh(),
                Cron.Hourly);
            RecurringJob.AddOrUpdate(
                () => getPosIndex.AddPsitiveIndexToNews(),
                Cron.Hourly(30));
        }
    }
}
