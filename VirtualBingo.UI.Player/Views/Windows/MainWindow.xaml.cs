using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using System;
using System.Windows.Controls;
using VirtualBingo.UI.Player.Views.TabItems;
using VirtualBingo.UI.Shared.ENUMs;
using VirtualBingo.UI.Shared.Messages;

namespace VirtualBingo.UI.Player.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            Messenger.Default.Register<ChangeTabMessage>(this, HandleLaunchTabMessage);

            InitializeComponent();
        }

        private void HandleLaunchTabMessage(ChangeTabMessage obj)
        {
            Type tabItemType = GetTypeFromTabType(obj.TabType);

            if(tabItemType != null)
            {
                for (int i = 0; i < TabControl.Items.Count; i++)
                {
                    if(TabControl.Items[i].GetType() == tabItemType)
                    {
                        TabControl.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private Type GetTypeFromTabType(TabType tabType)
        {
            switch (tabType)
            {
                case TabType.Play:
                    return typeof(PlayTabItem);

                case TabType.CreateGame:
                    return typeof(CreateGameTabItem);

                case TabType.Explore:
                    return typeof(ExploreTabItem);

                case TabType.GenerateCards:
                    return typeof(GenerateCardsTabItem);

                case TabType.Settings:
                    return typeof(SettingsTabItem);

                case TabType.Help:
                    return typeof(HelpTabItem);
            }

            return null;
        }
    }
}
