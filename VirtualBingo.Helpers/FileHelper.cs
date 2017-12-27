using System.IO;

namespace VirtualBingo.Helpers
{
    public static class FileHelper
    {
        public static string GenerateUniqueFileNameForPath(string path, string extension)
        {
            string fileName;

            do
            {
                fileName = string.Format("{0}{1}", Path.GetRandomFileName(), extension);
                
                // According to https://stackoverflow.com/a/21684837/5686352
                // Chances of generating an repeated name are 1 in (26 + 10) ^ 11, but hey, you never know
            } while (File.Exists(Path.Combine(path, fileName)));

            return fileName;
        }
    }
}
