using Microsoft.Extensions.Logging;
using ScheduleListUI.Services;
using ScheduleListUI.Validations;

namespace ScheduleListUI;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif //registrando os serviçõs client
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<ApiServices>();
		builder.Services.AddSingleton<IValidator, Validator>();
        return builder.Build();
	}
}
