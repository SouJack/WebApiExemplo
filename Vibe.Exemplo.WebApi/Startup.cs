using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Caching.Memory;
using Refit;
using Vibe.Exemplo.WebApi.Processos;
using Vibe.Exemplo.WebApi.Servicos;
using Vibe.Transporte.Core;

namespace Vibe.Exemplo.WebApi
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

            services.AddControllers()
                    .AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vibe.Exemplo.WebApi", Version = "v1" });
                c.EnableAnnotations();
            });

            var politicaRetentativas = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                                             .RetryAsync(2, onRetry: (res, tentativas) =>
                                                        {
                                                            Console.Out.WriteLine($"# Request: {res.Result.RequestMessage}");
                                                            Console.Out.WriteLine($"# Content: {res.Result.Content.ReadAsStringAsync().Result}");
                                                            Console.Out.WriteLine($"# ReasonPhrase: {res.Result.ReasonPhrase}");
                                                            Console.Out.WriteLine($"# Retentativa: {tentativas}");
                                                        });

            services.AddRefitClient<IConsultaCepServico>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://viacep.com.br"))
                    .AddPolicyHandler(politicaRetentativas);

            services.AddSingleton<IServicoExemplo, ServicoExemplo>()
                    .AddSingleton<IExemploOrquestrador, ExemploOrquestrador>();
            services//.AddHostedService<SegundoPlanoExemplo>()
                    .AddHostedService<SegundoPlanoExemplo2>();  
            services.AddMemoryCache();

            services.AddSingleton(s =>
            {
                var memoryCache = new MemoryCache(new MemoryCacheOptions());
                var memoryCacheProvider = new MemoryCacheProvider(memoryCache);
                var cachePolicy = Policy.Cache(memoryCacheProvider, TimeSpan.FromMinutes(5));
                return cachePolicy;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vibe.Exemplo.WebApi v1"));
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
