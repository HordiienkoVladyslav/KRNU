namespace LB9_2
{
    public class ColorConverterService : IColorConverter
    {

        public string ToHex(RgbColor color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
            public CmykColor ToCmyk(RgbColor color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double k = 1 - Math.Max(r, Math.Max(g, b));

            return k switch
            {
                1 => new CmykColor { C = 0, M = 0, Y = 0, K = 1 },
                _ => new CmykColor
                {
                    C = (1 - r - k) / (1 - k),
                    M = (1 - g - k) / (1 - k),
                    Y = (1 - b - k) / (1 - k),
                    K = k
                }
            };
        }

        public HslColor ToHsl(RgbColor color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));
            double delta = max - min;

            double h = 0, s = 0, l = (max + min) / 2.0;

            if (delta != 0)
            {
                s = l > 0.5 ? delta / (2.0 - max - min) : delta / (max + min);

                h = max switch
                {
                    _ when max == r => (g - b) / delta + (g < b ? 6 : 0),
                    _ when max == g => (b - r) / delta + 2,
                    _ when max == b => (r - g) / delta + 4,
                    _ => 0
                };

                h *= 60; 
            }

            return new HslColor { H = h, S = s, L = l };
        }
    }
}