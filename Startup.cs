using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatNews.Services;
using GreatNews.Models;
using GreatNews.Repository;
using GreatNews.Services;
using GreatNews.UoW;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreatNews
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
            //string connection = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<NewsContext>(options => options.UseSqlServer(connection));
            //services.AddMvc();

            string connection = Configuration.GetConnectionString("DefaultConnection");
           /* services.AddDbContext<NewsContext>(options => options.UseSqlServer(connection));*/

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            services.AddIdentity<UserIdent, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();

            services.AddTransient<IGenericRepository<News>, NewsRepository>();
            
            services.AddTransient<IGenericRepository<Comment>, CommentRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IHtmlArticleServiceS13, ArticleServiceS13>();
            services.AddTransient<IHtmlArticleServiceOnliner, ArticleServiceOnliner>();

            services.AddMvc();



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
                app.UseExceptionHandler("/Home/Error");

            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
