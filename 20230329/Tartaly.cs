using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230329
{
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
        public double Toltottseg { get => aktLiter * 1000 / Terfogat / 10; }
        public string Nev { get => nev; }
        public int AEl { get => a; }
        public int BEl { get => b; }
        public int CEl { get => c; }
        public double AktLiter { get => aktLiter; }

        public void Tolt(double liter)
        {
            if (Terfogat / 1000 < liter + aktLiter)
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
