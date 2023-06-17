using System;
using SkiaSharp;
using SkiaSharp.Extended.UI.Controls;

namespace BucketListMAUI.Controls;

// В данный момент этот класс не используется, но он позволяет добавлять Lottie анимацию в проект
public class CircularLottie : SKLottieView
{
    public new Color BackgroundColor = Colors.Transparent;


    public CircularLottie() : base()
    {
        Console.Write("test");
        this.IsAnimationEnabled = true;
        //base.OnPaintSurface(); 
    }

    protected override void OnPaintSurface(SKCanvas canvas, SKSize size)
    {
        Console.Write("test");
        using var paint = new SKPaint
        {
            IsAntialias = true,
            Color = SKColors.Blue
        };
        canvas.DrawCircle(200, 200, 100, paint);

    }
}

