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

        private void btnFelvesz_Click(object sender, RoutedEventArgs e)
        {
            list.Add(new Tartaly(txtNev.Text, Convert.ToInt32(txtAel.Text), Convert.ToInt32(txtBel.Text), Convert.ToInt32(txtCel.Text)));
            lbTartalyok.Items.Clear();
            foreach (Tartaly item in list)
            {
                lbTartalyok.Items.Add(item.Info());
            }
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
    }

    class Tartaly
    {
        private String nev;
        private int a, b, c;
        private double aktLiter;

        public Tartaly(String nev, int a, int b, int c)
        {
            this.nev = nev;
            this.a = a;
            this.b = b;
            this.c = c;
            aktLiter = 0;
        }

        public Tartaly(String nev)
        {
            this.nev = nev;
            this.a = 10;
            this.b = 10;
            this.c = 10;
            aktLiter = 0;
        }
        public double Terfogat { get => a * b * c; }
        public double Toltottseg { get => aktLiter / (Terfogat / 1000) * 100; }
        public string Nev { get => nev; }
        public int AEl { get => a; }
        public int BEl { get => b; }
        public int CEl { get => c; }
        public double AktLiter { get => aktLiter; }

        public void Tolt(double liter)
        {
            if (Terfogat * 1000 < liter + aktLiter)
            {
                throw new OverflowException("Sok lesz!");
                return;
            }
            aktLiter += liter;
        }
        public void DuplazMeretet() => a *= 2;
        public void TeljesLeengedes() => aktLiter = 0;

        public string Info()
        {
            return $"{this.nev}: {this.Terfogat * 1000:n1} cm3 = ({this.Terfogat:n2} liter)," +
                $" töltöttsége: {this.Toltottseg:n2}%, ({this.aktLiter:n2} liter)," +
                $" méretei: {this.a}x{this.b}x{this.c} cm";

        }
    }
}
