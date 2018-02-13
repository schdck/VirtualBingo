using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using SimpleDialogs;
using SimpleDialogs.Controls;
using SimpleDialogs.Enumerators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VirtualBingo.Helpers;
using VirtualBingo.UI.Shared.Models;

namespace VirtualBingo.UI.Player.ViewModels
{
    public class CreateGameViewModel : ViewModelBase
    {
        public IEnumerable<string> AvaliableLanguages => LanguageHelper.Languages;

        public ObservableCollection<BingoQuestion> AddedQuestions { get; private set; }

        public RelayCommand SaveGameCommand { get; private set; }
        public RelayCommand AddQuestionCommand { get; private set; }
        public RelayCommand<BingoQuestion> RemoveQuestionCommand { get; private set; }
        public RelayCommand<string> AddImageCommand { get; private set; }

        public string GameSubject { get; set; }
        public string GameTopic { get; set; }
        public string GameLanguage { get; set; }
        public string GameAuthor { get; set; } = "Unknown";

        public string CurrentTitle { get; set; }
        public string CurrentTitleImagePath { get; set; }
        public bool CurrentIsImageNecessary { get; set; }

        public string CurrentAnswer { get; set; }
        public string CurrentAnswerImagePath { get; set; }

        public string AddQuestionErrorMessage { get; private set; }

        public CreateGameViewModel()
        {
            AddedQuestions = new ObservableCollection<BingoQuestion>();

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SaveGameCommand = new RelayCommand(() =>
            {
                try
                {
                    BingoGame.CreateGameFile(GameSubject, GameTopic, GameLanguage, GameAuthor, AddedQuestions);
                }
                catch(Exception e)
                {
                    DialogManager.ShowDialog(new AlertDialog()
                    {
                        AlertLevel = AlertLevel.Error,
                        Title = Properties.Resources.GENERIC_Error,
                        Message = Properties.Resources.CREATE_GAME_ErrorCreatingGame,
                        ShowCopyToClipboardButton = true,
                        Exception = e
                    });
                }
            });

            AddQuestionCommand = new RelayCommand(() =>
            {
                if((string.IsNullOrWhiteSpace(CurrentTitle) && string.IsNullOrWhiteSpace(CurrentTitleImagePath)) || (string.IsNullOrWhiteSpace(CurrentAnswer) && string.IsNullOrWhiteSpace(CurrentAnswerImagePath)))
                {
                    AddQuestionErrorMessage = Properties.Resources.CREATE_GAME_ErrorAddingQuestion;
                    return;
                }

                AddQuestionErrorMessage = null;

                AddedQuestions.Add(new BingoQuestion()
                {
                    Title = CurrentTitle,
                    Answer = CurrentAnswer,

                    TitleImagePath = CurrentTitleImagePath,
                    AlwaysDisplayTitleImage = CurrentIsImageNecessary,
                    AnswerImagePath = CurrentAnswerImagePath
                });
            });

            RemoveQuestionCommand = new RelayCommand<BingoQuestion>((x) =>
            {
                AddedQuestions.Remove(x);
            });

            AddImageCommand = new RelayCommand<string>((x) =>
            {
                OpenFileDialog fileDialog = new OpenFileDialog()
                {
                    CheckPathExists = true,
                    CheckFileExists = true,
                    Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png",
                    Multiselect = false
                };

                if(fileDialog.ShowDialog() == true)
                {
                    if (string.Compare(x, "Question", true) == 0)
                    {
                        CurrentTitleImagePath = fileDialog.FileName;
                    }
                    else if (string.Compare(x, "Answer", true) == 0)
                    {
                        CurrentAnswerImagePath = fileDialog.FileName;
                    }
                }                
            });
    }
    }
}
