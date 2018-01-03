using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VirtualBingo.UI.Shared.Converters;

namespace VirtualBingo.UI.Shared.Models
{
    public class BingoQuestion : IComparable<BingoQuestion>, INotifyPropertyChanged
    {
        #pragma warning disable CS0067
        public event PropertyChangedEventHandler PropertyChanged;
        #pragma warning restore CS0067

        public string Title { get; set; }
        public string Answer { get; set; }

        [JsonConverter(typeof(PathToFileNameJsonConverter))]
        public string TitleImagePath { get; set; }

        [JsonConverter(typeof(PathToFileNameJsonConverter))]
        public string AnswerImagePath { get; set; }

        public bool AlwaysDisplayTitleImage { get; set; }

        public int CompareTo(BingoQuestion other)
        {
            if (other == null)
            {
                return 1;
            }

            return Title.CompareTo(other.Title);
        }
    }
}
