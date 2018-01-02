using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VirtualBingo.Helpers;
using VirtualBingo.UI.Player.Classes;

namespace VirtualBingo.UI.Player.Views.Controls
{
    public class GameChooser : Control, INotifyPropertyChanged
    {
        public static readonly DependencyProperty SelectedGameProperty = DependencyProperty.Register("SelectedGame", typeof(GameInfo), typeof(GameChooser));
        public static readonly DependencyProperty FilterSelectedLanguageProperty = DependencyProperty.Register("FilterSelectedLanguage", typeof(string), typeof(GameChooser), new PropertyMetadata(FilterChanged));
        public static readonly DependencyProperty FilterSelectedSubjectProperty = DependencyProperty.Register("FilterSelectedSubject", typeof(string), typeof(GameChooser), new PropertyMetadata(FilterChanged));
        public static readonly DependencyProperty FilterSelectedTopicProperty = DependencyProperty.Register("FilterSelectedTopic", typeof(string), typeof(GameChooser), new PropertyMetadata(FilterChanged));

        private static void FilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string propertyName = e.Property.Name;
            var changedObject = (d as GameChooser);

            if (changedObject != null)
            {
                bool filterChanged = false;

                if(propertyName == nameof(FilterSelectedLanguage))
                {
                    SettingsHelper.Instance.Preferences_FilterSelectedLanguage = (string)e.NewValue;
                    filterChanged = true;
                }
                else if(propertyName == nameof(FilterSelectedSubject))
                {
                    SettingsHelper.Instance.Preferences_FilterSelectedSubject = (string)e.NewValue;
                    filterChanged = true;
                }
                else if(propertyName == nameof(FilterSelectedTopic))
                {
                    SettingsHelper.Instance.Preferences_FilterSelectedTopic = (string)e.NewValue;
                    filterChanged = true;
                }
                
                if(filterChanged)
                {
                    changedObject.FilterAvaliableGames();
                }
            }
        }

        // Dependency Properties
        public GameInfo SelectedGame
        {
            get { return (GameInfo)GetValue(SelectedGameProperty); }
            set { SetValue(SelectedGameProperty, value); }
        }

        public string FilterSelectedLanguage
        {
            get { return (string)GetValue(FilterSelectedLanguageProperty); }
            set { SetValue(FilterSelectedLanguageProperty, value); }
        }

        public string FilterSelectedSubject
        {
            get { return (string)GetValue(FilterSelectedSubjectProperty); }
            set { SetValue(FilterSelectedSubjectProperty, value); }
        }

        public string FilterSelectedTopic
        {
            get { return (string)GetValue(FilterSelectedTopicProperty); }
            set { SetValue(FilterSelectedTopicProperty, value); }
        }

        // Properties
        private IEnumerable<GameInfo> _AvaliableGames;

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<string> AvaliableLanguages { get; private set; }
        public IEnumerable<string> AvaliableSubjects { get; private set; }
        public IEnumerable<string> AvaliableTopics { get; private set; }

        public ObservableCollection<GameInfo> FilteredAvaliableGames { get; private set; }

        public ICommand RefreshFiltersCommand { get; private set; }

        public GameChooser()
        {
            FilterSelectedLanguage = SettingsHelper.Instance.Preferences_FilterSelectedLanguage;
            FilterSelectedSubject = SettingsHelper.Instance.Preferences_FilterSelectedSubject;
            FilterSelectedTopic = SettingsHelper.Instance.Preferences_FilterSelectedTopic;

            SettingsHelper.Instance.PropertyChanged += (s, e) =>
            {
                if(e.PropertyName == nameof(SettingsHelper.Instance.Preferences_FilterSelectedLanguage))
                {
                    FilterSelectedLanguage = SettingsHelper.Instance.Preferences_FilterSelectedLanguage;
                }
                else if (e.PropertyName == nameof(SettingsHelper.Instance.Preferences_FilterSelectedSubject))
                {
                    FilterSelectedSubject = SettingsHelper.Instance.Preferences_FilterSelectedSubject;
                }
                else if (e.PropertyName == nameof(SettingsHelper.Instance.Preferences_FilterSelectedTopic))
                {
                    FilterSelectedTopic = SettingsHelper.Instance.Preferences_FilterSelectedTopic;
                }
            };

            LoadAvaliableGamesAndFilters();

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            RefreshFiltersCommand = new RelayCommand(() => LoadAvaliableGamesAndFilters());
        }

        private void LoadAvaliableGamesAndFilters()
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
            if(_AvaliableGames == null)
            {
                LoadAvaliableGamesAndFilters();
            }

            var gameList = new List<GameInfo>();

            bool language = !string.IsNullOrEmpty(FilterSelectedLanguage),
                 subject = !string.IsNullOrEmpty(FilterSelectedSubject),
                 topic = !string.IsNullOrEmpty(FilterSelectedTopic);

            foreach (GameInfo g in _AvaliableGames)
            {
                if ((language && g.Language != FilterSelectedLanguage) || (subject && g.Subject != FilterSelectedSubject) || (topic && g.Topic != FilterSelectedTopic))
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
