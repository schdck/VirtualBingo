using GalaSoft.MvvmLight.Messaging;
using VirtualBingo.UI.Shared.ENUMs;
using VirtualBingo.UI.Shared.Models;

namespace VirtualBingo.UI.Shared.Messages
{
    public class LaunchGameMessage : MessageBase
    {
        public BingoGame Game { get; private set; }        

        public LaunchGameMessage(object sender, BingoGame game)
        {
            Game = game;
        }
    }
}
