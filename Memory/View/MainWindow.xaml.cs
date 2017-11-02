using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public MainWindow()
        {
            var Cards = new ObservableCollection<Button>();

            //Button C1 = new Button();   
  
            //C1.Margin = new Thickness((double)100, (double)10, (double)0, (double)0);
            InitializeComponent();

        }


        private void C1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked Card 1", "J'ai perdu");

        }

        
      
    }
}
