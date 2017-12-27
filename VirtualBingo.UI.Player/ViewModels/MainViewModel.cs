using GalaSoft.MvvmLight;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Deployment.Application;
using System.Windows;
using System;
using GalaSoft.MvvmLight.Messaging;
using VirtualBingo.UI.Shared.Messages;
using VirtualBingo.UI.Shared.ENUMs;

namespace VirtualBingo.UI.Player.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<MetroTabItem> Tabs { get; private set; }

        public string Version { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            InitializeProperties();
        }

        private void InitializeProperties()
        {
            Tabs = new ObservableCollection<MetroTabItem>();

            try
            {
                Version = string.Format("v{0}", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
            }
            catch
            {
                Version = null;
            }
        }
    }
}