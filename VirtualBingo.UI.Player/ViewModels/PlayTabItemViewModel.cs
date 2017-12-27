using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualBingo.UI.Shared.ENUMs;
using VirtualBingo.UI.Shared.Messages;

namespace VirtualBingo.UI.Player.ViewModels
{
    public class PlayTabItemViewModel : ViewModelBase
    {
        public PlayingState PlayingState { get; private set; }

        public PlayTabItemViewModel()
        {
            Messenger.Default.Register<LaunchGameMessage>(this, HandleLaunchGameMessage);
            Messenger.Default.Register<EndGameMessage>(this, HandleEndGameMessage);
        }

        private void HandleEndGameMessage(EndGameMessage obj)
        {
            PlayingState = PlayingState.StartingGame;
        }

        private void HandleLaunchGameMessage(LaunchGameMessage obj)
        {
            PlayingState = PlayingState.Playing;
        }
    }
}
