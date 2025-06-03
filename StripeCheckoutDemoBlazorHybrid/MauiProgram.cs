using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stripe;
using StripeCheckoutDemo.Models;

namespace StripeCheckoutDemoBlazorHybrid;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

		builder.Services.AddScoped(sp => new HttpClient
		{
			BaseAddress = new Uri("http://localhost:5070/")
		});

		// Configure Stripe settings
		// builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
		// builder.Services.Configure<StripeSettings>(options =>
		// {
		// 	builder.Configuration.GetSection("Stripe").Bind(options);
		// });
			
		// StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];


#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		// builder.ConfigureEssentials(essentials =>
		// {
		// 	essentials.AddAppAction("success", "Payment Success", "Payment was successful");
		// 	essentials.AddAppAction("cancel", "Payment Cancelled", "Payment was cancelled");
		// });

		return builder.Build();
	}
}
