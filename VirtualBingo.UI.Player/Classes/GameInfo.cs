namespace VirtualBingo.UI.Player.Classes
{
    public class GameInfo
    {
        public string Language { get; private set; }
        public string Subject { get; private set; }
        public string Topic { get; private set; }

        public GameInfo(string language, string subject, string topic)
        {
            Language = language;
            Subject = subject;
            Topic = topic;
        }
    }
}
