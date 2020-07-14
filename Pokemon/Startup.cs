using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokemon.Business.Interfaces;
using Pokemon.Business.Services;
using Pokemon.Data.Configuration;
using Pokemon.Data.Http;
using Pokemon.Data.Intefaces;
using Pokemon.Data.Repositories;

namespace Pokemon
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
            services.Configure<PokemonAPIConfig>(Configuration.GetSection("PokemonApi"));
            services.Configure<TranslationAPIConfig>(Configuration.GetSection("ShakespeareTranslationApi"));

            services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();
            services.AddTransient<IEscapeService, EscapeService>();
            services.AddTransient<IPokeAPIRepository, PokeAPIRepository>();
            services.AddTransient<IShakespeareTranslatorRepository, ShakespeareTranslatorRepository>();
            services.AddTransient<IPokeShakespeareTranslationService, PokeShakespeareTranslationService>();

            services.AddControllers();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
