using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VirtualBingo.Helpers
{
    public class SettingsHelper : INotifyPropertyChanged
    {
        public static readonly SettingsHelper Instance = new SettingsHelper();

        public event PropertyChangedEventHandler PropertyChanged;

        #region Settings
        public bool Settings_NarrateQuestionTitle
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        // Internal because GamesDirectoryHelper should be used instead
        internal string Settings_GamesDirectory
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        // Internal because GamesDirectoryHelper should be used instead
        internal string Settings_TemporaryDirectory
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        #endregion

        #region Preferences
        public string Preferences_FilterSelectedLanguage
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Preferences_FilterSelectedSubject
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Preferences_FilterSelectedTopic
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        #endregion

        public void Reset()
        {
            Properties.Settings.Default.Reset();
        }

        private SettingsHelper() { }

        private T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            return (T) Properties.Settings.Default[propertyName];
        }

        private void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            if(value == Properties.Settings.Default[propertyName])
            {
                return;
            }

            Properties.Settings.Default[propertyName] = value;
            Properties.Settings.Default.Save();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
