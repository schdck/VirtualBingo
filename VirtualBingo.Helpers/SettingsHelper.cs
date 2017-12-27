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

        public bool Settings_NarrateQuestionTitle
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        // Internal because GamesDirectoryHelper should be used instead
        internal string Settings_GamesDirectory
        {
            get
            {
                return GetValue<string>();
            }
            set
            {
                SetValue(value);
            }
        }

        // Internal because GamesDirectoryHelper should be used instead
        internal string Settings_TemporaryDirectory
        {
            get
            {
                return GetValue<string>();
            }
            set
            {
                SetValue(value);
            }
        }

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
