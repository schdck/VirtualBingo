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

        public static readonly DependencyProperty DisplayAnswerInsteadOfTitleProperty = DependencyProperty.Register("DisplayAnswerInsteadOfTitle", typeof(bool), typeof(QuestionDisplayer), new PropertyMetadata(new PropertyChangedCallback(UpdateDisplayType)));

        public QuestionDisplayerPossibilities DisplayType
        {
            get { return (QuestionDisplayerPossibilities)GetValue(DisplayTypeProperty); }
            protected set { SetValue(DisplayTypeProperty, value); }
        }

        public BingoQuestion Question
        {
            get { return (BingoQuestion) GetValue(QuestionProperty); }
            set { SetValue(QuestionProperty, value); }
        }

        public bool DisplayAnswerInsteadOfTitle
        {
            get { return (bool)GetValue(DisplayAnswerInsteadOfTitleProperty); }
            set { SetValue(DisplayAnswerInsteadOfTitleProperty, value); }
        }

        private static void UpdateDisplayType(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var question = (BingoQuestion) (e.Property.Name == nameof(Question) ? e.NewValue as BingoQuestion : s.GetValue(QuestionProperty));
            
            if(question != null)
            {
                QuestionDisplayerPossibilities newValue;

                bool displayAnswerInstead = (bool) s.GetValue(DisplayAnswerInsteadOfTitleProperty),
                     textNullOrEmpty = string.IsNullOrEmpty(displayAnswerInstead ? question.Answer : question.Title),
                     imageNullOrEmpty = string.IsNullOrEmpty(displayAnswerInstead ? question.AnswerImagePath : question.TitleImagePath);

                if (textNullOrEmpty && imageNullOrEmpty)
                {
                    newValue = QuestionDisplayerPossibilities.Invalid;
                }
                else if (textNullOrEmpty)
                {
                    newValue = QuestionDisplayerPossibilities.DisplayImageOnly;
                }
                else if (imageNullOrEmpty)
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
