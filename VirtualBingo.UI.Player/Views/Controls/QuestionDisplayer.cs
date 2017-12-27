using System.Windows;
using System.Windows.Controls;
using VirtualBingo.UI.Shared.ENUMs;
using VirtualBingo.UI.Shared.Models;

namespace VirtualBingo.UI.Player.Views.Controls
{
    public class QuestionDisplayer : Control
    {
        private static readonly DependencyPropertyKey DisplayTypePropertyKey = DependencyProperty.RegisterReadOnly("DisplayType", typeof(QuestionDisplayerPossibilities), typeof(QuestionDisplayer), new FrameworkPropertyMetadata(default(QuestionDisplayerPossibilities)));

        public static readonly DependencyProperty DisplayTypeProperty = DisplayTypePropertyKey.DependencyProperty;

        public static readonly DependencyProperty QuestionProperty = DependencyProperty.Register("Question", typeof(BingoQuestion), typeof(QuestionDisplayer), new PropertyMetadata(new PropertyChangedCallback(UpdateDisplayType)));

        public BingoQuestion Question
        {
            get { return (BingoQuestion) GetValue(QuestionProperty); }
            set { SetValue(QuestionProperty, value); }
        }

        public QuestionDisplayerPossibilities DisplayType
        {
            get { return (QuestionDisplayerPossibilities)GetValue(DisplayTypeProperty); }
            protected set { SetValue(DisplayTypeProperty, value); }
        }

        private static void UpdateDisplayType(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var question = e.NewValue as BingoQuestion;

            if(question != null)
            {
                QuestionDisplayerPossibilities newValue;

                bool titleNullOrEmpty = string.IsNullOrEmpty(question.Title),
                     titleImageNullOrEmpty = string.IsNullOrEmpty(question.TitleImagePath);

                if (titleNullOrEmpty && titleImageNullOrEmpty)
                {
                    newValue = QuestionDisplayerPossibilities.Invalid;
                }
                else if (titleNullOrEmpty)
                {
                    newValue = QuestionDisplayerPossibilities.DisplayImageOnly;
                }
                else if (titleImageNullOrEmpty)
                {
                    newValue = QuestionDisplayerPossibilities.DisplayTextOnly;
                }
                else
                {
                    newValue = QuestionDisplayerPossibilities.DisplayBoth;
                }

                s.SetValue(DisplayTypePropertyKey, newValue);
            }
            
        }
    }
}
