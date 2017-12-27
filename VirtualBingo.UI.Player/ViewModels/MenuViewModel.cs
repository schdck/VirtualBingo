using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VirtualBingo.UI.Shared.ENUMs;
using VirtualBingo.UI.Shared.Messages;

namespace VirtualBingo.UI.Player.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public RelayCommand<TabType> ChangeTabCommand { get; private set; }

        public MenuViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            ChangeTabCommand = new RelayCommand<TabType>((x) =>
            {
                MessengerInstance.Send(new ChangeTabMessage(this, x));
            });
        }
    }
}
