namespace LB9_2
{
    public interface IColorConverter
    {
        string ToHex(RgbColor color);
        CmykColor ToCmyk(RgbColor color); 
        HslColor ToHsl(RgbColor color);   
    }
}