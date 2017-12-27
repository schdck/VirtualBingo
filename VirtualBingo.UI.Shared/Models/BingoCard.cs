using System.Collections.Generic;
using System.Linq;

namespace VirtualBingo.UI.Shared.Models
{
    public class BingoCard
    {
        private List<BingoQuestion> _Questions;

        public int Id { get; private set; }
        public int MaxAmountOfQuestions { get; private set; }
        public int Count => _Questions.Count;

        public BingoQuestion this[int index] { get => _Questions[index]; }

        public BingoCard(int id, int maxAmountOfQuestions)
        {
            _Questions = new List<BingoQuestion>(maxAmountOfQuestions);

            Id = id;
            MaxAmountOfQuestions = maxAmountOfQuestions;
        }

        public void Add(BingoQuestion item)
        {
            if (Count < MaxAmountOfQuestions)
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
            if(card.Count != Count)
            {
                return -1;
            }

            return _Questions.Intersect(card._Questions).Count() / Count;
        }

        public IEnumerator<BingoQuestion> GetEnumerator()
        {
            return _Questions.GetEnumerator();
        }
    }
}
