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
        public MainWindow()
        {
            InitializeComponent();
            CreateCard(100, 0, 100, 100);
            this.Show();
        }


        public void CreateCard(int x, int y, int width, int height)
        {
            System.Windows.Controls.Button card = new Button();
            card.Margin = new Thickness(x, y,0,0);
            card.Height = 40;
            card.Width = 300;
            card.Content = "A New Button";
            grid.Children.Add(card);
        }
    }
}
