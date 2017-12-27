using GalaSoft.MvvmLight.Messaging;
using VirtualBingo.UI.Shared.ENUMs;

namespace VirtualBingo.UI.Shared.Messages
{
    public class ChangeTabMessage : MessageBase
    {
        public TabType TabType { get; private set; }

        public ChangeTabMessage(object sender, TabType tabType) : base(sender)
        {
            TabType = tabType;
        }
    }
}
