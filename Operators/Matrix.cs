using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace Lab07
{
    /**
     * Class representing matrix mxn, ie. m-rows and n-columns
     */
    public class Matrix
    {
        private int m;
        private int n;
        private double[,] value;

        public Matrix(int m, int n)
        {
            this.m = m;
            this.n = n;
            value = new double[m, n];
        }

        public Matrix(double[,] val)
        {
            m = val.GetLength(0);
            n = val.GetLength(1);
            value = new double[m, n];

            for (int i = 0; i < val.GetLength(0); i++)
            {
                for(int j=0; j < val.GetLength(1); j++)
                {
                    value[i, j] = val[i, j];
                }
            }
        }

        public int M { get { return m; } }
        public int N { get { return n; } }

        public double this[Index i, Index j]
        {
            get 
            {
                if (i.GetOffset(m) > m || j.GetOffset(n) > n)
                    throw new IndexOutOfRangeException();
                return value[i.GetOffset(m), j.GetOffset(n)]; 
            } 
            set
            {
                if (i.GetOffset(m) > m || j.GetOffset(n) > n)
                    throw new IndexOutOfRangeException();
                this.value[i.GetOffset(m), j.GetOffset(n)] = value; 
            } 
        }
        public Matrix this[Range i, Range j]
        {
            get
            {
                (int ioffset, int ilength) = i.GetOffsetAndLength(m);
                (int joffset, int jlength) = j.GetOffsetAndLength(n);
                if (ilength > m || jlength > n)
                    throw new IndexOutOfRangeException();
                double[,] sub_matrix = new double[ilength, jlength];

                for(int k=ioffset; k < ilength+ioffset; k++)
                {
                    for(int l = joffset; l < jlength+joffset; l++) 
                    {
                        sub_matrix[k-ioffset, l-ioffset] = value[k, l];
                    }
                }
                return new Matrix(sub_matrix);
            }

            set
            {
                (int ioffset, int ilength) = i.GetOffsetAndLength(m);
                (int joffset, int jlength) = j.GetOffsetAndLength(n);
                if (ioffset > m || joffset > n)
                    throw new IndexOutOfRangeException();

                for (int k = ioffset; k < ilength+ioffset; k++)
                {
                    for (int l = joffset; l < jlength+joffset; l++)
                    {
                        this.value[k, l] = value[k - ioffset, l - joffset];
                    }
                }
            }
                
        }

        public Matrix this[Index i, Range j]
        {
            get 
            {  
                return (Matrix)this[i.GetOffset(m)..(i.GetOffset(m)+1), j];
            }
            set
            {
                this[i.GetOffset(m), j] = value;
            }
        }

        public Matrix this[Range i, Index j]
        {
            get
            {
                return (Matrix)this[i, j.GetOffset(n)..(j.GetOffset(n)+1)] ;
            }
            set
            {
                this[i, j.GetOffset(m)] = value;
            }
        }


        
        
        
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < M; i++)
            {
                builder.Append('|');
                for (int j = 0; j < N; j++)
                {
                    builder.Append($" {this[i, j],5:0.0} ");
                }
                builder.Append('|');
                if (i != M - 1) builder.Append('\n');
            }

            return builder.ToString();
        }

        public static explicit operator double[,](Matrix m)
        {
            return m.value;
        }
        public static explicit operator double(Matrix mat)
        {
            if (mat.m != 1 || mat.n != 1)
                throw new InvalidCastException();
            return mat.value[0, 0];
        }

        public static implicit operator Matrix(double[,] val)
        {
            return new Matrix(val);
        }
        public static implicit operator Matrix(double val)
        {
            double[,] new_double = new double[1, 1];
            new_double[1,1] = val;
            return new Matrix(new_double);
        }

        public override bool Equals(Object? obj)
        {
            if (obj == null)
                return false;
            var a = (Matrix)obj;
            if (a.m > this.m || a.n > this.n)
                return false;
            for (int i = 0; i < this.m; i++)
            {
                for (int j = 0; j < this.n; j++)
                {
                    if (a.value[i, j] != a.value[j, i])
                        return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator==(Matrix? a, Matrix? b) 
        {
            return a.Equals(b);
        }
        public static bool operator!=(Matrix a, Matrix b) 
        {
            return !a.Equals(b);
        }

    }
}