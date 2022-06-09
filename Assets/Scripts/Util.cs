internal class Util
{
    internal static void Shuffle<T>(T[] array)
    {
        // Create a random num gen
        var r = new System.Random();

        int n = array.Length;
        while (n > 1)
        {
            int k = r.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
