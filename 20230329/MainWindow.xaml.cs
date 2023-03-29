using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
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

namespace _20230329
{
    public partial class MainWindow : Window
    {
        List<Tartaly> list = new List<Tartaly>();
        public MainWindow()
        {
            InitializeComponent();
            rdoTeglatest.IsChecked = true;
        }

        private void rdoTeglatest_Checked(object sender, RoutedEventArgs e)
        {
            txtAel.IsEnabled = txtBel.IsEnabled = txtCel.IsEnabled = true;
            txtAel.Text = txtBel.Text = txtCel.Text = "";
            txtAel.Focus();
        }

        private void rdoKocka_Checked(object sender, RoutedEventArgs e)
        {
            txtAel.IsEnabled = txtBel.IsEnabled = txtCel.IsEnabled = false;
            txtAel.Text = txtBel.Text = txtCel.Text = "10";
        }

        private void ListUpdate()
        {
            lbTartalyok.Items.Clear();
            foreach (Tartaly item in list)
            {
                lbTartalyok.Items.Add(item.Info());
            }
        }

        private void btnFelvesz_Click(object sender, RoutedEventArgs e)
        {
            list.Add(new Tartaly(txtNev.Text, Convert.ToInt32(txtAel.Text), Convert.ToInt32(txtBel.Text), Convert.ToInt32(txtCel.Text)));
            ListUpdate();
        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter("tartalyok.csv");
            foreach (Tartaly item in list)
            {
                string line = $"{item.Nev};{item.AEl};{item.BEl};{item.CEl};{item.AktLiter}";
                sw.WriteLine(line);
            }

            sw.Close();
            MessageBox.Show("Sikeres fájlbaírás!");
        }

        private bool CheckSelectedItem()
        {
            if (lbTartalyok.SelectedIndex < 0)
            {
                MessageBox.Show("Nem lett kiválasztva elem!");
                return false;
            }
            return true;
        }
        
        private void btnDuplaz_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelectedItem())
            {
                list[lbTartalyok.SelectedIndex].DuplazMeretet();
                ListUpdate();
            }
        }

        private void btnLeenged_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelectedItem())
            {
                list[lbTartalyok.SelectedIndex].TeljesLeengedes();
                ListUpdate();
            }
        }

        private void btntolt_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelectedItem())
            {
                int index = lbTartalyok.SelectedIndex;
                try
                {
                    double toCharge = Convert.ToDouble(txtMennyitTolt.Text);
                    list[lbTartalyok.SelectedIndex].Tolt(toCharge);
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Túl nagy szám!");
                    return;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Nem szám lett megadva!");
                    return;
                }
                ListUpdate();
                lbTartalyok.Focus();
            }
        }
    }

    
}
