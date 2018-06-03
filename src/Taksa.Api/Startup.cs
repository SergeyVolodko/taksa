using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taksa.Api.BribesApi;
using Taksa.Domain.Bribes;
using Taksa.Framework;

namespace Taksa.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) =>
			ConfigureServicesAsync(services).GetAwaiter().GetResult();

		private async Task ConfigureServicesAsync(IServiceCollection services)
		{
			var esConnection = await Defaults.GetConnection();
			var typeMapper = ConfigureTypeMapper();
			var serializer = new JsonNetSerializer();


			services.AddSingleton<IAggregateStore>(new GesAggregateStore(
				(type, id) => $"{type.Name}-{id}",
				esConnection,
				serializer,
				typeMapper
			));

			services.AddTransient<IBribeService, BribeService>();


			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.Authority = "https://my-test-api.eu.auth0.com";
				options.Audience = "Q3XqA2aXJjZYIqZ86PhszauwIHzTZ1TE";
			});

			services.AddMvc();
		}

		private static TypeMapper ConfigureTypeMapper()
		{
			var mapper = new TypeMapper();
			mapper.Map<Events.V1.BribeCreated>("BribeCreated");

			return mapper;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			// Enable swagger and customize
			var iisVirtualDirectory = "";
			app.UseSwagger();
			app.UseStaticFiles(); // Used by custom script for swagger-ui
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Taksa API");
				
				c.InjectOnCompleteJavaScript($"{iisVirtualDirectory}/swagger-ui/tokeninput.js");

				c.InjectStylesheet($"{iisVirtualDirectory}/swagger-ui/swagger.css");
				c.DocExpansion("none");
			});

			app.UseAuthentication();
			app.UseMvc();
		}
	}
}
