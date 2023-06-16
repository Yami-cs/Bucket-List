using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using BucketListMAUI.View;
using BucketListMAUI.ViewModel;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace BucketListMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.UseSkiaSharp()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton<ItemService>();

        builder.Services.AddTransient<UserListDetails>();
        //builder.Services.AddTransient<UserListDetailViewModel>();

        builder.Services.AddSingleton<UserListViewModel>();
        builder.Services.AddTransient<UserListDataInput>();
        builder.Services.AddTransient<UserListDataInputViewModel>();
        builder.Services.AddSingleton<UserListService>();
        builder.Services.AddSingleton<ItemService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
