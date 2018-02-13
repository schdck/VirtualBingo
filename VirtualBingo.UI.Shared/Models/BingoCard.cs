using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace VirtualBingo.UI.Shared.Models
{
    public class BingoCard : IEnumerable, INotifyPropertyChanged
    {
        public List<BingoQuestion> _Questions;

        #pragma warning disable CS0067
        public event PropertyChangedEventHandler PropertyChanged;
        #pragma warning restore CS0067

        public int Id { get; private set; }
        public int MaxAmountOfQuestions { get; private set; }
        public int QuestionCount => _Questions.Count;

        public BingoQuestion this[int index] { get => _Questions[index]; }

        public BingoCard(int id, int maxAmountOfQuestions)
        {
            _Questions = new List<BingoQuestion>(maxAmountOfQuestions);

            Id = id;
            MaxAmountOfQuestions = maxAmountOfQuestions;
        }

        public void Add(BingoQuestion item)
        {
            if (QuestionCount < MaxAmountOfQuestions)
            {
                _Questions.Add(item);
            }
        }

        public bool Remove(BingoQuestion item)
        {
            return _Questions.Remove(item);
        }

        public void Clear()
        {
            _Questions.Clear();
        }

        public bool Contains(BingoQuestion item)
        {
            return _Questions.Contains(item);
        }

        public double CompareTo(BingoCard card)
        {
            if(card.QuestionCount != QuestionCount)
            {
                return -1;
            }

            return _Questions.Intersect(card._Questions).Count() / QuestionCount;
        }

        public IEnumerator GetEnumerator()
        {
            return _Questions.GetEnumerator();
        }
    }
}
