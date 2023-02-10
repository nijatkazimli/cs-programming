using System.Collections;
using System.Security.Cryptography;

namespace Lab8_EN
{
    interface IFormula
    {
        double Calculate(double x);
        string PrintFormula();
        
    }

    class ArithmeticSequenceSumFormula : IFormula
    {
        public double A1 { get; set; }
        public double D { get; set; }

        public ArithmeticSequenceSumFormula(double a1, double d)
        {
            A1 = a1;
            D = d;
        }
        double IFormula.Calculate(double x)
        {
            double a_n = A1 + D * (x - 1);
            return x * (A1 + a_n) / 2;
        }

        string IFormula.PrintFormula()
        {
            return "f(n)=n*(" + A1 + "+a_n)/2";
        }
    }

    class GeometricSequenceSumFormula : IFormula
    {

        public double A1 { get; set; }
        public double R { get; set; }

        public GeometricSequenceSumFormula(double a1, double r)
        {
            A1 = a1;
            R = r;
        }

        double IFormula.Calculate(double x)
        {
            return A1 * (1 - Math.Pow(R, x)) / (1 - R);
        }
        string IFormula.PrintFormula()
        {
            return "" + A1 + "(1-" + R + "^n)/(1-" + R + ")";
        }
    }

    abstract class Generator : IEnumerable
    {
        public readonly IFormula frm;

        public string Formula
        {
            get { return frm.PrintFormula(); }
        }

        public Generator(IFormula frm) 
        {
            this.frm = frm;
        }

        public abstract IEnumerator GetEnumerator();
    }

    class FibonacciGenerator : Generator
    {
        public FibonacciGenerator(IFormula frm) : base(frm)
        {
        }
        public override IEnumerator GetEnumerator()
        {
            int i = 0;
            int j = 1;
            yield return frm.Calculate(i);
            yield return frm.Calculate(j);
            while (true) 
            {
                yield return frm.Calculate(i+j);
                int temp = j;
                j = i + j;
                i = temp;
            }
        }
    }

    class WeirdFibonacciGenerator : Generator
    {
        public WeirdFibonacciGenerator(IFormula frm) : base(frm)
        {
        }

        public override IEnumerator GetEnumerator()
        {
            int i = 2;
            int j = 1;
            int k = 3;
            yield return frm.Calculate(i);
            yield return frm.Calculate(j);
            yield return frm.Calculate(k);
            while (true) 
            {
                yield return frm.Calculate(2 * i + 3 * j + k);
                int temp = 2 * i + 3 * j + k;
                i = j;
                j = k;
                k = temp;
            }
        }
    }

    class DefaultFormula : IFormula
    {

        double IFormula.Calculate(double x)
        {
            while (true)
                yield return x++;
        }

        string IFormula.PrintFormula()
        {
            return "D(n)=n";
        }
    }

}
