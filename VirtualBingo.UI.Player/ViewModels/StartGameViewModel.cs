using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VirtualBingo.Helpers;
using VirtualBingo.UI.Player.Classes;
using VirtualBingo.UI.Shared.Messages;
using VirtualBingo.UI.Shared.Models;

namespace VirtualBingo.UI.Player.ViewModels
{
    public class StartGameViewModel : ViewModelBase
    {
        private IEnumerable<GameInfo> _AvaliableGames;

        public IEnumerable<string> AvaliableLanguages { get; private set; }
        public IEnumerable<string> AvaliableSubjects { get; private set; }
        public IEnumerable<string> AvaliableTopics { get; private set; }

        public ICommand RefreshFiltersCommand { get; private set; }
        public ICommand LaunchGameCommand { get; private set; }

        public ObservableCollection<GameInfo> FilteredAvaliableGames { get; private set; }

        public string FilterSelectedLanguage { get; set; }
        public string FilterSelectedSubject { get; set; }
        public string FilterSelectedTopic { get; set; }

        public StartGameViewModel()
        {
            FilterSelectedLanguage = Properties.Settings.Default.Filter_Language;
            FilterSelectedSubject = Properties.Settings.Default.Filter_Subject;
            FilterSelectedTopic = Properties.Settings.Default.Filter_Topic;

            InitializeCommands();

            RefreshFilters();

            PropertyChanged += (s, e) =>
            {
                string propertyName = e.PropertyName;

                if(propertyName == nameof(FilterSelectedLanguage) || propertyName == nameof(FilterSelectedSubject) || propertyName == nameof(FilterSelectedTopic))
                {
                    FilterAvaliableGames();

                    Properties.Settings.Default.Filter_Language = FilterSelectedLanguage;
                    Properties.Settings.Default.Filter_Subject = FilterSelectedSubject;
                    Properties.Settings.Default.Filter_Topic = FilterSelectedTopic;

                    Properties.Settings.Default.Save();
                }
            };
        }

        private void InitializeCommands()
        {
            RefreshFiltersCommand = new RelayCommand(() => RefreshFilters());

            LaunchGameCommand = new RelayCommand<GameInfo>((x) =>
            {
                try
                { 
                    MessengerInstance.Send(new LaunchGameMessage(this, BingoGame.LoadGame(x.Language, x.Subject, x.Topic)));
                }
                catch(Exception e)
                {
                    // XXX Display exception
                }
            });
        }

        private void RefreshFilters()
        {
            var gameList = new List<GameInfo>();

            HashSet<string> avaliableLanguages = new HashSet<string>();
            HashSet<string> avaliableSubjects = new HashSet<string>();
            HashSet<string> avaliableTopics = new HashSet<string>();

            foreach (string languageFolder in Directory.GetDirectories(GamesDirectoryHelper.GamesDirectory))
            {
                string languageFolderName = new DirectoryInfo(languageFolder).Name;

                avaliableLanguages.Add(languageFolderName);

                foreach (string subjectFolder in Directory.GetDirectories(languageFolder))
                {
                    string subjectFolderName = new DirectoryInfo(subjectFolder).Name;

                    avaliableSubjects.Add(subjectFolderName);

                    foreach (string topicFile in Directory.GetFiles(subjectFolder))
                    {
                        string topicFileName = Path.GetFileNameWithoutExtension(topicFile);

                        avaliableTopics.Add(topicFileName);

                        gameList.Add(new GameInfo(languageFolderName, subjectFolderName, topicFileName));
                    }
                }
            }

            AvaliableLanguages = avaliableLanguages;
            AvaliableSubjects = avaliableSubjects;
            AvaliableTopics = avaliableTopics;

            _AvaliableGames = gameList;

            FilterAvaliableGames();
        }

        private void FilterAvaliableGames()
        {
            var gameList = new List<GameInfo>();

            bool language = !string.IsNullOrEmpty(FilterSelectedLanguage),
                 subject = !string.IsNullOrEmpty(FilterSelectedSubject),
                 topic = !string.IsNullOrEmpty(FilterSelectedTopic);

            foreach (GameInfo g in _AvaliableGames)
            {
                if((language && g.Language != FilterSelectedLanguage) || (subject && g.Subject != FilterSelectedSubject) || (topic && g.Topic != FilterSelectedTopic))
                {
                    // Should not be added (the code is clearer this way)
                }
                else
                {
                    gameList.Add(g);
                }
            }
            
            FilteredAvaliableGames = new ObservableCollection<GameInfo>(gameList.OrderBy((x) =>
            {
                return x.Topic;
            }));
        }
    }
}
