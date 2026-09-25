namespace LB9_2
{
    public struct RgbColor
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public RgbColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
    }

    public struct CmykColor
    {
        public double C { get; set; }
        public double M { get; set; }
        public double Y { get; set; }
        public double K { get; set; }

        public override string ToString() =>
            $"CMYK({C * 100:F0}%, {M * 100:F0}%, {Y * 100:F0}%, {K * 100:F0}%)";
    }

    public struct HslColor
    {
        public double H { get; set; } 
        public double S { get; set; } 
        public double L { get; set; } 

        public override string ToString() =>
            $"HSL({H:F0}°, {S * 100:F0}%, {L * 100:F0}%)";
    }
}