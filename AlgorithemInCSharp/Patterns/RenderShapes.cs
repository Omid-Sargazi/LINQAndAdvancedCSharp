namespace AlgorithemInCSharp.Patterns
{
    public interface IRenderer
    {
        void RenderCirecle(string radius);
        void RenderSquare(string side);
        void RenderTriangle(string side);
    }

    public class VectorRender : IRenderer
    {
        public void RenderCirecle(string radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius} using vector graphics.");
        }

        public void RenderSquare(string side)
        {
            Console.WriteLine($"Drawing a square with side {side} using vector graphics.");
        }

        public void RenderTriangle(string side)
        {
            Console.WriteLine($"Drawing an equilateral triangle with side {side} using vector graphics.");
        }
    }

    public class RasterRender : IRenderer
    {
        public void RenderCirecle(string radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius} using pixels.");
        }

        public void RenderSquare(string side)
        {
            Console.WriteLine($"Drawing a square with side {side} using pixels.");
        }

        public void RenderTriangle(string side)
        {
            Console.WriteLine($"Drawing an equilateral triangle with side {side} using pixels.");
        }
    }

    public abstract class Shape
    {
        protected readonly IRenderer _renderer;
        public Shape(IRenderer renderer)
        {
            _renderer = renderer;
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }


    public class Circle : Shape
    {
        protected readonly string _radius;
        public Circle(string radius, IRenderer renderer) : base(renderer)
        {
            _radius = radius;
        }

        public override void Draw()
        {
            _renderer.RenderCirecle(_radius);
        }

        public override void Resize(float factor)
        {
            Console.WriteLine($"Circle resized. New radius: {factor}");
        }
    }

    public class Square : Shape
    {
        protected readonly string _side;
        public Square(string side, IRenderer renderer) : base(renderer)
        {
            _side = side;
        }

        public override void Draw()
        {
            _renderer.RenderSquare(_side);
        }

        public override void Resize(float factor)
        {
            throw new NotImplementedException();
        }
    }

    public class CLientRender
    {
        public static void Run()
        {
            Console.WriteLine("Bridge Pattern: Shapes and Renderers\n");
            
        }
    }
}