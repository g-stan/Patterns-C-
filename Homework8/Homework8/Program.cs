using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework8
{
    class Program
    {
        static void Main(string[] args)
        {
            Shape[] shapes = {
                new Rectangle(new Point(100,50), new Point(200,10)),
                new Triangle(new Point(100,50), new Point(200,10), new Point(300, 50)),
                new Circle(new Point(100,50), 20)};

            var drawVisitor = new Draw();
            var getAreaVisitor = new GetArea();
            var getPerimeterVisitor = new GetPerimeter();
            foreach (var curShape in shapes)
            {
                curShape.Accept(drawVisitor);
                curShape.Accept(getAreaVisitor);
                curShape.Accept(getPerimeterVisitor);
            }
            Console.ReadLine();
        }
    }
}
