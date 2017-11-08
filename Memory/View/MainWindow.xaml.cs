using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;


namespace Memory.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Size screenSize;

        public MainWindow()
        {
            InitializeComponent();
            Show();
        }

        /// <summary>
        /// Creates a button
        /// </summary>
        /// <param name="panel">The parent to which add this button</param>
        /// <param name="x">The button column</param>
        /// <param name="y">The button row</param>
        /// <param name="width">The button width</param>
        /// <param name="height">The button height</param>
        /// <param name="content">The text displayed in the button. Optional</param>
        /// <param name="marginLeft">The margin to keep at the left of the button. Optional</param>
        /// <param name="marginTop">The margin to keep at the top of the button. Optional</param>
        /// <param name="marginRight">The margin to keep at the right of the button. Optional</param>
        /// <param name="marginBottom">The margin to keep at the bottom of the button. Optional</param>
        /// <returns>The created button</returns>
        public Button CreateCard(Panel panel, int x, int y, int width, int height, string content = "", int marginLeft = 0, int marginTop = 0, int marginRight = 0, int marginBottom = 0)
        {
            Button card = new Button();
            Grid.SetRow(card, y);
            Grid.SetColumn(card, x);
            card.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
            card.Height = height;
            card.Width = width;
            card.Content = content;
            panel.Children.Add(element: card);
            panel.UpdateLayout();

            return card;
        }
    }
}
