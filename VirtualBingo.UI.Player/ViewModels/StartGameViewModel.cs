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
        public ICommand LaunchGameCommand { get; private set; }

        public string FilterSelectedLanguage { get; set; }
        public string FilterSelectedSubject { get; set; }
        public string FilterSelectedTopic { get; set; }

        public StartGameViewModel()
        {
            FilterSelectedLanguage = Properties.Settings.Default.Filter_Language;
            FilterSelectedSubject = Properties.Settings.Default.Filter_Subject;
            FilterSelectedTopic = Properties.Settings.Default.Filter_Topic;

            InitializeCommands();
        }

        private void InitializeCommands()
        {
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
    }
}
