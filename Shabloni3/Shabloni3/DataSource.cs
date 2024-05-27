namespace Shabloni3
{
    class DataSource
    {
        private static int i = 0;
        public static string GetData()
        {
            i++;
            return $"Некоторая информация (id - {i})";
        }
    }
}
