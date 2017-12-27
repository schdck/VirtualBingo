using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using VirtualBingo.UI.Shared.Messages;
using VirtualBingo.UI.Shared.Models;
using System;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace VirtualBingo.UI.Player.ViewModels
{
    public class AnswersViewModel : ViewModelBase
    {
        public ICommand ShowTitleImageCommand { get; private set; }
        public ICommand ShowAnswerImageCommand { get; private set; }

        public AnswersViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            ShowTitleImageCommand = new RelayCommand<BingoQuestion>((x) =>
            {
                ShowImage(x.TitleImagePath);
            });

            ShowAnswerImageCommand = new RelayCommand<BingoQuestion>((x) =>
            {
                ShowImage(x.AnswerImagePath);
            });
        }

        private void ShowImage(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            var imageDisplayer = new MetroWindow()
            {
                Content = new Image()
                {
                    Source = new BitmapImage(new Uri(path)),
                    Margin = new Thickness(5)
                },
                Width = 400,
                Height = 400,
                BorderThickness = new Thickness(1, 0, 1, 1),
                BorderBrush = Brushes.Gray
            };
    
            imageDisplayer.Show();
        }
    }
}
