using System;
using System.Diagnostics.CodeAnalysis;
using System.IO.Pipes;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Lab06
{
    struct Vector3D
    {
        private double valueX;
        private double valueY;
        private double valueZ;

        /* Your Implementation */

        public Vector3D(double ValueX, double ValueY, double ValueZ)
        {
            this.valueX = ValueX;
            this.valueY = ValueY;
            this.valueZ = ValueZ;
        }

        public double X { get { return valueX; } set { valueX = value; } }
        public double Y { get { return valueY; } set { valueY = value; } }
        public double Z { get { return valueZ; } set { valueZ = value; } }

        public override int GetHashCode() =>
         new { X, Y, Z }.GetHashCode();

        public override bool Equals(object? obj)
        {
            if (obj is not Vector3D vector)
                return false;
            return vector.X == X && vector.valueY == Y && vector.valueZ == Z;
        }

        public static bool operator ==(Vector3D obj1, Vector3D obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Vector3D obj1, Vector3D obj2)
        {
            return !(obj1 == obj2);
        }

        public override string ToString()
        {
            return String.Format("Vector3D(X = {0}, Y = {1}, Z = {2})", X, Y, Z);
        }





        public static Vector3D Zero = new Vector3D(0, 0, 0);
        public static Vector3D One = new Vector3D(1, 1, 1);
        public static Vector3D UnitX = new Vector3D(1, 0, 0);
        public static Vector3D UnitY = new Vector3D(0, 1, 0);
        public static Vector3D UnitZ = new Vector3D(0, 0, 1);


        public double Length
        {
            get { return Math.Sqrt(Length2); }
        }

        public double Length2
        {
            get { return X * X + Y * Y + Z * Z; }
        }

        public static double Dot(Vector3D a, Vector3D b)
        {
            return a.valueX * b.valueX + a.valueY * b.valueY + a.valueZ * b.valueZ;
        }

        public double Dot(Vector3D other)
        {
            return valueX * other.valueX + valueY * other.valueY + valueZ * other.valueZ;
        }

        public static Vector3D Cross(Vector3D a, Vector3D b)
        {
            double x_ = a.Y * b.Z - b.Y * a.Z;
            double y_ = (a.X * b.Z - b.X * a.Z) * -1;
            double z_ = a.X * b.Y - b.X * a.Y;

            return new Vector3D(x_, y_, z_);
        }

        public Vector3D Cross(Vector3D b)
        {
            double x_ = Y * b.Z - b.Y * Z;
            double y_ = (X * b.Z - b.X * Z) * -1;
            double z_ = X * b.Y - b.X * Y;

            return new Vector3D(x_, y_, z_);
        }





        public void Deconstruct(out double px, out double py, out double pz)
        {
            px = X;
            py = Y;
            pz = Z;
        }

        public static implicit operator Vector3D((double, double, double) Tuple) => new Vector3D(Tuple.Item1, Tuple.Item2, Tuple.Item3);
        public static explicit operator (double, double, double) (Vector3D vector) => (vector.X, vector.Y, vector.Z);

        public double this[int i]
        {
            get
            {
                if(i < 0 || i >2 ) throw new IndexOutOfRangeException(nameof(i));
                if (i == 0)
                    return X;
                if(i == 1)
                    return Y;
                    return Z;
            }

            set 
            {
                if (i < 0 || i > 2) throw new IndexOutOfRangeException(nameof(i));
                if (i == 0)
                    X = value;
                if(i==1)
                    Y = value;
                if (i == 2)
                    Z = value;
            }
        }

        public void Normalize() 
        {
            if (Length == 0)
                return;
            double Length_ = Length;
            X = X / Length_;
            Y = Y / Length_;
            Z = Z / Length_;
        }

        public Vector3D Normalized()
        {
            if (Length == 0)
                return Zero;
            return new Vector3D(X/Length, Y/Length, Z/Length);
        }

        public static Vector3D MakeUnit(double x, double y, double z)
        {
            Vector3D vector = new Vector3D(x, y, z);
            return vector.Normalized();
        }

        public bool AlmostEquals(Vector3D obj2, double tolerance = 1e-7) 
        {
            return Math.Abs(X - obj2.X) < tolerance && Math.Abs(Y - obj2.Y) < tolerance && Math.Abs(Z - obj2.Z) < tolerance;
        }

        public bool AlmostZero(double tolerance = 1e-7)
        {
            return Math.Abs(X) < tolerance && Math.Abs(Y) < tolerance && Math.Abs(Z) < tolerance;
        }

        public bool IsNaN() 
        {
            return double.IsNaN(X) || double.IsNaN(Y) || double.IsNaN(Z);
        }

        public static bool IsNan(Vector3D obj)
        {
            return double.IsNaN(obj.X) || double.IsNaN(obj.Y) || double.IsNaN(obj.Z);
        }
    }
}
