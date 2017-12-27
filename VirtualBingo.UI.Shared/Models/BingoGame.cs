using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using VirtualBingo.Helpers;

namespace VirtualBingo.UI.Shared.Models
{
    public class BingoGame
    {
        private static int _CreatedGames = 1;

        private string _TemporaryFolder;
        private string _ImagesFolder;

        public int Id { get; private set; }
        public string Subject { get; private set; }
        public string Topic { get; private set; }
        public string Language { get; private set; }
        public string Author { get; private set; }

        public ObservableCollection<BingoQuestion> Questions { get; private set; }

        [JsonConstructor]
        private BingoGame(string subject, string topic, string language, string author, IEnumerable<BingoQuestion> questions)
        {
            Id = _CreatedGames++;

            Subject = subject;
            Topic = topic;
            Language = language;
            Author = author;

            Random random = new Random();

            // Shuffle the questions
            Questions = new ObservableCollection<BingoQuestion>(questions.OrderBy(x => random.Next()));
        }

        public static void CreateGameFile(string subject, string topic, string language, string author, IEnumerable<BingoQuestion> questions)
        {
            string gameDirectory = Path.Combine(GamesDirectoryHelper.GamesDirectory, language, subject);
            string gameFile = Path.Combine(gameDirectory, string.Format("{0}.zip", topic));

            if(File.Exists(gameFile))
            {
                throw new Exception("This game already exists");
            }

            List<BingoQuestion> copy = new List<BingoQuestion>();

            foreach(BingoQuestion q in questions)
            {
                copy.Add(new BingoQuestion()
                {
                    AlwaysDisplayTitleImage = q.AlwaysDisplayTitleImage,
                    Answer = q.Answer,
                    AnswerImagePath = q.AnswerImagePath,
                    Title = q.Title,
                    TitleImagePath = q.TitleImagePath
                });
            }

            questions = copy;

            BingoGame game = new BingoGame(subject, topic, language, author, questions);

            game._TemporaryFolder = Path.Combine(GamesDirectoryHelper.TemporaryDirectory, "CreatedGame");
            game._ImagesFolder = Path.Combine(game._TemporaryFolder, "Images");

            try
            {
                if (Directory.Exists(game._TemporaryFolder))
                {
                    Directory.Delete(game._TemporaryFolder, true);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to delete CreatedGame folder", e);
            }

            try
            {
                Directory.CreateDirectory(game._TemporaryFolder);
                Directory.CreateDirectory(game._ImagesFolder);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to create CreatedGame or Images folder", e);
            }

            Dictionary<string, string> generatedNames = new Dictionary<string, string>();

            try
            {
                foreach (BingoQuestion q in game.Questions)
                {
                    if (!string.IsNullOrEmpty(q.TitleImagePath))
                    {
                        string newPath;

                        if(!generatedNames.TryGetValue(q.TitleImagePath, out newPath))
                        {
                            newPath = Path.Combine(game._ImagesFolder, FileHelper.GenerateUniqueFileNameForPath(game._ImagesFolder, Path.GetExtension(q.TitleImagePath)));

                            generatedNames.Add(q.TitleImagePath, newPath);

                            File.Copy(q.TitleImagePath, newPath);
                        }                        

                        q.TitleImagePath = newPath;
                    }

                    if (!string.IsNullOrEmpty(q.AnswerImagePath))
                    {
                        string newPath;

                        if (!generatedNames.TryGetValue(q.AnswerImagePath, out newPath))
                        {
                            newPath = Path.Combine(game._ImagesFolder, FileHelper.GenerateUniqueFileNameForPath(game._ImagesFolder, Path.GetExtension(q.AnswerImagePath)));

                            generatedNames.Add(q.AnswerImagePath, newPath);

                            File.Copy(q.AnswerImagePath, newPath);
                        }

                        q.AnswerImagePath = newPath;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to move an image to game folder", e);
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(Path.Combine(game._TemporaryFolder, "Game.json")))
                {
                    writer.Write(JsonConvert.SerializeObject(game, Formatting.Indented));
                }
            }
            catch(Exception e)
            {
                throw new Exception("Failed to serialize the game or write it to JSON", e);
            }

            try
            {
                if (!Directory.Exists(gameDirectory))
                {
                    Directory.CreateDirectory(gameDirectory);
                }

                ZipFile.CreateFromDirectory(game._TemporaryFolder, Path.Combine(gameDirectory, string.Format("{0}.zip", game.Topic)));
            }
            catch
            {
                throw new Exception("Failed to create ZIP file");
            }
        }

        public static BingoGame LoadGame(string pathToGameFile)
        {
            if (!File.Exists(pathToGameFile))
            {
                throw (new FileNotFoundException("Game file not found.", pathToGameFile));
            }

            string temporaryFolder = Path.Combine(GamesDirectoryHelper.TemporaryDirectory, $"Game {_CreatedGames}");

            if (Directory.Exists(temporaryFolder))
            {
                Directory.Delete(temporaryFolder, true);
            }

            ZipFile.ExtractToDirectory(pathToGameFile, temporaryFolder);

            string jsonString;

            using (StreamReader reader = new StreamReader(Path.Combine(temporaryFolder, "Game.json")))
            {
                jsonString = reader.ReadToEnd();
            }

            return LoadGameFromJson(jsonString, temporaryFolder);
        }

        public static BingoGame LoadGame(string language, string subject, string topic)
        {
            return LoadGame(Path.Combine(GamesDirectoryHelper.GamesDirectory, language, subject, string.Format("{0}.zip", topic)));
        }

        private static BingoGame LoadGameFromJson(string jsonString, string temporaryFolder)
        {
            BingoGame g = JsonConvert.DeserializeObject<BingoGame>(jsonString);

            g._TemporaryFolder = temporaryFolder;
            g._ImagesFolder = Path.Combine(temporaryFolder, "Images");

            foreach(BingoQuestion q in g.Questions)
            {
                if(!string.IsNullOrEmpty(q.TitleImagePath))
                {
                    q.TitleImagePath = Path.Combine(g._ImagesFolder, q.TitleImagePath);
                }
                if(!string.IsNullOrEmpty(q.AnswerImagePath))
                {
                    q.AnswerImagePath = Path.Combine(g._ImagesFolder, q.AnswerImagePath);
                }
            }

            return g;
        }
    }
}
