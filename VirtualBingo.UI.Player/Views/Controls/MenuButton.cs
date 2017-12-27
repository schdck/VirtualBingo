using ControlzEx;
using System.Windows;
using System.Windows.Controls;

namespace VirtualBingo.UI.Player.Views.Controls
{
    public class MenuButton : Button
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(MenuButton), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(MenuButton), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(PackIconBase), typeof(MenuButton), new PropertyMetadata(default(PackIconBase)));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public PackIconBase Icon
        {
            get { return (PackIconBase)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
    }
}
