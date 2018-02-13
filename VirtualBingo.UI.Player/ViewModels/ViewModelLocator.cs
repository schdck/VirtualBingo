using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;

namespace VirtualBingo.UI.Player.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<IDialogCoordinator, DialogCoordinator>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MenuViewModel>();
            SimpleIoc.Default.Register<CreateGameViewModel>();
            SimpleIoc.Default.Register<StartGameViewModel>();
            SimpleIoc.Default.Register<PlayViewModel>();
            SimpleIoc.Default.Register<PlayTabItemViewModel>();
            SimpleIoc.Default.Register<AnswersViewModel>();
            SimpleIoc.Default.Register<GenerateCardsViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public MenuViewModel Menu
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MenuViewModel>();
            }
        }

        public CreateGameViewModel CreateGame
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateGameViewModel>();
            }
        }

        public StartGameViewModel StartGame
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartGameViewModel>();
            }
        }

        public PlayViewModel Play
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlayViewModel>();
            }
        }

        public PlayTabItemViewModel PlayTabItem
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlayTabItemViewModel>();
            }
        }

        public AnswersViewModel Answers
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AnswersViewModel>();
            }
        }

        public GenerateCardsViewModel GenerateCards
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GenerateCardsViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}