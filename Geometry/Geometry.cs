using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace POL_Lab05
{
    /*
        Stage_1 (1 Point) create struct 'Point' representing point in R^2 space (it should contain two doubles)
            * Implement public constructor with 2 parameters of correct type.
            * Implement public method 'Length' which calculates distance from an origin (O,0) to the point  
            * Implement public method 'ToString' inherited from 'Object' class, which returns '[X,Y]'.
    */


    struct Point
    {
        public double X;
        public double Y;
        public Point(double X, double Y) 
        {
            this.X = X;
            this.Y = Y;
        }
        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }
        public override string ToString()
        {
            return $"[{X},{Y}]";
        }

    }

    
    /*
        Stage_2 (1 point): Create abstract class 'Figure' representing geometric figure in R^2 space.
            * It should contain array of points accessible from derived classes (but not from outside).
            * Implement public constructor which takes variable amount of points.
                * Initialize array of points and copy received points.
            * Implement public method 'ComputeCenter' which calculates the center of the figure.
            * Implement public virtual method 'Description' which returns a following description 'Points: [x1,y1] [x2,y2] ...'.
                * Use ToString method from class Point.
                * U can use ordinary operations on 'String' class, but for those type of operations use of 'StringBuilder' class is highly recommended.  
            * Create public abstract method 'Area' which will calculate area of the figure.  
    */

    abstract class Figure 
    {
        protected Point[] points;
        
        public Figure(params Point[] points_) 
        {
            points = new Point[points_.Length];
            Array.Copy(points_ , 0 , points, 0, points_.Length);
        }

        public Point[] GetPoints()
        {
            return points;
        }

        public Point ComputeCenter()
        {
            double x_sum = 0;
            double y_sum = 0;
            foreach (var v in points)
            {
                x_sum += v.X;
                y_sum += v.Y;
            }
            return new Point(x_sum/points.Length, y_sum/points.Length);
        }

        public virtual string Description() 
        {
            string desc = "";
            foreach (var v in points)
                desc = desc + v.ToString() + " ";
            return "Points: " + desc;
        }
        public abstract double Area();
    }


    /*
        Stage_3 (3 points) Create classes 'Circle' and 'Rectangle', which are derived classes of 'Figure'/
            * Class 'Circle' (1.5 point):
                * Should contain one field of type double representing radius of a circle.
                * Implement public constructor which takes 2 parameters, representing the center of a circle and its radius.
                * Implement two methods 'GetCenter' and 'GetRadius' that return corresponding values.
                * Implement public method 'MaxPoint', which returns distance to the point on a circle which is furthers from the origin (0,0).
                    * (Distance from origin to circle center + radius)
                * Implement public method 'Area' inherited from the base class.
                * Implement public method 'Description' which describes circle in a following way: "Circle with center at: 'center' and radius: 'radius'".
            * Class 'Rectangle' (1.5 point):
                * Shouldn't contain any fields.
                * Implement constructor which takes two points (NW, SE), that defines rectangle.
                * Implement two methods 'GetPoints' that return the copy of points array.  
                * Implement public method 'Area' inherited from the base class.
                * Implement public method 'Description' which describes circle in a following way: "Rectangle with" appending points calling the base class.
    */

    class Circle : Figure
    {
        protected double radius;

        public Circle(Point Center, double radius) : base(Center)
        {
            this.radius = radius;
        }

        public Point GetCenter() { return ComputeCenter(); }
        public double GetRadius() { return radius; }

        public double MaxPoint() 
        {
            return GetCenter().Length() + radius;
        }

        public new Point[] GetPoints()
        {
            Point[] points_ = new Point[points.Length];
            Array.Copy(points, 0, points_, 0, points_.Length);
            return points_;
        }

        public override double Area()
        {
            return Math.PI * radius * radius;
        }

        public override string Description()
        {
            return "Circle with center at: " + GetCenter().ToString() + " and radius: " + GetRadius();
        }
    }

    class Rectangle : Figure
    {
        public Rectangle(Point NW, Point SE) : base(NW, SE)
        {

        }

        public new Point[] GetPoints()
        {
            Point[] points_ = new Point[points.Length];
            Array.Copy(points, 0, points_, 0, points_.Length);
            return points_;
        }

        public override double Area()
        {
            Point[] points = GetPoints();
            return (Math.Abs(points[1].X - points[0].X))* Math.Abs(points[1].Y - points[0].Y);
        }

        public override string Description()
        {
            return "Rectangle with " + base.Description();
        }
    }

}
