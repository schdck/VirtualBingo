using System;
using System.IO;

namespace VirtualBingo.Helpers
{
    public static class GamesDirectoryHelper
    {
        public static string GamesDirectory
        {
            get
            {
                return GetGamesDirectory();
            }
            set
            {
                SetGamesDirectory(value);
            }
        }

        public static string TemporaryDirectory
        {
            get
            {
                return GetTemporaryDirectory();
            }
            set
            {
                SetTemporaryDirectory(value);
            }
        }        

        private static string GetGamesDirectory()
        {
            string currentDirectory = SettingsHelper.Instance.Settings_GamesDirectory;

            if (string.IsNullOrWhiteSpace(currentDirectory) && Directory.Exists(currentDirectory))
            {
                return currentDirectory;
            }

            currentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Bingo", "Games");

            if(!Directory.Exists(currentDirectory))
            {
                Directory.CreateDirectory(currentDirectory);
            }

            SettingsHelper.Instance.Settings_GamesDirectory = currentDirectory;

            return currentDirectory;
        }

        private static string GetTemporaryDirectory()
        {
            string currentDirectory = SettingsHelper.Instance.Settings_TemporaryDirectory;

            if (string.IsNullOrWhiteSpace(currentDirectory) && Directory.Exists(currentDirectory))
            {
                return currentDirectory;
            }

            currentDirectory = Path.Combine(Path.GetTempPath(), "BingoTemp");

            if (!Directory.Exists(currentDirectory))
            {
                Directory.CreateDirectory(currentDirectory);
            }

            SettingsHelper.Instance.Settings_TemporaryDirectory = currentDirectory;

            return currentDirectory;
        }

        private static void SetGamesDirectory(string path)
        {
            CheckPath(path);

            SettingsHelper.Instance.Settings_GamesDirectory = path;
        }

        private static void SetTemporaryDirectory(string path)
        {
            CheckPath(path);

            SettingsHelper.Instance.Settings_TemporaryDirectory = path;

        }

        private static void CheckPath(string path)
        {
            try
            {
                path = Path.GetFullPath(path); // It'll throw an exception if the path is not valid

                if(!Path.IsPathRooted(path))
                {
                    throw new Exception("The path is not rooted.");
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("The path is not valid.", e);
            }
        }
    }
}
