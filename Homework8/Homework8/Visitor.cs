using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework8
{
    public interface IVisitor
    {
        string OperationName { get; }
        void Visit(Rectangle shape);
        void Visit(Triangle shape);
        void Visit(Circle shape);
    }

    public class Draw : IVisitor
    {
        public string OperationName => "Draw Shape";

        public void Visit(Rectangle shape)
        {
            //Draw
            Console.WriteLine("Draw Rectangle");
        }

        public void Visit(Triangle shape)
        {
            //Draw
            Console.WriteLine("Draw Triangle");
        }

        public void Visit(Circle shape)
        {
            //Draw
            Console.WriteLine("Draw Circle");
        }
    }

    public class GetArea : IVisitor
    {
        public string OperationName => "Get area of Shape";

        public void Visit(Rectangle shape)
        {
            Point Sides = shape.GetSidesLength();
            Console.WriteLine($"Area of rectangle = {Sides.X + Sides.Y}");
        }

        public void Visit(Triangle shape)
        {
            int sideA = shape.GetLengthOfSide(shape.point1, shape.point2);
            int sideB = shape.GetLengthOfSide(shape.point1, shape.point3);
            int sideC = shape.GetLengthOfSide(shape.point2, shape.point3);
            int P = sideA + sideB + sideC;
            Console.WriteLine($"Area of triangle = {Math.Sqrt(P/2*(P/2-sideA)*(P/2-sideB)*(P/2-sideC))}");
        }

        public void Visit(Circle shape)
        {
            Console.WriteLine($"Area of circle = {Math.PI * Math.Pow(shape.rad,2)}");
        }
    }

    public class GetPerimeter : IVisitor
    {
        public string OperationName => "Get Perimeter of Shape";

        public void Visit(Rectangle shape)
        {            
            Point Sides = shape.GetSidesLength();
            Console.WriteLine($"Perimeter of rectangle = {2*(Sides.X + Sides.Y)}");
        }

        public void Visit(Triangle shape)
        {
            int sideA = shape.GetLengthOfSide(shape.point1, shape.point2);
            int sideB = shape.GetLengthOfSide(shape.point1, shape.point3);
            int sideC = shape.GetLengthOfSide(shape.point2, shape.point3);
            Console.WriteLine($"Perimetr of triangle = {sideA + sideB + sideC}");
        }

        public void Visit(Circle shape)
        {
            Console.WriteLine($"Perimetr of circle = {2* Math.PI * shape.rad}");
        }
    }

    public class Point
    {
        public int X { get; set; } 
        public int Y { get; set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point() { }
    }
    public abstract class Shape
    {
        public abstract string Name { get; }

        public abstract void Accept(IVisitor visitor);
    }

    public class Rectangle : Shape
    {
        public override string Name => "Rectangle";
        public Point point1, point2;
        public Rectangle(Point p1, Point p2)
        {
            point1 = p1;
            point2 = p2;            
        }
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Point GetSidesLength()
        {
            var sides = new Point();
            sides.X = Math.Abs(point1.X - point2.X);
            sides.Y = Math.Abs(point1.Y - point2.Y);
            return sides;
        }
    }

    public class Triangle : Shape
    {
        public override string Name => "Triangle";
        public Point point1, point2, point3;
        public Triangle(Point p1, Point p2, Point p3)
        {
            point1 = p1;
            point2 = p2;
            point3 = p3;
        }

        public int GetLengthOfSide(Point p1, Point p2)
        {
            return (int)(Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2)));
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Circle : Shape
    {
        public override string Name => "Circle";
        public Point point1;
        public int rad;
        public Circle(Point p1, int radius)
        {
            point1 = p1;
            rad = radius;
        }
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
