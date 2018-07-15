using System;
using System.Collections.Generic;
using System.Data;
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
        Nameday nday;
        GoogleConnection gc;
        public DataTable data;
        

        public MainWindow()
        {
            InitializeComponent();
            nday = new Nameday();
            gc = new GoogleConnection();
            data = gc.GetContactsNames();
            dataGrid.DataContext = data.DefaultView;

            dataGrid.CellEditEnding += DataGrid_CellEditEnding;
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
               
                //string s = (dataGrid.Items[0] as DataRowView).Row.ItemArray[0].ToString();








            }
            throw new NotImplementedException();
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

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
