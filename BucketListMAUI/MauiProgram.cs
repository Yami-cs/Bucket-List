﻿using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace BucketListMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddSingleton<TaskPage>();
		builder.Services.AddSingleton<TaskViewModel>();

        builder.Services.AddSingleton<DetailPage>();
        builder.Services.AddSingleton<DetailViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
