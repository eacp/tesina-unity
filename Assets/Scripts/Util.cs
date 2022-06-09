internal class Util
{
    // https://stackoverflow.com/questions/273313/randomize-a-listt
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
