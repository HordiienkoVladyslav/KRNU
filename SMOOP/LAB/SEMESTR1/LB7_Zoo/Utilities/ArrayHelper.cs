namespace LB7_Zoo
{
    public static class ArrayHelper
    {
        // Додати елемент в кінець
        public static void AddElement<T>(ref T[] array, T element)
        {
            System.Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = element;
        }

        // Видалити перший елемент (зсув черги)
        public static void RemoveFirst<T>(ref T[] array)
        {
            if (array.Length == 0) return;
            T[] newArray = new T[array.Length - 1];
            for (int i = 1; i < array.Length; i++)
            {
                newArray[i - 1] = array[i];
            }
            array = newArray;
        }
    }
}