using BucketListMAUI.Handlers;
using IImage = Microsoft.Maui.Graphics.IImage;

// Класс, который отвечает за элемент интерфейса "Круглая кнопка"
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
        nameof(ButtonColor), typeof(Color), typeof(CircularButton), propertyChanged: OnButtonColorChanged);
        //propertyChanged: OnButtonColorChanged);

        public static BindableProperty ImageProperty = BindableProperty.Create(
            nameof(Image), typeof(string), typeof(CircularButton), propertyChanged: OnImageChanged);
        //propertyChanged: OnImageChanged);

        public new static BindableProperty IsVisibleProperty = BindableProperty.Create(
            nameof(IsVisible), typeof(bool), typeof(CircularButton),
            propertyChanged: OnIsVisibleChanged);
        //propertyChanged: OnIsVisibleChanged);

        public CircularButton()
        {
            var drawable = new CircularButtonDrawable();
            Drawable = drawable;
        }

        //Метод, отвечающий за цвет кнопки
        private static void OnButtonColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CircularButton)bindable;
            var buttonColor = control.ButtonColor;
            var thisDrawable = control.Drawable as BucketListMAUI.Drawable.CircularButtonDrawable;
            thisDrawable.ButtonColor = buttonColor;

            control.Invalidate();
        }

        //Метод, отвечающий за изображение на кнопке
        private static void OnImageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CircularButton)bindable;
            var image = control.Image;
            var thisDrawable = control.Drawable as BucketListMAUI.Drawable.CircularButtonDrawable;
            thisDrawable.Image = image;
            control.Invalidate();

        }

        //Метод, отвечающий за видимость кнопки
        private static void OnIsVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CircularButton)bindable;

            var newValueAsBool = (bool)newValue;

            control.FadeTo(newValueAsBool == true ? 1 : 0, 500);
        }

        public async Task<bool> BounceOnPressAsync()
        {
            await this.ScaleTo(1.2, 100, Easing.BounceIn);

            return await this.ScaleTo(1.0, 100, Easing.BounceOut);
        }
    }
}
