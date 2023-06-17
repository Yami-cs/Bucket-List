using Microsoft.Maui.Handlers;

namespace BucketListMAUI.Handlers;

// В данный момент этот класс не используется (проблемы с переходом с .net6 на .net7)
public partial class CircularButtonHandler : GraphicsViewHandler
{
    private static readonly IPropertyMapper<CircularButton, CircularButtonHandler> PropertyMapper = new PropertyMapper<CircularButton, CircularButtonHandler>(GraphicsViewHandler.Mapper)
    {
        [nameof(CircularButton.ButtonColor)] = MapButtonColor,
        [nameof(CircularButton.Image)] = MapImage,
        [nameof(CircularButton.IsVisible)] = MapIsVisible
    };

    public CircularButtonHandler() : base(PropertyMapper)
    {
    }

    private static void MapButtonColor(CircularButtonHandler handler, CircularButton button)
    {
        if (button.Drawable is CircularButtonDrawable drawable)
            drawable.ButtonColor = button.ButtonColor;
        button.Invalidate();
    }

    private static void MapImage(CircularButtonHandler handler, CircularButton button)
    {
        if (button.Drawable is CircularButtonDrawable drawable)
            drawable.Image = button.Image;
        button.Invalidate();
    }
    static void MapIsVisible(CircularButtonHandler handler, CircularButton button)
    {
        button.FadeTo(button.IsVisible ? 1 : 0, 500);
    }

}