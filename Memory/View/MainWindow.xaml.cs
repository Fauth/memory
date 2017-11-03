using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
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

namespace Memory.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {
        int N = 10;
        public MainWindow()
        {

            



            //Button C1 = new Button();   

            //C1.Margin = new Thickness((double)100, (double)10, (double)0, (double)0);

            ObservableCollection<Button> Buttons = new ObservableCollection<Button>();
            for (int k = 0; k<N; k++ )
            { 
             Buttons.Add(item: new Button() { Margin = new Thickness(10), Width = 127, Height = 200, Name= $"Card{k.ToString()}" });
             Buttons[k].Click += Button_Click;
             Buttons[k].Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/ICO ("+(k+1).ToString()+").png")));
            }


            DataContext = Buttons;
            InitializeComponent();
        }


        //private void C1_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("You clicked Card 1", "Info");

        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button_clicked = (Button)sender;
            string buttonId = button_clicked.Name as string;
            MessageBox.Show("You clicked " + buttonId, "Info");

        }
    }
}
