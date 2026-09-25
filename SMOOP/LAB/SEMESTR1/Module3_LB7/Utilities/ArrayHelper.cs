namespace Module3_LB7
{
    public static class ArrayHelper
    {
        public static void AddElement<T>(ref T[] array, T element)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = element;
        }
    }
}