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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactsNamedays
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Nameday nday = new Nameday();

        public MainWindow()
        {
            InitializeComponent();
            GoogleConnection gc = new GoogleConnection();
            gc.start();

        }

        private void input_TextChanged(object sender, TextChangedEventArgs e)
        {
            label.Content = nday.GetDate(input.Text);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(!(label.Content == ""))
            {
                //pridat do tabulky//datatable//datagrid
            }
        }
    }
}
