namespace LB9_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            RgbColor color = new RgbColor(255, 87, 51); 
            IColorConverter service = new ColorConverterService();

            string hex = service.ToHex(color);
            CmykColor cmyk = service.ToCmyk(color);
            HslColor hsl = service.ToHsl(color);


            Console.WriteLine($"HEX:  {hex}");
            Console.WriteLine($"CMYK: {cmyk}"); 
            Console.WriteLine($"HSL:  {hsl}");

            Console.ReadLine();
        }
    }
}