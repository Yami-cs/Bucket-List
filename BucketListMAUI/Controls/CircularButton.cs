using BucketListMAUI.Handlers;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace BucketListMAUI.Controls
{
    public class CircularButton : GraphicsView
    {
        /// <summary>
        /// Цвет, которые передается в drawable для "FIll Color" метода Fill Circle
        /// </summary>
        public Color ButtonColor
        {
            get => (Color)GetValue(ButtonColorProperty);
            set => SetValue(ButtonColorProperty, value);
        }
        /// <summary>
        /// Строка, содержащая название изображения (имя файла, не директории)
        /// <br/> Ожидается, что файл лежит в Recources/Images/
        /// </summary>
        public string Image
        {
            get => (string)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);

        }

        public new bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }
        public static BindableProperty ButtonColorProperty = BindableProperty.Create(
        nameof(ButtonColor), typeof(Color), typeof(CircularButton));
        //propertyChanged: OnButtonColorChanged);

        public static BindableProperty ImageProperty = BindableProperty.Create(
            nameof(Image), typeof(string), typeof(CircularButton));
        //propertyChanged: OnImageChanged);

        public new static BindableProperty IsVisibleProperty = BindableProperty.Create(
            nameof(IsVisible), typeof(bool), typeof(CircularButton),
            defaultValue: false);
        //propertyChanged: OnIsVisibleChanged);

        public CircularButton()
        {
            Handler = new CircularButtonHandler();
            var drawable = new CircularButtonDrawable();
            Drawable = drawable;
        }

        //Перенес эту логику в circular button handler
        ///<seealso cref = "CircularButtonHandler" />
        static void OnButtonColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CircularButton)bindable;
            var buttonColor = control.ButtonColor;
            if (control.Drawable is CircularButtonDrawable thisDrawable)
                thisDrawable.ButtonColor = buttonColor;

            control.Invalidate();
        }

        static void OnImageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CircularButton)bindable;
            var image = control.Image;
            if (control.Drawable is CircularButtonDrawable thisDrawable)
                thisDrawable.Image = image;
            control.Invalidate();

        }

        //Перенес эту логику в circular button handler
        ///<seealso cref = "CircularButtonHandler" />
        static void OnIsVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CircularButton)bindable;

            var newValueAsBool = (bool)newValue;

            if (newValueAsBool == true)
            {
                control.FadeTo(1, 500);
            }
            else
            {
                control.FadeTo(0, 500);
            }

        }

        public async Task<bool> BounceOnPressAsync()
        {
            await this.ScaleTo(1.2, 100, Easing.BounceIn);

            return await this.ScaleTo(1.0, 100, Easing.BounceOut);
        }
    }
}
