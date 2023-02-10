using System;

namespace Lab9
{

    class Matrix2D<T>
    {
        public int m;
        public int n;
        public T[,] matrix;
        public Matrix2D(T[,] mat)
        {
            matrix = mat;
            m = matrix.GetLength(0);
            n = matrix.GetLength(1);
        }

        public T[] this[Index i, Range j]
        {
            get
            {
                (int joffset, int jlength) = j.GetOffsetAndLength(n);
                T[] sub_matrix = new T[jlength];
                int l = 0;
                for (int k = joffset; k < jlength + joffset; k++, l++)
                {
                    sub_matrix[l] = matrix[i.GetOffset(m), k];
                }
                return sub_matrix;
            }
            
            set 
            {
                (int joffset, int jlength) = j.GetOffsetAndLength(n);
                int l = i.GetOffset(m);
                for (int k = joffset; k < jlength + joffset; k++)
                {
                    matrix[l,k] = value[k - joffset];
                }
            }
        }
    }

    static class Matrix2D_ext
    {
        public static double Sum<T>(this Matrix2D<T> mat)
        {
            double sum = 0;
            for (int i = 0; i < mat.m; i++)
            {
                for (int j = 0; j < mat.n; j++)
                {
                    sum += Convert.ToDouble( mat.matrix[i, j]);
                }
            }
            return sum;
        }

        public static double Average<T>(this Matrix2D<T> mat)
        {
            double sum = mat.Sum<T>();
            return sum / (mat.m * mat.n);
        }

        public static T Max<T>(this Matrix2D<T> mat)
            where T : IComparable<T>
        {
            T max = mat.matrix[0, 0];
            for(int i = 0; i < mat.m; i++) 
            {
                for(int j = 0; j < mat.n; j++) 
                {
                    if (max.CompareTo(mat.matrix[i,j]) < 0)
                        max = mat.matrix[i, j];
                }
            }
            return max;
        }

        public static T Min<T>(this Matrix2D<T> mat)
        where T : IComparable<T>
        {
            T min = mat.matrix[0, 0];
            for (int i = 0; i < mat.m; i++)
            {
                for (int j = 0; j < mat.n; j++)
                {
                    if (min.CompareTo(mat.matrix[i, j]) > 0)
                        min = mat.matrix[i, j];
                }
            }
            return min;
        }

        public static T[] Flatten<T>(this Matrix2D<T> mat)
        {
            T[] flat = new T[mat.m * mat.n];
            int write = 0;
            for(int i = 0; i < mat.m; i++)
            {
                for(int j = 0; j< mat.n;j++)
                    flat[write++] = mat.matrix[i, j];
            }
            return flat;

        }

        public static void AddConst(this Matrix2D<double> mat, double c)
        {
            for(int i = 0; i < mat.m; i++) 
            {
                for (int j = 0; j < mat.n; j++)
                    mat.matrix[i, j] += c;
            }
        }
        public static void AddConst(this Matrix2D<int> mat, int c)
        {
            for (int i = 0; i < mat.m; i++)
            {
                for (int j = 0; j < mat.n; j++)
                    mat.matrix[i, j] += c;
            }
        }

        public static void AddConst(this Matrix2D<uint> mat, uint c)
        {
            for (int i = 0; i < mat.m; i++)
            {
                for (int j = 0; j < mat.n; j++)
                    mat.matrix[i, j] += c;
            }
        }
    }


}
