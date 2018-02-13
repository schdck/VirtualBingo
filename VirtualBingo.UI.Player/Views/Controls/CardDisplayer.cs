using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VirtualBingo.UI.Shared.Models;

namespace VirtualBingo.UI.Player.Views.Controls
{
    public class CardDisplayer : Control
    {
        private static readonly DependencyPropertyKey AmountOfColumnsPropertyKey = DependencyProperty.RegisterReadOnly("AmountOfColumns", typeof(int), typeof(CardDisplayer), new FrameworkPropertyMetadata(3));
        private static readonly DependencyPropertyKey QuestionsPropertyKey = DependencyProperty.RegisterReadOnly("Questions", typeof(ObservableCollection<BingoQuestion>), typeof(CardDisplayer), new FrameworkPropertyMetadata(default(ObservableCollection<BingoQuestion>)));

        public static readonly DependencyProperty CardProperty = DependencyProperty.Register("Card", typeof(BingoCard), typeof(CardDisplayer), new PropertyMetadata(new PropertyChangedCallback(CardUpdated)));

        public static readonly DependencyProperty QuestionsProperty = QuestionsPropertyKey.DependencyProperty;

        public static readonly DependencyProperty AmountOfColumnsProperty = AmountOfColumnsPropertyKey.DependencyProperty;

        public BingoCard Card
        {
            get { return (BingoCard)GetValue(CardProperty); }
            set { SetValue(CardProperty, value); }
        }

        public ObservableCollection<BingoCard> Questions
        {
            get { return (ObservableCollection<BingoCard>) GetValue(QuestionsProperty); }
            protected set { SetValue(QuestionsProperty, value); }
        }

        public int AmountOfColumns
        {
            get { return (int)GetValue(AmountOfColumnsProperty); }
            protected set { SetValue(AmountOfColumnsProperty, value); }
        }

        private static void CardUpdated(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var card = e.NewValue as BingoCard;

            if (card != null && card.QuestionCount > 0)
            {
                if (card.QuestionCount % 3 == 0)
                {
                    s.SetValue(AmountOfColumnsPropertyKey, 3);
                }
                else if (card.QuestionCount % 5 == 0)
                {
                    s.SetValue(AmountOfColumnsPropertyKey, 5);
                }
                else
                {
                    s.SetValue(AmountOfColumnsPropertyKey, 4);
                }
            }
        }
    }
}
