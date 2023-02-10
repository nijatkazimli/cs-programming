using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace LAB10_EN
{
    //Stage 2. - 1 points
    public static class BaseFunctions
    {
        public static Func<double, double> ConstantFunction(double d)
        {
            return delegate (double x) { return d; };
        }

        public static Func<double, double> QuadraticFunction(double a, double b, double c)
        {
            return delegate (double x) { return a*x*x + b*x + c; };
        }

        public static Func<double, double> PolynomialFunction(params double[] tab)
        {
            return delegate (double x)
            {
                double val = 0;
                int j = 0;
                for(int i = tab.Length - 1; i >= 0; i--)
                {
                    double temp1 = tab[i];
                    for (int k = 0; k < j; k++)
                        temp1 *= x;
                    val += temp1;
                    j++;
                }
                return val;
            };
        }


    }
    
    //Stage 3. - 1 point
    public static class FunctionsManipulator
    {
        public static Func<double, (double, double)> NewPoint(Func<double, double> f, Func<double, double> g) => x => (f(x), g(x));
        public static Func<double, double> Power(Func<double, double> f, Func<double, double> g) => x => Math.Pow(f(x), g(x));
        public static Func<double, double> CombineFunctions(Func<double, double> f, Func<double, double> g) => x => f(g(x));
    }
    
    //Stage 4. - 2 points
    public static class ExtensionMethods
    {
       public static void ForEachWithBrake<T>(this IEnumerable<T> collection, Action<T> action, Func<T,bool> func) 
        {
            foreach (var item in collection)
                if (func(item) != false)
                {
                    action(item);
                }
                else
                    break;
        }
        public static List<T> Distinct<T>(this IEnumerable<T> collection, Comparison )
    }
}