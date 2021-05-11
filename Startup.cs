using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RestPay.Models;
using RestPay.Repositories;
using RestPay.Services;

namespace RestPay
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
			services.AddHttpClient();

			services.Configure<MongoDBSettings>(Configuration.GetSection(nameof(MongoDBSettings)));
			services.Configure<TransactionAuthenticatorSettings>(Configuration.GetSection(nameof(TransactionAuthenticatorSettings)));
			services.Configure<NotificationSettings>(Configuration.GetSection(nameof(NotificationSettings)));

			services.AddSingleton<IMongoDBSettings>(sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
			services.AddSingleton<ITransactionAuthenticatorSettings>(sp => sp.GetRequiredService<IOptions<TransactionAuthenticatorSettings>>().Value);
			services.AddSingleton<INotificationSettings>(sp => sp.GetRequiredService<IOptions<NotificationSettings>>().Value);
			services.AddSingleton<IUserRepository, UserRepository>();
			services.AddSingleton<ITransactionRepository, TransactionRepository>();

			services.AddTransient<ITransactionService, TransactionService>();
			services.AddTransient<INotificationService, NotificationService>();
			services.AddTransient<IUserService, UserService>();

			services.AddControllers();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = "Respay API",
					Version = "v1",
					Description = "API RESTfull para transações financeiras",
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

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});
		}
	}
}
