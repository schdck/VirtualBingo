using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Threading;
using VirtualBingo.Helpers;
using VirtualBingo.UI.Player.Views.Windows;
using VirtualBingo.UI.Shared.Messages;
using VirtualBingo.UI.Shared.Models;

namespace VirtualBingo.UI.Player.ViewModels
{
    public class PlayViewModel : ViewModelBase
    {
        private AnswersWindow AnswersWindow;

        public bool IsMuted
        {
            get => !SettingsHelper.Instance.Settings_NarrateQuestionTitle;
            private set => SettingsHelper.Instance.Settings_NarrateQuestionTitle = !value;
        }

        public bool HasPreviousQuestion
        {
            get => CurrentGame != null && CurrentQuestionIndex > 0;
        }
        public bool HasNextQuestion
        {
            get => CurrentGame != null && CurrentQuestionIndex < CurrentGame.Questions.Count - 1;
        }

        public int CurrentQuestionIndex { get; private set; }

        public ICommand ToggleMuteCommand { get; private set; }
        public ICommand PreviousQuestionCommand { get; private set; }
        public ICommand NextQuestionCommand { get; private set; }
        public ICommand ShowAnswersWindowCommand { get; private set; }
        public ICommand EndGameCommand { get; private set; }

        public BingoGame CurrentGame { get; private set; }

        public BingoQuestion CurrentQuestion { get; private set; }
        public BingoQuestion PreviousQuestion { get; private set; }

        public PlayViewModel()
        {
            Messenger.Default.Register<LaunchGameMessage>(this, HandleLaunchGameMessage);

            InitializeCommands();

            PropertyChanged += (s, e) =>
            {
                if (!IsMuted && e.PropertyName == nameof(CurrentQuestion) && !string.IsNullOrWhiteSpace(CurrentQuestion.Title))
                {
                    SpeechHelper.PlaySpeech(CurrentQuestion.Title);
                }
            };
        }

        private void HandleLaunchGameMessage(LaunchGameMessage obj)
        {
            CurrentGame = obj.Game;
            CurrentQuestion = obj.Game.Questions[0];
            PreviousQuestion = null;

            CurrentQuestionIndex = 0;

            Dispatcher.CurrentDispatcher.Invoke(() => CommandManager.InvalidateRequerySuggested());
        }

        private void InitializeCommands()
        {
            ToggleMuteCommand = new RelayCommand(() =>
            {
                IsMuted = !IsMuted;
            });

            PreviousQuestionCommand = new RelayCommand(() =>
            {
                CurrentQuestion = CurrentGame.Questions[--CurrentQuestionIndex];

                if (CurrentQuestionIndex > 0)
                {
                    PreviousQuestion = CurrentGame.Questions[CurrentQuestionIndex - 1];
                }
                else
                {
                    PreviousQuestion = null;
                }
            });

            NextQuestionCommand = new RelayCommand(() =>
            {
                PreviousQuestion = CurrentQuestion;
                CurrentQuestion = CurrentGame.Questions[++CurrentQuestionIndex];
            });

            ShowAnswersWindowCommand = new RelayCommand(() =>
            {
                if(AnswersWindow == null || AnswersWindow.IsVisible == false)
                {
                    AnswersWindow = new AnswersWindow();
                }

                AnswersWindow.Show();
                AnswersWindow.Activate();
            });

            EndGameCommand = new RelayCommand(() =>
            {
                MessengerInstance.Send(new EndGameMessage());

                if(AnswersWindow != null && AnswersWindow.IsVisible)
                {
                    AnswersWindow.Close();
                }
            });
        }
    }
}
