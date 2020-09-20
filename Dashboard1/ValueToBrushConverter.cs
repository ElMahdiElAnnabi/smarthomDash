using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;


namespace Dashboard1
{
    class ValueToBrushConverter: IValueConverter
    {
        static readonly Color[] _colorTable =
           {
            Color.FromRgb(  0, 255, 255),
            Color.FromRgb(  0, 255,   0),
            Color.FromRgb(255, 255,   0),
            Color.FromRgb(255,   0,   0),
            };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var args = parameter as string;
            var minimumInput = int.Parse(args.Split('~')[0]);
            var maximumInput = int.Parse(args.Split('~')[1]);

            var currentValue = ((double)value - minimumInput) / (maximumInput - minimumInput);
            var col1 = (int)(currentValue * (_colorTable.Length - 1));
            var col2 = Math.Min(col1 + 1, (_colorTable.Length - 1));

            var t = 1.0 / (_colorTable.Length - 1);
            return new SolidColorBrush(Lerp(_colorTable[col1], _colorTable[col2], (currentValue - t * col1) / t));
        }

        public static Color Lerp(Color col1, Color col2, double t)
        {
            var r = col1.R * (1 - t) + col2.R * t;
            var g = col1.G * (1 - t) + col2.G * t;
            var b = col1.B * (1 - t) + col2.B * t;
            return Color.FromRgb((byte)r, (byte)g, (byte)b);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
